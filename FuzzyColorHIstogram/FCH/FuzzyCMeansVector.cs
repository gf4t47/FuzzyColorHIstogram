using System;
using System.Collections.Generic;

namespace FuzzyColorHIstogram.FCH
{
    public class FuzzyCMeansVector
    {
        private readonly double _weightExponetM;
        private readonly double _erroeTolerance;
        private readonly int _clusterNum;
        private readonly int _inputDimension;

        private readonly List<int[]> _datas;
        public List<double[]> Cluster
        {
            get;
            private set;
        }

        public double[,] MatrixU
        {
            get;
            private set;
        }

        public FuzzyCMeansVector(List<int[]> dts, double weight, double tolerance, int num)
        {
            _datas = dts;
            _inputDimension = _datas[0].Length;

            _weightExponetM = weight;
            _clusterNum = num;
            _erroeTolerance = tolerance;
        }

        private double[,] InitializeMatrix()
        {
            var matrixU = new double[_datas.Count, _clusterNum];

            var rd = new Random();

            for (var x = 0; x < _datas.Count; x++)
            {
                var maximum = 1.0;
                const double minimum = 0;
                for (var c = 0; c < _clusterNum; c++)
                {
                    if (c == _clusterNum - 1)
                    {
                        matrixU[x, c] = maximum;
                    }
                    else
                    {
                        matrixU[x, c] = rd.NextDouble() * (maximum - minimum) + minimum;
                        maximum -= matrixU[x, c];
                    }
                }
            }

            return matrixU;
        }

        private static void Reset(ref double[] data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
        }

        private static double[] Mul(double p, IList<int> data)
        {
            var ret = new double[data.Count];
            for (var i = 0; i < data.Count; i++)
            {
                ret[i] = p * data[i];
            }

            return ret;
        }

        private double[] Plus(IList<double> l, IList<double> r)
        {
            var count = l.Count < r.Count ? l.Count : r.Count;

            var ret = new double[count];

            for (var i = 0; i < count; i++)
            {
                ret[i] = l[i] + r[i];
            }

            return ret;
        }

        private static double[] Plus(IList<double> l, double r)
        {
            var count = l.Count;

            var ret = new double[count];

            for (var i = 0; i < count; i++)
            {
                ret[i] = l[i] + r;
            }

            return ret;
        }

        private static double[] Divid(IList<double> n, IList<double> d)
        {
            var count = n.Count < d.Count ? n.Count : d.Count;

            var ret = new double[count];

            for (var i = 0; i < count; i++)
            {
                ret[i] = n[i] / d[i];
            }

            return ret;
        }

        private double diffVectorNorm(IList<int> x, IList<double> c)
        {
            var count = x.Count < c.Count ? x.Count : c.Count;
            double sum = 0;
            for (var i = 0; i < count; i++)
            {
                sum += Math.Pow(x[i] - c[i], 2);
            }

            return Math.Pow(sum, 0.5);
        }

        private double DiffMatrixNorm(double[,] pre, double[,] cur)
        {
            double sum = 0;

            for (var x = 0; x < _datas.Count; x++)
            {
                for (var c = 0; c < _clusterNum; c++)
                {
                    sum += Math.Pow(cur[x, c] - pre[x, c], 2);

                }
            }

            return Math.Pow(sum, 0.5);
        }

        private List<double[]> UpdateCluster(double[,] matrixU)
        {
            var cluster = new List<double[]>(_clusterNum);

            for (var c = 0; c < _clusterNum; c++)
            {
                var sumNumerator = new double[_inputDimension];
                Reset(ref sumNumerator);
                for (var x = 0; x < _datas.Count; x++)
                {

                    sumNumerator = Plus(sumNumerator, Mul(Math.Pow(matrixU[x, c], _weightExponetM), _datas[x]));
                }

                var sumDenominator = new double[_inputDimension];
                Reset(ref sumDenominator);
                for (var x = 0; x < _datas.Count; x++)
                {
                    sumDenominator = Plus(sumDenominator, Math.Pow(matrixU[x, c], _weightExponetM));
                }

                cluster.Add(Divid(sumNumerator, sumDenominator));
            }

            return cluster;
        }

        private double[,] UpdateMatrix(IReadOnlyList<double[]> cluster)
        {
            var matrixU = new double[_datas.Count, _clusterNum];

            for (var x = 0; x < _datas.Count; x++)
            {
                for (var c = 0; c < _clusterNum; c++)
                {
                    double sum = 0;

                    for (var k = 0; k < _clusterNum; k++)
                    {
                        var numerator = diffVectorNorm(_datas[x], cluster[c]);
                        var denominiator = diffVectorNorm(_datas[x], cluster[k]);
                        sum += Math.Pow(numerator / denominiator, 2 / (_weightExponetM - 1));
                    }

                    matrixU[x, c] = 1 / sum;
                }
            }

            return matrixU;
        }

        private void FCMIter(double[,] preMatrix)
        {
            var curCluster = UpdateCluster(preMatrix);
            var curMatrix = UpdateMatrix(curCluster);

            var diff = DiffMatrixNorm(preMatrix, curMatrix);
            if (diff < _erroeTolerance)
            {
                Cluster = curCluster;
                MatrixU = curMatrix;
            }
            else
            {
                FCMIter(curMatrix);
            }
        }

        public void RunFCM()
        {
            FCMIter(InitializeMatrix());
        }
    }
}
