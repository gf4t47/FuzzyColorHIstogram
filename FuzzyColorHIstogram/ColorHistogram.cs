using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace FuzzyColorHIstogram
{
    public class ColorHistogram
    {
        private float[] BlueHist;
        private float[] GreenHist;
        private float[] RedHist;

        public DenseHistogram calcRGB(Image<Bgr,Byte> img, int bins)
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

        public Image<Bgr, Byte> saveRGB(Image<Bgr,Byte> img, int bins)
        {
            return GenerateRGBHistImage(calcRGB(img, bins), bins);
        }

        public void calcHistogram(string filename)
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>(filename);
            DenseHistogram Histo = new DenseHistogram(255, new RangeF(0, 255));

            Image<Gray, Byte> img2Blue = img[0];
            Image<Gray, Byte> img2Green = img[1];
            Image<Gray, Byte> img2Red = img[2];


            Histo.Calculate(new Image<Gray, Byte>[] { img2Blue }, false, null);
            BlueHist = new float[256];
            Histo.MatND.ManagedArray.CopyTo(BlueHist, 0);
            Image<Bgr, Byte> blue = GenerateHistImage(Histo);
            blue.Save("blue.jpg");
            Histo.Clear();

            Histo.Calculate(new Image<Gray, Byte>[] { img2Green }, false, null);
            GreenHist = new float[256];
            Histo.MatND.ManagedArray.CopyTo(GreenHist, 0);
            Image<Bgr, Byte> green = GenerateHistImage(Histo);
            blue.Save("green.jpg");
            Histo.Clear();

            Histo.Calculate(new Image<Gray, Byte>[] { img2Red }, false, null);
            RedHist = new float[256];
            Histo.MatND.ManagedArray.CopyTo(RedHist, 0);
            Image<Bgr, Byte> red = GenerateHistImage(Histo);
            blue.Save("red.jpg");
            Histo.Clear();
        }

        public Image<Bgr, Byte> GenerateHistImage(DenseHistogram hist)
        {
            Image<Bgr, Byte> imageHist = null;
            float minValue, maxValue;
            int[] minLocations, maxLocations;
            hist.MinMax(out minValue, out maxValue, out minLocations, out maxLocations);

            if (hist.Dimension == 1)
            {
                int bins = hist.BinDimension[0].Size;
                int width = bins;
                int height = 300;
                imageHist = new Image<Bgr, Byte>(width, height, new Bgr(255d, 255d, 255d));
                double heightPerTick = 1d * height / maxValue;
                Bgr color = new Bgr(0d, 0d, 255d);

                for (int i = 0; i < bins; i++)
                {
                    LineSegment2D line = new LineSegment2D(new Point(i, height), new Point(i, (int)(height - heightPerTick * hist[i])));
                    imageHist.Draw(line, color, 1);
                }
            }
            else if (hist.Dimension == 2)
            {
                int scale = 2;
                int width = hist.BinDimension[0].Size * scale;
                int height = hist.BinDimension[1].Size * scale;
                imageHist = new Image<Bgr, Byte>(width, height, new Bgr(255d, 255d, 255d));

                for (int i = 0; i < width / scale; i++)
                {
                    for (int j = 0; j < height / scale; j++)
                    {
                        double binValue = hist[i, j];
                        double intensity = 1d * binValue * 255 / maxValue;
                        Rectangle rect = new Rectangle(i * scale, j * scale, 1, 1);
                        Bgr color = new Bgr(intensity, intensity, intensity);
                        imageHist.Draw(rect, color, 1);
                    }
                }
            }
            else if(hist.Dimension == 3)
            {
                int bins = hist.BinDimension[0].Size;
                int width = 300;
                int height = 300;
                imageHist = new Image<Bgr, Byte>(width, height, new Bgr(255d, 255d, 255d));
                double heightPerTick = 1d * height / maxValue;
                Bgr color = new Bgr(0d, 0d, 255d);

                for (int i = 0; i < bins; i++)
                {
                    LineSegment2D line = new LineSegment2D(new Point(i, height), new Point(i, (int)(height - heightPerTick * hist[i])));
                    imageHist.Draw(line, color, 1);
                }
            }

            return imageHist;
        }
    }
}
