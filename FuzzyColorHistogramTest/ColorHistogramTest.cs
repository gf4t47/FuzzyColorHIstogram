using Emgu.CV;
using Emgu.CV.Structure;
using FuzzyColorHIstogram;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FuzzyColorHistogramTest
{
    /// <summary>
    /// Summary description for ColorHistogramTest
    /// </summary>
    [TestClass]
    public class ColorHistogramTest
    {
        public ColorHistogramTest()
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
        public void TestColorHistogram1()
        {
            int NO = 6601;
            string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            string file = "\\IMG_" + NO + ".jpg";
            string filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            Image<Bgr, Byte> img = new Image<Bgr, byte>(filename);

            ColorHistogram ch = new ColorHistogram();
            Image<Bgr, Byte> ret = ch.saveRGB(img, 4);
            ret.Save("testImg1.jpg");
        }

        [TestMethod]
        public void TestColorHistogram2()
        {
            int NO = 6602;
            string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            string file = "\\IMG_" + NO + ".jpg";
            string filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            Image<Bgr, Byte> img = new Image<Bgr, byte>(filename);

            ColorHistogram ch = new ColorHistogram();
            Image<Bgr, Byte> ret = ch.saveRGB(img, 4);
            ret.Save("testImg2.jpg");
        }
    }
}
