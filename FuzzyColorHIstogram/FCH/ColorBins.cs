using System;
using System.Collections.Generic;
using System.Linq;
using FCH;

namespace FuzzyColorHIstogram.FCH
{
    public class ColorBins
    {
        public ColorBins(List<Range<byte>> cs)
        {
            Colors = cs;
        }

        public List<Range<Byte>> Colors { get; private set; }

        public int Count
        {
            get { return Colors.Count; }
        }

        public CalcBins Mul(double fac)
        {
            var ranges = Colors.Select(c => new Range<double>(c.Minimum*fac, c.Maximum*fac)).ToList();

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

        public double Diff(CalcBins other)
        {
            int num = Count < other.Count ? Count : other.Count;

            double sum = 0;
            for (int i = 0; i < num; i++)
            {
                sum += Math.Pow(Colors[i].Minimum - other.Colors[i].Minimum, 2) + Math.Pow(Colors[i].Maximum - other.Colors[i].Maximum, 2);
            }

            return Math.Pow(sum, 0.5);
        }
    }
}
