using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzyColorHIstogram;
using System.Collections.Generic;

namespace FuzzyColorHistogramTest
{
    [TestClass]
    public class FcmTest
    {
        [TestMethod]
        public void TestFcmArray1()
        {
            List<int[]> datas = new List<int[]>() { new int[] { 2 }, new int[] { 3 }, new int[] { 4 }, new int[] { 5 }, 
                                                    new int[]{ 23 }, new int[]{ 24 }, new int[]{ 25 }, new int[]{ 26 }, 
                                                    new int[]{ 55 }, new int[]{ 56 }, new int[]{ 57 }, new int[]{ 58 } };
            FuzzyCMeans fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.runFCM();

            List<double[]> cluster = fcm.cluster;
            double[,] matrix = fcm.matrix_u;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray2()
        {
            List<int[]> datas = new List<int[]>() { new int[] { 2,2 }, new int[] { 3,3 }, new int[] { 4,4 }, new int[] { 5,5 }, 
                                                    new int[]{ 23,23 }, new int[]{ 24,24 }, new int[]{ 25,25 }, new int[]{ 26,26 }, 
                                                    new int[]{ 55,55 }, new int[]{ 56,56 }, new int[]{ 57,57 }, new int[]{ 58,58 } };
            FuzzyCMeans fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.runFCM();

            List<double[]> cluster = fcm.cluster;
            double[,] matrix = fcm.matrix_u;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray3()
        {
            List<int[]> datas = new List<int[]>() { new int[] { 2,2,2 }, new int[] { 3,3,3 }, new int[] { 4,4,4 }, new int[] { 5,5,5 }, 
                                                    new int[]{ 23,23,23 }, new int[]{ 24,24,24 }, new int[]{ 25,25,25 }, new int[]{ 26,26,26 }, 
                                                    new int[]{ 55,55,55 }, new int[]{ 56,56,56 }, new int[]{ 57,57,57 }, new int[]{ 58,58,58 } };
            FuzzyCMeans fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.runFCM();

            List<double[]> cluster = fcm.cluster;
            double[,] matrix = fcm.matrix_u;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray3_1()
        {
            List<int[]> datas = new List<int[]>() { new int[] { 2,23,55 }, new int[] { 3,24,56 }, new int[] { 4,25,57 }, new int[] { 5,26,58 }, 
                                                    new int[]{ 23,55,2 }, new int[]{ 24,56,3 }, new int[]{ 25,57,4 }, new int[]{ 26,58,5 }, 
                                                    new int[]{ 55,2,23 }, new int[]{ 56,3,24 }, new int[]{ 57,4,25 }, new int[]{ 58,5,26 } };
            FuzzyCMeans fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.runFCM();

            List<double[]> cluster = fcm.cluster;
            double[,] matrix = fcm.matrix_u;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }

        [TestMethod]
        public void TestFcmArray3_2()
        {
            List<int[]> datas = new List<int[]>() { new int[] { 2,23,23 }, new int[] { 3,24,56 }, new int[] { 4,25,57 }, new int[] { 15,26,58 }, 
                                                    new int[]{ 23,55,2 }, new int[]{ 24,56,34 }, new int[]{ 25,57,4 }, new int[]{ 26,58,5 }, 
                                                    new int[]{ 55,2,23 }, new int[]{ 56,3,24 }, new int[]{ 57,42,25 }, new int[]{ 58,51,26 } };
            FuzzyCMeans fcm = new FuzzyCMeans(datas, 1.8, 0.25, 3);

            fcm.runFCM();

            List<double[]> cluster = fcm.cluster;
            double[,] matrix = fcm.matrix_u;

            Assert.AreEqual(3, cluster.Count);
            Assert.AreEqual(datas.Count, matrix.Length / cluster.Count);
        }
    }
}
