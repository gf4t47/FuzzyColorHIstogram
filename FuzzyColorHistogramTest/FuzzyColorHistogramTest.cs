using System;
using FuzzyColorHIstogram.FCH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;

namespace FuzzyColorHistogramTest
{
    /// <summary>
    /// Summary description for FuzzyColorHistogramTest
    /// </summary>
    [TestClass]
    public class FuzzyColorHistogramTest
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
        public void TestConvertLab()
        {
            const int no = 1;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var fch = new FuzzyColorHistogram();
            var ret = fch.Convert(img);

            var channel = ret.Split();
            if (channel == null) throw new ArgumentNullException("chan" + "nel");

            ret.Save("Lab.jpg");
        }

        [TestMethod]
        public void TestFCH1()
        {
            const int no = 1;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var fch = new FuzzyColorHistogram();
            var ret = fch.GenerateRGBHistImage(fch.CalcFCH(img));
            ret.Save("testFCH1.jpg");
        }

        [TestMethod]
        public void TestFCH2()
        {
            const int no = 2;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var fch = new FuzzyColorHistogram();
            var ret = fch.GenerateRGBHistImage(fch.CalcFCH(img));
            ret.Save("testFCH2.jpg");
        }

        [TestMethod]
        public void TestFCH6703()
        {
            const int no = 6703;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var fch = new FuzzyColorHistogram();
            var ret = fch.GenerateRGBHistImage(fch.CalcFCH(img));
            ret.Save("testFCH6703.jpg");
        }

        [TestMethod]
        public void TestFCH6705()
        {
            const int no = 6705;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var fch = new FuzzyColorHistogram();
            var ret = fch.GenerateRGBHistImage(fch.CalcFCH(img));
            ret.Save("testFCH6705.jpg");
        }
    }
}
