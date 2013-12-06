using FuzzyColorHIstogram.FCH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FuzzyColorHistogramTest
{
    [TestClass]
    public class FcmVectorTest
    {
        [TestMethod]
        public void TestFcmArray1()
        {
            var datas = new List<int[]>
            {
                new[] {2},
                new[] {3},
                new[] {4},
                new[] {5},
                new[] {23},
                new[] {24},
                new[] {25},
                new[] {26},
                new[] {55},
                new[] {56},
                new[] {57},
                new[] {58}
            };
            var fcm = new FuzzyCMeansVector(datas, 1.8, 0.25, 3);

            fcm.RunFCM();

            var cluster = fcm.Cluster;
            var matrix = fcm.MatrixU;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray2()
        {
            var datas = new List<int[]>
            {
                new[] {2, 2},
                new[] {3, 3},
                new[] {4, 4},
                new[] {5, 5},
                new[] {23, 23},
                new[] {24, 24},
                new[] {25, 25},
                new[] {26, 26},
                new[] {55, 55},
                new[] {56, 56},
                new[] {57, 57},
                new[] {58, 58}
            };
            var fcm = new FuzzyCMeansVector(datas, 1.8, 0.25, 3);

            fcm.RunFCM();

            var cluster = fcm.Cluster;
            var matrix = fcm.MatrixU;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray3()
        {
            var datas = new List<int[]>
            {
                new[] {2, 2, 2},
                new[] {3, 3, 3},
                new[] {4, 4, 4},
                new[] {5, 5, 5},
                new[] {23, 23, 23},
                new[] {24, 24, 24},
                new[] {25, 25, 25},
                new[] {26, 26, 26},
                new[] {55, 55, 55},
                new[] {56, 56, 56},
                new[] {57, 57, 57},
                new[] {58, 58, 58}
            };
            
            var fcm = new FuzzyCMeansVector(datas, 1.8, 0.25, 3);

            fcm.RunFCM();

            var cluster = fcm.Cluster;
            var matrix = fcm.MatrixU;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray3_1()
        {
            var datas = new List<int[]>
            {
                new[] {2, 23, 55},
                new[] {3, 24, 56},
                new[] {4, 25, 57},
                new[] {5, 26, 58},
                new[] {23, 55, 2},
                new[] {24, 56, 3},
                new[] {25, 57, 4},
                new[] {26, 58, 5},
                new[] {55, 2, 23},
                new[] {56, 3, 24},
                new[] {57, 4, 25},
                new[] {58, 5, 26}
            };

            var fcm = new FuzzyCMeansVector(datas, 1.8, 0.25, 3);

            fcm.RunFCM();

            var cluster = fcm.Cluster;
            var matrix = fcm.MatrixU;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray3_2()
        {
            var datas = new List<int[]>
            {
                new int[] {2, 23, 23},
                new[] {3, 24, 56},
                new[] {4, 25, 57},
                new[] {15, 26, 58},
                new[] {23, 55, 2},
                new[] {24, 56, 34},
                new[] {25, 57, 4},
                new[] {26, 58, 5},
                new[] {55, 2, 23},
                new[] {56, 3, 24},
                new[] {57, 42, 25},
                new[] {58, 51, 26}
            };

            var fcm = new FuzzyCMeansVector(datas, 1.8, 0.25, 3);

            fcm.RunFCM();

            var cluster = fcm.Cluster;
            var matrix = fcm.MatrixU;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }
    }
}
