using System;
using System.Collections.Generic;

namespace FuzzyColorHIstogram.FCH
{
    public class FuzzyCMeans
    {
        private readonly double _weightExponetM;
        private readonly double _erroeTolerance;
        private readonly int _clusterNum;
        private readonly int _inputDimension;

        private readonly List<ColorBins> _datas;
        public List<CalcBins> Cluster
        {
            get;
            private set;
        }

        public double[,] MatrixU
        {
            get;
            private set;
        }

        public FuzzyCMeans(List<ColorBins> dts, double weight, double tolerance, int num)
        {
            _datas = dts;
            _inputDimension = _datas[0].Count;

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
                    if(c == _clusterNum -1)
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

        private List<CalcBins> UpdateCluster(double[,] matrixU)
        {
            var cluster = new List<CalcBins>(_clusterNum);

            for (var c = 0; c < _clusterNum; c++)
            {
                var sumNumerator = new CalcBins(_inputDimension);
                for (var x = 0; x < _datas.Count; x++)
                {
                    sumNumerator = sumNumerator.Plus(_datas[x].Mul(Math.Pow(matrixU[x, c], _weightExponetM)));
                }

                var sumDenominator = new CalcBins(_inputDimension);
                for (int x = 0; x < _datas.Count; x++)
                {
                    sumDenominator = sumDenominator.Plus(Math.Pow(matrixU[x, c], _weightExponetM));
                }

                cluster.Add(sumNumerator.Divid(sumDenominator));
            }

            return cluster;
        }

        private double[,] UpdateMatrix(List<CalcBins> cluster)
        {
            var matrixU = new double[_datas.Count, _clusterNum];

            for (var x = 0; x < _datas.Count; x++)
            {
                for (var c = 0; c < _clusterNum; c++)
                {
                    var sum = 0d;

                    for (var k = 0; k < _clusterNum; k++)
                    {
                        var numerator = _datas[x].Diff(cluster[c]);
                        var denominiator = _datas[x].Diff(cluster[k]);
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
