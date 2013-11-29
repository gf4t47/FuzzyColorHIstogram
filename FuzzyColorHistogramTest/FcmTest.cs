using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzyColorHIstogram;

namespace FuzzyColorHistogramTest
{
    [TestClass]
    public class FcmTest
    {
        [TestMethod]
        public void TestFCM()
        {
            int[] datas = { 2, 3, 4, 5, 23, 24, 25, 26, 55, 56, 57, 58};
            FuzzyCMeans fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.runFCM();
        }
    }
}
