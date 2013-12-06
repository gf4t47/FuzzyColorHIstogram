using System;
using System.Collections.Generic;
using FuzzyColorHIstogram.FCH;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzyColorHistogramTest
{
    /// <summary>
    /// Summary description for FcmTest
    /// </summary>
    [TestClass]
    public class FcmTest
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFcmColorBins()
        {
            const int bins = 16;
            const int tick = 256 / 16;
            const int dimension = 3;

            var datas = new List<ColorBins>(bins);
            for (var i = 0; i < bins; i++)
            {
                var ranges = new List<Range<Byte>>(dimension);
                for (var j = 0; j < 3; j++)
                {
                    ranges.Add(new Range<Byte>((Byte)(i * tick), (Byte)((i + 1) * tick - 1)));
                }

                datas.Add(new ColorBins(ranges));
            }

            var fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.RunFCM();

            var cluster = fcm.Cluster;
            var matrix = fcm.MatrixU;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }
    }
}
