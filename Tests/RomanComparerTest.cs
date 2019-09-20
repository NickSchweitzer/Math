using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCodingMonkey.Math.Tests
{
    [TestClass, TestCategory("Roman Numerals")]
    public class RomanComparerTest
    {
        private const int COLLECTION_SIZE = 500;

        [TestMethod]
        public void CollectionSortTest()
        {
            Random rand = new Random();
            List<string> sortList = new List<string>(COLLECTION_SIZE);

            // Fill the collection with a random assortment of roman numerals
            for (int i = 0; i < COLLECTION_SIZE; i++)
            {
                int num = rand.Next(RomanNumerals.MinValue, RomanNumerals.MaxValue);
                sortList.Add(RomanNumerals.IntegerToRoman(num));
            }

            // Sort the list with my comparer
            sortList.Sort(new RomanComparer());

            // Now iterate over the list and make sure that every element is less than the one after it
            for (int i = 0; i < sortList.Count - 2; i++)
            {
                string strLeft = (string)sortList[i];
                string strRight = (string)sortList[i + 1];

                int nLeft = RomanNumerals.RomanToInteger(strLeft);
                int nRight = RomanNumerals.RomanToInteger(strRight);

                Assert.IsTrue(nLeft <= nRight, "{0} is not less than {1}", strLeft, strRight);
            }
        }

        public void DigitComparisonTest()
        {
            RomanComparer comparer = new RomanComparer();

            char[] upper = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            char[] lower = { 'i', 'v', 'x', 'l', 'c', 'd', 'm' };

            for (int i = 0; i < upper.Length - 2; i++)
            {
                for (int j = 1; j < upper.Length - 1; j++)
                {
                    Assert.IsTrue(comparer.Compare(upper[i], lower[j]) < 0);
                    Assert.IsTrue(comparer.Compare(lower[j], upper[i]) > 0);
                }
            }
        }
    }
}