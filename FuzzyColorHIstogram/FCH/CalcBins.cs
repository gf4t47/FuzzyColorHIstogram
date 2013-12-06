using System.Collections.Generic;
using System.Linq;
using FCH;

namespace FuzzyColorHIstogram.FCH
{
    public class CalcBins
    {
        public CalcBins(List<Range<double>> cs)
        {
            Colors = cs;
        }

        public CalcBins(int count)
        {
            Colors = new List<Range<double>>();
            for (int i = 0; i < count; i++)
            {
                Colors.Add(new Range<double>(0, 0));
            }
        }

        public List<Range<double>> Colors { get; private set; }

        public int Count
        {
            get { return Colors.Count; }
        }

        //public CalcBins mul(double fac)
        //{
        //    List<Range<double>> ranges = new List<Range<double>>();
        //    foreach (Range<double> c in colors)
        //    {
        //        ranges.Add(new Range<double>(c.Minimum * fac, c.Maximum * fac));
        //    }

        //    return new CalcBins(ranges);
        //}

        public CalcBins Plus(CalcBins other)
        {
            int num = Count < other.Count ? Count : other.Count;

            var ranges = new List<Range<double>>();
            for (int i = 0; i < num; i++)
            {
                ranges.Add(new Range<double>(Colors[i].Minimum + other.Colors[i].Minimum, Colors[i].Maximum + other.Colors[i].Maximum));
            }

            return new CalcBins(ranges);
        }

        public CalcBins Plus(double p)
        {
            var ranges = Colors.Select(c => new Range<double>(c.Minimum + p, c.Maximum + p)).ToList();

            return new CalcBins(ranges);
        }

        public CalcBins Divid(CalcBins other)
        {
            var num = Count < other.Count ? Count : other.Count;

            var ranges = new List<Range<double>>();
            for (var i = 0; i < num; i++)
            {
                ranges.Add(new Range<double>(Colors[i].Minimum / other.Colors[i].Minimum, Colors[i].Maximum / other.Colors[i].Maximum));
            }

            return new CalcBins(ranges);
        }

        //public double diff(CalcBins other)
        //{
        //    int num = Count < other.Count ? Count : other.Count;

        //    double sum = 0;
        //    for (int i = 0; i < num; i++)
        //    {
        //        sum += Math.Pow(colors[i].Minimum - other.colors[i].Minimum, 2) + Math.Pow(colors[i].Maximum - other.colors[i].Maximum, 2);
        //    }

        //    return Math.Pow(sum, 0.5);
        //}
    }
}
