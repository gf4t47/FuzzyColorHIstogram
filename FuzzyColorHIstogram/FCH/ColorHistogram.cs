using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace FuzzyColorHIstogram.FCH
{
    public class ColorHistogram
    {
        private DenseHistogram CalcRGB(Image<Bgr, Byte> img, int bins)
        {
            var hRange = new RangeF(0f, 255f);
            var imagesRGB = img.Split();

            var hist = new DenseHistogram(new[] { bins, bins, bins }, new[] { hRange, hRange, hRange });
            hist.Calculate(new IImage[] { imagesRGB[0], imagesRGB[1], imagesRGB[2] }, false, null);

            return hist;
        }

        private Image<Bgr, Byte> GenerateRGBHistImage(DenseHistogram hist, int bins)
        {
            const int width = 480;
            const int widthOffset = 20;
            const int height = 640;
            var binsCount = bins * bins * bins;

            float minValue, maxValue;
            int[] minLocations, maxLocations;
            hist.MinMax(out minValue, out maxValue, out minLocations, out maxLocations);

            var imageHist = new Image<Bgr, Byte>(width, height, new Bgr(255d, 255d, 255d));
            var heightPerTick = 1d * height / maxValue;
            var widthPerTick = 1d * (width - widthOffset * 2) / binsCount;
            var color = new Bgr(0d, 0d, 0d);

            for (var i = 0; i < binsCount; i++)
            {
                var xPos = (int)(i * widthPerTick) + widthOffset;
                var yPos = (int)(height - heightPerTick * hist[i]);
                var line = new LineSegment2D(new Point(xPos, height), new Point(xPos, yPos));
                imageHist.Draw(line, color, 2);
            }

            return imageHist;
        }

        public Image<Bgr, Byte> SaveRGB(Image<Bgr, Byte> img, int bins)
        {
            return GenerateRGBHistImage(CalcRGB(img, bins), bins);
        }
    }
}
