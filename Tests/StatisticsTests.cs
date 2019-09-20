using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCodingMonkey.Math.Tests
{
    [TestClass, TestCategory("Statistics")]
    public class StatisticsTests
    {
        [TestMethod]
        public void NoModesTest()
        {
            var testValues = new int[] { };
            var modes = testValues.Mode();
            Assert.AreEqual(0, modes.Count);
        }

        [TestMethod]
        public void SingleModeTest()
        {
            var testValues = new int[] { 1, 10, 2, 8, 16, 37, 19, 10, 10, 5, 10, 2 };
            var modes = testValues.Mode();
            Assert.AreEqual(1, modes.Count);
            Assert.AreEqual(10, modes.First().Value);
            Assert.AreEqual(4, modes.First().Count);
        }

        [TestMethod]
        public void MultipleModesTest()
        {
            var testValues = new int[] { 1, 10, 15, 2, 8, 5, 2, 15, 20, 100, 5 };
            var modes = testValues.Mode().ToList();
            Assert.AreEqual(3, modes.Count);

            CollectionAssert.Contains(modes, (2, 2));
            CollectionAssert.Contains(modes, (5, 2));
            CollectionAssert.Contains(modes, (15, 2));
        }

        [TestMethod]
        public void IntegerMedianTest()
        {
            var oddNumber = new int[] { 1, 5, 8, 2, 20, 15, 0, 55, 3 };
            var evenNumber = new int[] { 1, 5, 8, 2, 20, 15, 0, 55, 3, 65 };

            Assert.AreEqual(5, oddNumber.Median());
            Assert.AreEqual(6.5, evenNumber.Median());
        }
    }
}