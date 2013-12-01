using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;


namespace FuzzyColorHIstogram
{
    public class FuzzyColorHistogram
    {
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

        public void calcFCH(Image<Bgr, Byte> img)
        {
            DenseHistogram chHist = calcRGB(img, 16);
            Image<Lab, Byte> labImg = convert(img);
        }
    }
}
