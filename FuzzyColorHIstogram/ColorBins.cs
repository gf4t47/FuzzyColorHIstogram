using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCH
{
    public class ColorBins
    {
        public ColorBins(List<Range<Byte>> cs)
        {
            colors = cs;
        }

        public ColorBins(int count)
        {
            colors = new List<Range<Byte>>();
            for (int i = 0; i < count; i++)
            {
                colors.Add(new Range<Byte>(0, 0));
            }
        }

        public List<Range<Byte>> colors { get; set; }

        public int Count
        {
            get { return colors.Count; }
        }

        public CalcBins mul(double fac)
        {
            List<Range<double>> ranges = new List<Range<double>>();
            foreach (Range<Byte> c in colors)
            {
                ranges.Add(new Range<double>(c.Minimum * fac, c.Maximum * fac));
            }

            return new CalcBins(ranges);
        }

        //public ColorBins plus(ColorBins other)
        //{
        //    int num = Count < other.Count ? Count : other.Count;

        //    List<Range<Byte>> ranges = new List<Range<Byte>>();
        //    for (int i = 0; i < num; i++)
        //    {
        //        ranges.Add(new Range<Byte>(colors[i].Minimum + other.colors[i].Minimum, colors[i].Maximum + other.colors[i].Maximum));
        //    }

        //    return new ColorBins(ranges);
        //}

        //public ColorBins plus(double p)
        //{
        //    List<Range<Byte>> ranges = new List<Range<Byte>>();

        //    foreach (Range<Byte> c in colors)
        //    {
        //        ranges.Add(new Range<Byte>((int)(c.Minimum + p), (int)(c.Maximum + p)));
        //    }

        //    return new ColorBins(ranges);
        //}

        //public ColorBins divid(ColorBins other)
        //{
        //    int num = Count < other.Count ? Count : other.Count;

        //    List<Range<Byte>> ranges = new List<Range<Byte>>();
        //    for (int i = 0; i < num; i++)
        //    {
        //        ranges.Add(new Range<Byte>(colors[i].Minimum / other.colors[i].Minimum, colors[i].Maximum / other.colors[i].Maximum));
        //    }

        //    return new ColorBins(ranges);
        //}

        public double diff(CalcBins other)
        {
            int num = Count < other.Count ? Count : other.Count;

            double sum = 0;
            for (int i = 0; i < num; i++)
            {
                sum += Math.Pow(colors[i].Minimum - other.colors[i].Minimum, 2) + Math.Pow(colors[i].Maximum - other.colors[i].Maximum, 2);
            }

            return Math.Pow(sum, 0.5);
        }
    }
}
