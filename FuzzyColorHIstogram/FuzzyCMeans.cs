using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCH
{
    public class FuzzyCMeans
    {
        private double weight_exponetM;
        private double ERROE_TOLERANCE;
        private int cluster_num;
        private int input_dimension;

        private List<ColorBins> datas;
        public List<CalcBins> cluster
        {
            get;
            private set;
        }

        public double[,] matrix_u
        {
            get;
            private set;
        }

        public FuzzyCMeans(List<ColorBins> dts, double weight, double tolerance, int num)
        {
            datas = dts;
            input_dimension = datas[0].Count;

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
                    if(c == cluster_num -1)
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

        private List<CalcBins> updateCluster(double[,] matrix_u)
        {
            List<CalcBins> cluster = new List<CalcBins>(cluster_num);

            for (int c = 0; c < cluster_num; c++)
            {
                CalcBins sum_numerator = new CalcBins(input_dimension);
                for (int x = 0; x < datas.Count; x++)
                {
                    sum_numerator = sum_numerator.plus(datas[x].mul(Math.Pow(matrix_u[x, c], weight_exponetM)));
                }

                CalcBins sum_denominator = new CalcBins(input_dimension);
                for (int x = 0; x < datas.Count; x++)
                {
                    sum_denominator = sum_denominator.plus(Math.Pow(matrix_u[x, c], weight_exponetM));
                }

                cluster.Add(sum_numerator.divid(sum_denominator));
            }

            return cluster;
        }

        private double[,] updateMatrix(List<CalcBins> cluster)
        {
            double[,] matrix_u = new double[datas.Count, cluster_num];

            for (int x = 0; x < datas.Count; x++)
            {
                for (int c = 0; c < cluster_num; c++)
                {
                    double sum = 0;

                    for (int k = 0; k < cluster_num; k++)
                    {
                        double numerator = datas[x].diff(cluster[c]);
                        double denominiator = datas[x].diff(cluster[k]);
                        sum += Math.Pow(numerator / denominiator, 2 / (weight_exponetM - 1));
                    }

                    matrix_u[x, c] = 1 / sum;
                }
            }

            return matrix_u;
        }

        private void fcmIter(double[,] preMatrix)
        {
            List<CalcBins> curCluster = updateCluster(preMatrix);
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
