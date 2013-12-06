using Emgu.CV;
using Emgu.CV.Structure;
using FuzzyColorHIstogram.FCH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FuzzyColorHistogramTest
{
    /// <summary>
    /// Summary description for ColorHistogramTest
    /// </summary>
    [TestClass]
    public class ColorHistogramTest
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
        public void TestColorHistogram1()
        {
            const int no = 1;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var ch = new ColorHistogram();
            var ret = ch.SaveRGB(img, 4);
            ret.Save("testImg1.jpg");
        }

        [TestMethod]
        public void TestColorHistogram2()
        {
            const int no = 2;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var ch = new ColorHistogram();
            var ret = ch.SaveRGB(img, 4);
            ret.Save("testImg2.jpg");
        }

        [TestMethod]
        public void TestColorHistogram6703()
        {
            const int no = 6703;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var ch = new ColorHistogram();
            var ret = ch.SaveRGB(img, 4);
            ret.Save("testImg6703.jpg");
        }

        [TestMethod]
        public void TestColorHistogram6705()
        {
            const int no = 6705;
            const string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            var file = "\\IMG_" + no + ".jpg";
            var filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            var img = new Image<Bgr, byte>(filename);

            var ch = new ColorHistogram();
            var ret = ch.SaveRGB(img, 4);
            ret.Save("testImg6705.jpg");
        }
    }
}
