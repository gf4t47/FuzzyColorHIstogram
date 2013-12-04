using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FCH;

namespace FuzzyColorHistogramTest
{
    /// <summary>
    /// Summary description for FcmTest
    /// </summary>
    [TestClass]
    public class FcmTest
    {
        public FcmTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
            int bins = 16;
            int tick = 256 / 16;
            int dimension = 3;

            List<ColorBins> datas = new List<ColorBins>(bins);
            for (int i = 0; i < bins; i++)
            {
                List<Range<Byte>> ranges = new List<Range<Byte>>(dimension);
                for (int j = 0; j < 3; j++)
                {
                    ranges.Add(new Range<Byte>((Byte)(i * tick), (Byte)((i + 1) * tick - 1)));
                }

                datas.Add(new ColorBins(ranges));
            }

            FuzzyCMeans fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.runFCM();

            List<CalcBins> cluster = fcm.cluster;
            double[,] matrix = fcm.matrix_u;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }
    }
}
