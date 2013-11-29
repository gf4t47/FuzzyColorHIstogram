using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyColorHIstogram
{
    public class FuzzyCMeans
    {
        private double weight_exponetM;
        private double ERROE_TOLERANCE;
        private int cluster_num;

        private int[] datas;
        private double[] cluster;
        private double[,] matrix_u;

        public FuzzyCMeans(int[] dts, double weight, double tolerance, int num)
        {
            datas = dts;
            weight_exponetM = weight;
            cluster_num = num;
            ERROE_TOLERANCE = tolerance;
        }

        private double[,] InitializeMatrix()
        {
            double[,] matrix_u = new double[datas.Length, cluster_num];

            int cluster_sum = 0;
            for (int c = 1; c <= cluster_num; c++)
            {
                cluster_sum += c;
            }

            for (int x = 0; x < datas.Length; x++)
            {
                for (int c = 0; c < cluster_num; c++)
                {
                    matrix_u[x, c] = (double)(c + 1) / (double)cluster_sum;
                }
            }

            return matrix_u;
        }

        private double diffVectorNorm(double x, double c)
        {
            return Math.Abs(x - c);
        }

        private double diffMatrixNorm(double[,] pre, double[,] cur)
        {
            double sum = 0;

            for (int x = 0; x < datas.Length; x++)
            {
                for (int c = 0; c < cluster_num; c++)
                {
                    sum += Math.Pow(cur[x, c] - pre[x, c], 2);

                }
            }

            return Math.Pow(sum, 1 / 2);
        }

        private double[] updateCluster(double[,] matrix_u)
        {
            double[] cluster = new double[cluster_num];

            for (int c = 0; c < cluster_num; c++)
            {
                double sum_numerator = 0;
                for (int x = 0; x < datas.Length; x++)
                {
                    sum_numerator += Math.Pow(matrix_u[x, c], weight_exponetM) * datas[x];
                }

                double sum_denominator = 0;
                for (int x = 0; x < datas.Length; x++)
                {
                    sum_denominator += Math.Pow(matrix_u[x, c], weight_exponetM);
                }

                cluster[c] = sum_numerator / sum_denominator;
            }

            return cluster;
        }

        private double[,] updateMatrix(double[] cluster)
        {
            double[,] matrix_u = new double[datas.Length, cluster_num];

            for (int x = 0; x < datas.Length; x++)
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
            double[] curCluster = updateCluster(preMatrix);
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
