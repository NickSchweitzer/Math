using System;
using System.Collections;
using System.Collections.Generic;

namespace TheCodingMonkey.Math
{
    /// <summary>Comparer class for comparing strings that represent Roman Numerals.</summary>
    public class RomanComparer : IComparer, IComparer<string>, IComparer<char>
    {
        /// <summary>Compares two strings or chars which represent Roman Numerals.</summary>
        /// <param name="left">First Roman Numeral to compare.</param>
        /// <param name="right">Second Roman Numeral to compare.</param>
        /// <returns>Returns -1 if left is less than right, 1 if left is greater than right, and 0 if they are equal.</returns>
        /// <exception cref="ArgumentException">Thrown if either parameter is not a roman numeral.</exception>
        /// <exception cref="InvalidCastException">Thrown if either parameter is not a string or character.</exception>
        public int Compare( object left, object right )
        {
            string strLeft, strRight;
            if ( left is char )
                strLeft = left.ToString();
            else
                strLeft = (string)left;

            if ( right is char )
                strRight = right.ToString();
            else
                strRight = (string)right;

            int nLeft  = RomanNumerals.RomanToInteger( strLeft );
            int nRight = RomanNumerals.RomanToInteger( strRight );

            return nLeft.CompareTo( nRight );
        }

        /// <summary>Compares two strings which represent Roman Numerals.</summary>
        /// <param name="left">First Roman Numeral to compare.</param>
        /// <param name="right">Second Roman Numeral to compare.</param>
        /// <returns>Returns -1 if left is less than right, 1 if left is greater than right, and 0 if they are equal.</returns>
        /// <exception cref="ArgumentException">Thrown if either parameter is not a roman numeral.</exception>
        public int Compare(string left, string right)
        {
            int nLeft = RomanNumerals.RomanToInteger(left);
            int nRight = RomanNumerals.RomanToInteger(right);

            return nLeft.CompareTo(nRight);
        }

        /// <summary>Compares two chars which represent Roman Numerals.</summary>
        /// <param name="left">First Roman Numeral to compare.</param>
        /// <param name="right">Second Roman Numeral to compare.</param>
        /// <returns>Returns -1 if left is less than right, 1 if left is greater than right, and 0 if they are equal.</returns>
        /// <exception cref="ArgumentException">Thrown if either parameter is not a roman numeral.</exception>
        public int Compare(char left, char right)
        {
            int nLeft = RomanNumerals.RomanToInteger(left);
            int nRight = RomanNumerals.RomanToInteger(right);

            return nLeft.CompareTo(nRight);
        }
    }
}