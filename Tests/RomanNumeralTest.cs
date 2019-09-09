using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCodingMonkey.Math.Tests
{
    [TestClass, TestCategory("Roman Numerals")]
    public class RomanConversionTest
    {
        [TestMethod]
        public void RomanToIntegerTest()
        {
            // Various test roman numerals and their correct decimal equivalents
            string[] romans  = { "XLIX", "CCCLXII", "CCC", "LX", "II", "M",  "V", "IL", "MDCCCCXCIX", "MDCCCCLXXXXVIIII", "MIM" };
            int[]    numbers = {  49,     362,       300,   60,   2,    1000, 5,   49,   1999,         1999,			   1999 };

            // Loop through both arrays and make sure they match
            for ( int index = 0; index < romans.Length; index++ )
            {
                // Test upper case roman numerals
                int calcNumber = RomanNumerals.RomanToInteger( romans[index] );
                Assert.AreEqual( numbers[index], calcNumber );

                // Test lower case roman numerals
                calcNumber = RomanNumerals.RomanToInteger( romans[index].ToLower() );
                Assert.AreEqual( numbers[index], calcNumber );
            }
        }

        [TestMethod]
        public void RomanCharToIntegerTest()
        {
            // Every single roman numeral, and it's decimal equivalent
            char[] romans  = { 'I', 'V', 'X', 'L', 'C', 'D', 'M'  };
            int[]  numbers = {  1,   5,  10,  50,  100, 500, 1000 };

            // These better match or we're really done for
            for ( int index = 0; index < romans.Length; index++ )
            {
                int calcNumber = RomanNumerals.RomanToInteger( romans[index] );
                Assert.AreEqual( numbers[index], calcNumber );
            }
        }

        [TestMethod]
        public void CompleteIntegerToRomanTest()
        {
            // Create a roman numeral for every valid number, then recalculate the decimal equivalent using my
            // own algorithm.  Test to make sure that they're the same.  If they are, since the algorithms are so 
            // different, then I can assume that they're probably both right.
            for ( int iIteration = RomanNumerals.MinValue; iIteration < RomanNumerals.MaxValue; iIteration++ )
            {
                string strRoman = RomanNumerals.IntegerToRoman( iIteration );
                int    iCheck   = RomanNumerals.RomanToInteger( strRoman );

                Assert.AreEqual( iIteration, iCheck, "{0} generated {1} which is equal to {2}", iIteration, strRoman, iCheck );
                Console.WriteLine( "{0,4} : {1}", iIteration, strRoman );
            }
        }

        [TestMethod]
        public void IllegalCharTest()
        {
            // Test to make sure that an invalid character throws an exception
            Assert.ThrowsException<ArgumentException>(() => RomanNumerals.RomanToInteger('t'));
        }

        [TestMethod]
        public void IllegalStringTest()
        {
            // Test to make sure a string with valid and invalid characters throws an exception
            Assert.ThrowsException<ArgumentException>(() => RomanNumerals.RomanToInteger("XIVt"));
        }

        [TestMethod]
        public void NullStringTest()
        {
            // Test to make sure we throw the proper exception on a null argument
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumerals.RomanToInteger(null));
        }

        [TestMethod]
        public void EmptyStringTest()
        {
            // Test to make sure we throw a valid exception on an empty string
            Assert.ThrowsException<ArgumentException>(() => RomanNumerals.RomanToInteger(string.Empty));
        }

        [TestMethod]
        public void NumberToLargeTest()
        {
            // Test to make sure that we throw a valid exception for a number which is too large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => RomanNumerals.IntegerToRoman(RomanNumerals.MaxValue + 1));
        }

        [TestMethod]
        public void NegativeNumberTest()
        {
            // Test to make sure that we throw a valid exception for a number which is too large
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => RomanNumerals.IntegerToRoman(RomanNumerals.MinValue - 1));
        }
    }
}