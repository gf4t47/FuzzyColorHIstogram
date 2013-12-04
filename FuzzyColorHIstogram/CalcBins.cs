using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCH
{
    public class CalcBins
    {
        public CalcBins(List<Range<double>> cs)
        {
            colors = cs;
        }

        public CalcBins(int count)
        {
            colors = new List<Range<double>>();
            for (int i = 0; i < count; i++)
            {
                colors.Add(new Range<double>(0, 0));
            }
        }

        public List<Range<double>> colors { get; set; }

        public int Count
        {
            get { return colors.Count; }
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

        public CalcBins plus(CalcBins other)
        {
            int num = Count < other.Count ? Count : other.Count;

            List<Range<double>> ranges = new List<Range<double>>();
            for (int i = 0; i < num; i++)
            {
                ranges.Add(new Range<double>(colors[i].Minimum + other.colors[i].Minimum, colors[i].Maximum + other.colors[i].Maximum));
            }

            return new CalcBins(ranges);
        }

        public CalcBins plus(double p)
        {
            List<Range<double>> ranges = new List<Range<double>>();

            foreach (Range<double> c in colors)
            {
                ranges.Add(new Range<double>(c.Minimum + p, c.Maximum + p));
            }

            return new CalcBins(ranges);
        }

        public CalcBins divid(CalcBins other)
        {
            int num = Count < other.Count ? Count : other.Count;

            List<Range<double>> ranges = new List<Range<double>>();
            for (int i = 0; i < num; i++)
            {
                ranges.Add(new Range<double>(colors[i].Minimum / other.colors[i].Minimum, colors[i].Maximum / other.colors[i].Maximum));
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
