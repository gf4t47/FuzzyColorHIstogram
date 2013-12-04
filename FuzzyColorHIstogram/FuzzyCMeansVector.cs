using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCH
{
    public class FuzzyCMeansVector
    {
        private double weight_exponetM;
        private double ERROE_TOLERANCE;
        private int cluster_num;
        private int input_dimension;

        private List<int[]> datas;
        public List<double[]> cluster
        {
            get;
            private set;
        }

        public double[,] matrix_u
        {
            get;
            private set;
        }

        public FuzzyCMeansVector(List<int[]> dts, double weight, double tolerance, int num)
        {
            datas = dts;
            input_dimension = datas[0].Length;

            weight_exponetM = weight;
            cluster_num = num;
            ERROE_TOLERANCE = tolerance;
        }

        private double[,] InitializeMatrix()
        {
            double[,] matrix_u = new double[datas.Count, cluster_num];

            Random rd = new Random();

            for (int x = 0; x < datas.Count; x++)
            {
                double maximum = 1;
                double minimum = 0;
                for (int c = 0; c < cluster_num; c++)
                {
                    if (c == cluster_num - 1)
                    {
                        matrix_u[x, c] = maximum;
                    }
                    else
                    {
                        matrix_u[x, c] = rd.NextDouble() * (maximum - minimum) + minimum;
                        maximum -= matrix_u[x, c];
                    }
                }
            }

            return matrix_u;
        }

        private void reset(ref double[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
        }

        private double[] mul(double p, int[] data)
        {
            double[] ret = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                ret[i] = p * data[i];
            }

            return ret;
        }

        private double[] plus(double[] l, double[] r)
        {
            int count = l.Length < r.Length ? l.Length : r.Length;

            double[] ret = new double[count];

            for (int i = 0; i < count; i++)
            {
                ret[i] = l[i] + r[i];
            }

            return ret;
        }

        private double[] plus(double[] l, double r)
        {
            int count = l.Length;

            double[] ret = new double[count];

            for (int i = 0; i < count; i++)
            {
                ret[i] = l[i] + r;
            }

            return ret;
        }

        private double[] divid(double[] n, double[] d)
        {
            int count = n.Length < d.Length ? n.Length : d.Length;

            double[] ret = new double[count];

            for (int i = 0; i < count; i++)
            {
                ret[i] = n[i] / d[i];
            }

            return ret;
        }

        private double diffVectorNorm(int[] x, double[] c)
        {
            int count = x.Length < c.Length ? x.Length : c.Length;
            double sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += Math.Pow(x[i] - c[i], 2);
            }

            return Math.Pow(sum, 0.5);
        }

        private double diffMatrixNorm(double[,] pre, double[,] cur)
        {
            double sum = 0;

            for (int x = 0; x < datas.Count; x++)
            {
                for (int c = 0; c < cluster_num; c++)
                {
                    sum += Math.Pow(cur[x, c] - pre[x, c], 2);

                }
            }

            return Math.Pow(sum, 0.5);
        }

        private List<double[]> updateCluster(double[,] matrix_u)
        {
            List<double[]> cluster = new List<double[]>(cluster_num);

            for (int c = 0; c < cluster_num; c++)
            {
                double[] sum_numerator = new double[input_dimension];
                reset(ref sum_numerator);
                for (int x = 0; x < datas.Count; x++)
                {

                    sum_numerator = plus(sum_numerator, mul(Math.Pow(matrix_u[x, c], weight_exponetM), datas[x]));
                }

                double[] sum_denominator = new double[input_dimension];
                reset(ref sum_denominator);
                for (int x = 0; x < datas.Count; x++)
                {
                    sum_denominator = plus(sum_denominator, Math.Pow(matrix_u[x, c], weight_exponetM));
                }

                cluster.Add(divid(sum_numerator, sum_denominator));
            }

            return cluster;
        }

        private double[,] updateMatrix(List<double[]> cluster)
        {
            double[,] matrix_u = new double[datas.Count, cluster_num];

            for (int x = 0; x < datas.Count; x++)
            {
                for (int c = 0; c < cluster_num; c++)
                {
                    double sum = 0;

                    for (int k = 0; k < cluster_num; k++)
                    {
                        double numerator = diffVectorNorm(datas[x], cluster[c]);
                        double denominiator = diffVectorNorm(datas[x], cluster[k]);
                        sum += Math.Pow(numerator / denominiator, 2 / (weight_exponetM - 1));
                    }

                    matrix_u[x, c] = 1 / sum;
                }
            }

            return matrix_u;
        }

        private void fcmIter(double[,] preMatrix)
        {
            List<double[]> curCluster = updateCluster(preMatrix);
            double[,] curMatrix = updateMatrix(curCluster);

            double diff = diffMatrixNorm(preMatrix, curMatrix);
            if (diff < ERROE_TOLERANCE)
            {
                cluster = curCluster;
                matrix_u = curMatrix;
                return;
            }
            else
            {
                fcmIter(curMatrix);
            }
        }

        public void runFCM()
        {
            fcmIter(InitializeMatrix());
        }
    }
}
