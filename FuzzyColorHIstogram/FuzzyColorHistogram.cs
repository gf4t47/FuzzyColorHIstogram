using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FCH
{
    public class FuzzyColorHistogram
    {
        private int n_;
        private int n;
        private double m;
        private double e;

        public FuzzyColorHistogram()
        {
            n_ = 8;
            n = 4;
            m = 1.90;
            e = 0.25;
        }

        public FuzzyColorHistogram(int n_, int n, double m, double e)
        {
            this.n_ = n_;
            this.n = n;
            this.m = m;
            this.e = e;
        }

        public DenseHistogram calcRGB(Image<Bgr, Byte> img, int bins)
        {
            RangeF hRange = new RangeF(0f, 255f);
            Image<Gray, Byte>[] imagesRGB = img.Split();

            DenseHistogram hist = new DenseHistogram(new int[] { bins, bins, bins }, new RangeF[] { hRange, hRange, hRange });
            hist.Calculate(new IImage[] { imagesRGB[0], imagesRGB[1], imagesRGB[2] }, false, null);

            return hist;
        }

        public DenseHistogram calcLAB(Image<Lab, Byte> img, int bins)
        {
            RangeF hRange = new RangeF(0f, 255f);
            Image<Gray, Byte>[] imagesRGB = img.Split();

            DenseHistogram hist = new DenseHistogram(new int[] { bins, bins, bins }, new RangeF[] { hRange, hRange, hRange });
            hist.Calculate(new IImage[] { imagesRGB[0], imagesRGB[1], imagesRGB[2] }, false, null);

            return hist;
        }

        public Image<Lab, Byte> convert(Image<Bgr, Byte> src)
        {
            return src.Convert<Lab, Byte>();
        }

        private List<ColorBins> getColorBins(int bin_num, int dimension)
        {
            int tick = 256 / bin_num;

            List<Range<Byte>> ranges = new List<Range<Byte>>();
            for (int i = 0; i < bin_num; i++)
            {
                ranges.Add(new Range<Byte>((Byte)(i * tick), (Byte)((i + 1) * tick - 1)));
            }

            List<ColorBins> bins = new List<ColorBins>((int)Math.Pow(bin_num, dimension));
            for (int d1 = 0; d1 < bin_num; d1++)
            {
                for (int d2 = 0; d2 < bin_num; d2++)
                {
                    for (int d3 = 0; d3 < bin_num; d3++)
                    {
                        List<Range<Byte>> rgs = new List<Range<Byte>>(dimension) { ranges[d1], ranges[d2], ranges[d3] };
                        bins.Add(new ColorBins(rgs));
                    }
                }
            }

            return bins;
        }

        private unsafe List<ColorBins> convertLAB(List<ColorBins> rgb_bins)
        {
            List<ColorBins> lab_bins = new List<ColorBins>(rgb_bins.Count);

            foreach (ColorBins rgb_bin in rgb_bins)
            {
                byte* rgb_min = stackalloc byte[3];
                *rgb_min = rgb_bin.colors[0].Minimum;
                *(rgb_min + 1) = rgb_bin.colors[1].Minimum;
                *(rgb_min + 2) = rgb_bin.colors[2].Minimum;
                byte* lab_min = stackalloc byte[3];
                RGBLAB.ToLAB(rgb_min, lab_min);

                byte* rgb_max = stackalloc byte[3];
                *rgb_min = rgb_bin.colors[0].Maximum;
                *(rgb_min + 1) = rgb_bin.colors[1].Maximum;
                *(rgb_min + 2) = rgb_bin.colors[2].Maximum;
                byte* lab_max = stackalloc byte[3];
                RGBLAB.ToLAB(rgb_max, lab_max);

                List<Range<byte>> ranges = new List<Range<byte>> { new Range<byte>(*lab_min, *lab_max), 
                                                                   new Range<byte>(*(lab_min + 1), *(lab_max + 1)), 
                                                                   new Range<byte>(*(lab_min + 2), *(lab_max + 2)) };
                lab_bins.Add(new ColorBins(ranges));
            }

            return lab_bins;
        }

        public List<int> calcFCH(Image<Bgr, Byte> img)
        {
            int cch_bins = n_;
            int fch_bins = n;
            int dimension = 3;
            DenseHistogram cchHist = calcRGB(img, cch_bins);

            List<ColorBins> rgb_bins = getColorBins(cch_bins, dimension);
            List<ColorBins> lab_bins = convertLAB(rgb_bins);

            int cch_bins_total = (int)Math.Pow(cch_bins, dimension);
            int fch_bins_total = (int)Math.Pow(fch_bins, dimension);
            FuzzyCMeans fcm = new FuzzyCMeans(lab_bins, m, e, fch_bins_total);
            fcm.runFCM();
            List<CalcBins> cluster = fcm.cluster;
            double[,] matrix = fcm.matrix_u;

            List<int> fch = new List<int>(fch_bins_total);
            for (int j = 0; j < fch_bins_total; j++)
            {
                double sum = 0;
                for (int i = 0; i < cch_bins_total; i++)
                {
                    sum += cchHist[i] * matrix[i, j];
                }
                fch.Add((int)sum);
            }

            return fch;
        }

        public Image<Bgr, Byte> GenerateRGBHistImage(List<int> hist)
        {
            int width = 480;
            int widthOffset = 20;
            int height = 640;

            int minValue = hist.Min();
            int maxValue = hist.Max();

            Image<Bgr, Byte> imageHist = new Image<Bgr, Byte>(width, height, new Bgr(255d, 255d, 255d));
            double heightPerTick = 1d * height / maxValue;
            double widthPerTick = 1d * (width - widthOffset * 2) / hist.Count;
            Bgr color = new Bgr(0d, 0d, 0d);

            for (int i = 0; i < hist.Count; i++)
            {
                int xPos = (int)(i * widthPerTick) + widthOffset;
                int yPos = (int)(height - heightPerTick * hist[i]);
                LineSegment2D line = new LineSegment2D(new Point(xPos, height), new Point(xPos, yPos));
                imageHist.Draw(line, color, 2);
            }

            return imageHist;
        }
    }
}
