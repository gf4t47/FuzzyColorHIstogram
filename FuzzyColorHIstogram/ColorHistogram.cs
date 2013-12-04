using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace FCH
{
    public class ColorHistogram
    {
        private float[] BlueHist;
        private float[] GreenHist;
        private float[] RedHist;

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

        public Image<Bgr, Byte> GenerateRGBHistImage(DenseHistogram hist, int bins)
        {
            int width = 480;
            int widthOffset = 20;
            int height = 640;
            int binsCount = bins * bins * bins;

            float minValue, maxValue;
            int[] minLocations, maxLocations;
            hist.MinMax(out minValue, out maxValue, out minLocations, out maxLocations);

            Image<Bgr, Byte> imageHist = new Image<Bgr, Byte>(width, height, new Bgr(255d, 255d, 255d));
            double heightPerTick = 1d * height / maxValue;
            double widthPerTick = 1d * (width - widthOffset * 2) / binsCount;
            Bgr color = new Bgr(0d, 0d, 0d);

            for (int i = 0; i < binsCount; i++)
            {
                int xPos = (int)(i * widthPerTick) + widthOffset;
                int yPos = (int)(height - heightPerTick * hist[i]);
                LineSegment2D line = new LineSegment2D(new Point(xPos, height), new Point(xPos, yPos));
                imageHist.Draw(line, color, 2);
            }

            return imageHist;
        }

        public Image<Bgr, Byte> saveRGB(Image<Bgr, Byte> img, int bins)
        {
            return GenerateRGBHistImage(calcRGB(img, bins), bins);
        }
    }
}
