using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzyColorHIstogram;
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
        public FuzzyColorHistogramTest()
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
        public void TestConvertLab()
        {
            int NO = 6601;
            string path = "C:\\Users\\Ding\\SkyDrive\\Program\\ImageProcess\\Project\\FuzzyColorHIstogram\\FuzzyColorHistogramTest\\img";
            string file = "\\IMG_" + NO + ".jpg";
            string filename = path + file;
            Assert.IsTrue(File.Exists(filename));
            Image<Bgr, Byte> img = new Image<Bgr, byte>(filename);

            FuzzyColorHistogram fch = new FuzzyColorHistogram();
            Image<Lab, Byte> ret = fch.convert(img);

            Image<Gray, Byte>[] channel = ret.Split();

            ret.Save("Lab.jpg");
        }
    }
}
