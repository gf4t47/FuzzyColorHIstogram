using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using FCH;

namespace FuzzyColorHIstogram.FCH
{
    public class FuzzyColorHistogram
    {
        private readonly int _nb;
        private readonly int _n;
        private readonly double _m;
        private readonly double _e;

        public FuzzyColorHistogram()
        {
            _nb = 8;
            _n = 4;
            _m = 1.90;
            _e = 0.25;
        }

        public FuzzyColorHistogram(int nb, int n, double m, double e)
        {
            _nb = nb;
            _n = n;
            _m = m;
            _e = e;
        }

        private DenseHistogram CalcRGB(Image<Bgr, Byte> img, int bins)
        {
            var hRange = new RangeF(0f, 255f);
            var imagesRGB = img.Split();

            var hist = new DenseHistogram(new[] { bins, bins, bins }, new[] { hRange, hRange, hRange });
            hist.Calculate(new IImage[] { imagesRGB[0], imagesRGB[1], imagesRGB[2] }, false, null);

            return hist;
        }

        public Image<Lab, Byte> Convert(Image<Bgr, Byte> src)
        {
            return src.Convert<Lab, Byte>();
        }

        private static List<ColorBins> GetColorBins(int binNum, int dimension)
        {
            var tick = 256 / binNum;

            var ranges = new List<Range<Byte>>();
            for (int i = 0; i < binNum; i++)
            {
                ranges.Add(new Range<Byte>((Byte)(i * tick), (Byte)((i + 1) * tick - 1)));
            }

            var bins = new List<ColorBins>((int)Math.Pow(binNum, dimension));
            for (var d1 = 0; d1 < binNum; d1++)
            {
                for (var d2 = 0; d2 < binNum; d2++)
                {
                    for (var d3 = 0; d3 < binNum; d3++)
                    {
                        var rgs = new List<Range<Byte>>(dimension) { ranges[d1], ranges[d2], ranges[d3] };
                        bins.Add(new ColorBins(rgs));
                    }
                }
            }

            return bins;
        }

        private unsafe List<ColorBins> convertLAB(IReadOnlyCollection<ColorBins> rgbBins)
        {
            if (rgbBins == null) throw new ArgumentNullException("rgbBins");
            var labBins = new List<ColorBins>(rgbBins.Count);

            foreach (var rgbBin in rgbBins)
            {
                byte* rgbMin = stackalloc byte[3];
                *rgbMin = rgbBin.Colors[0].Minimum;
                *(rgbMin + 1) = rgbBin.Colors[1].Minimum;
                *(rgbMin + 2) = rgbBin.Colors[2].Minimum;
                byte* labMin = stackalloc byte[3];
                RGBLAB.ToLAB(rgbMin, labMin);

                byte* rgbMax = stackalloc byte[3];
                *rgbMin = rgbBin.Colors[0].Maximum;
                *(rgbMin + 1) = rgbBin.Colors[1].Maximum;
                *(rgbMin + 2) = rgbBin.Colors[2].Maximum;
                byte* labMax = stackalloc byte[3];
                RGBLAB.ToLAB(rgbMax, labMax);

                var ranges = new List<Range<byte>> { new Range<byte>(*labMin, *labMax), 
                                                                   new Range<byte>(*(labMin + 1), *(labMax + 1)), 
                                                                   new Range<byte>(*(labMin + 2), *(labMax + 2)) };
                labBins.Add(new ColorBins(ranges));
            }

            return labBins;
        }

        public List<int> CalcFCH(Image<Bgr, Byte> img)
        {
            var cchBins = _nb;
            var fchBins = _n;
            const int dimension = 3;
            var cchHist = CalcRGB(img, cchBins);

            var rgbBins = GetColorBins(cchBins, dimension);
            var labBins = convertLAB(rgbBins);

            var cchBinsTotal = (int)Math.Pow(cchBins, dimension);
            var fchBinsTotal = (int)Math.Pow(fchBins, dimension);
            var fcm = new FuzzyCMeans(labBins, _m, _e, fchBinsTotal);
            fcm.RunFCM();
            var matrix = fcm.MatrixU;

            var fch = new List<int>(fchBinsTotal);
            for (var j = 0; j < fchBinsTotal; j++)
            {
                double sum = 0;
                for (var i = 0; i < cchBinsTotal; i++)
                {
                    sum += cchHist[i] * matrix[i, j];
                }
                fch.Add((int)sum);
            }

            return fch;
        }

        public Image<Bgr, Byte> GenerateRGBHistImage(List<int> hist)
        {
            const int width = 480;
            const int widthOffset = 20;
            const int height = 640;

            var maxValue = hist.Max();

            var imageHist = new Image<Bgr, Byte>(width, height, new Bgr(255d, 255d, 255d));
            var heightPerTick = 1d * height / maxValue;
            var widthPerTick = 1d * (width - widthOffset * 2) / hist.Count;
            var color = new Bgr(0d, 0d, 0d);

            for (var i = 0; i < hist.Count; i++)
            {
                var xPos = (int)(i * widthPerTick) + widthOffset;
                var yPos = (int)(height - heightPerTick * hist[i]);
                var line = new LineSegment2D(new Point(xPos, height), new Point(xPos, yPos));
                imageHist.Draw(line, color, 2);
            }

            return imageHist;
        }
    }
}
