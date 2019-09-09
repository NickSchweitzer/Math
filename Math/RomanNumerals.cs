using System;
using System.Diagnostics;

namespace TheCodingMonkey.Math
{
    /// <summary>Static utility class for converting between Arabic Numerals and Roman Numerals.</summary>
    public static class RomanNumerals
    {
        // All Valid Roman Numerals and their corresponding integer values
        private static readonly char[] LETTERS		 = { 'I', 'V', 'X', 'L', 'C', 'D', 'M'  };
        private static readonly char[] LETTERS_SMALL = { 'i', 'v', 'x', 'l', 'c', 'd', 'm'  };
        private static readonly int[]  NUMBERS		 = {  1,   5,   10,  50, 100, 500, 1000 };

        // All Roman Numerals which are powers of 10 and their corresponding integer values.
        // These are the ones that can be repeated and subtracted.
        private static readonly char[] LETTERS_10 = { 'I', 'X', 'C', 'M'  };
        private static readonly int[]  NUMBERS_10 = {  1,   10, 100, 1000 };

        /// <summary>Represents the smallest possible integer that can be converted to a Roman Numeral.</summary>
        public const int MinValue = 1;

        /// <summary>Represents the largest possible integer that can be converted to a Roman Numeral.</summary>
        public const int MaxValue = 3999;

        /// <summary>Calculates the integer equivalent of a multi character roman numeral.</summary>
        /// <param name="strRoman">Roman numeral to analyze.</param>
        /// <returns>Decimal equivalent of the roman numeral.</returns>
        /// <exception cref="ArgumentNullException">Thrown if a null string is passed in.</exception>
        /// <exception cref="ArgumentException">Thrown if an empty string is passed in.</exception>
        /// <exception cref="ArgumentException">Thrown if a character is passed in that is not a roman numeral.</exception>
        public static int RomanToInteger( string strRoman )
        {
            // Can't analyze an empty string
            if ( strRoman == null )
                throw new ArgumentNullException( "strRoman" );
            else if ( strRoman == string.Empty )
                throw new ArgumentException( "String to analyze cannot be Empty", strRoman );

            // Seed the return value with the last digit in the string
            int nReturn = RomanToInteger( strRoman[strRoman.Length - 1] );
            int nLast   = nReturn;

            // Now loop through the remaining characters in the string backwards
            for ( int index = strRoman.Length - 2; index >= 0; index-- )
            {
                // Calculate the decimal equivalent of the current letter
                int nCurrent = RomanToInteger( strRoman[index] );

                // If the current digit is greater than or equal to the one I previously looked at
                // then it just gets added to the total, otherwise we're looking at the subtraction
                // rule of roman numerals and so we need to take away from the total.
                if ( nCurrent >= nLast )
                    nReturn += nCurrent;
                else
                    nReturn -= nCurrent;

                // Remember the last one I looked at
                nLast = nCurrent;
            }

            return nReturn;
        }

        /// <summary>Returns the integer equivalent of a single roman numeral.</summary>
        /// <param name="cRoman">Character to analyze.</param>
        /// <returns>Decimal equivalent of that roman numeral.</returns>
        /// <exception cref="ArgumentException">Thrown if a character is passed in that is not a roman numeral.</exception>
        public static int RomanToInteger( char cRoman )
        {
            for ( int iValid = 0; iValid < LETTERS.Length; iValid++ )
            {
                if ( LETTERS[iValid] == cRoman || LETTERS_SMALL[iValid] == cRoman )
                    return NUMBERS[iValid];
            }

            // If didn't find it in my array, then must be invalid
            throw new ArgumentException( string.Format( "{0} is not a valid roman numeral", cRoman ) );
        }

        /// <summary>Returns the roman numeral equivalent of an integer.</summary>
        /// <param name="num">Arabic number to convert.</param>
        /// <returns>Equivalent roman numeral.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if num is less than MinValue or greater than MaxValue.</exception>
        /// <remarks>This method is limited to numbers smaller than 3999 because there is no appropriate roman numeral
        /// character in ASCII to use for numbers greater than or equal to 4000.</remarks>
        public static string IntegerToRoman( int num )
        {
            if ( ( num < MinValue ) || ( num > MaxValue ) )
                throw new ArgumentOutOfRangeException( "num", num, "Number must be greater than MinValue and less than MaxValue" );

            // The final roman numeral that I'll return
            string strReturn = string.Empty;

            int divisor = 1;
            do
            {
                // Look at each digit in the number individually instead of trying to analyze
                // the entire integer number as a whole. This will yield the most valid roman numeral.
                divisor  *= 10;
                int digit = num % divisor;

                if ( digit > 0 )
                    strReturn = DigitToRoman( digit ) + strReturn;
			
                num -= digit;

            } while ( ( num / divisor ) > 0 );

            return strReturn;
        }

        /// <summary>Helper method which returns the roman numeral equivalent of a single "digit".</summary>
        /// <param name="digit">Digit to find roman numeral for.</param>
        /// <returns>Partial roman numeral string for the digit.</returns>
        /// <remarks>It's not a digit in the truest sense, because it can be larger than 9.  However, for numbers
        /// larger than 9, it must be a multiple of 10.  Digits must also be greater than zero.  These constraints
        /// are not enforced programmatically since it's just a private method.</remarks>
        private static string DigitToRoman( int digit )
        {
            if (digit <= 0 || (digit > 10 && digit % 10 != 0))
                throw new ArgumentException("Invalid digit - Must be a single digit, or a multiple of 10");

            // First find the roman numeral that's one larger than this one
            bool bFoundSubtract = false;

            int index;
            for ( index = 0; index < NUMBERS.Length; index++ )
            {
                if ( NUMBERS[index] == digit )  // If found an exact match, then bail immediately
                    return LETTERS[index].ToString();
                else if ( NUMBERS[index] > digit )
                {
                    // Found the roman numeral that's one larger than this digit.
                    // See if the subtract rule applies
                    bFoundSubtract = true;
                    break;
                }
            }

            // If found a larger numeral, see if I can use the subtraction rule
            if ( bFoundSubtract )
            {
                int idx10;
                // Try to find a number that is a power of 10 that I can subtract from this one
                for ( idx10 = 0; idx10 < NUMBERS_10.Length; idx10++ )
                {
                    int possibleDigit = NUMBERS[index] - NUMBERS_10[idx10];

                    // Found a possible extra digit thats valid to subtract
                    if ( possibleDigit == digit )
                    {
                        // Now make sure it follows the 1/10th, 1/5th rule.  If it does, return the combination
                        // Otherwise we break out and do the additive rule
                        if ( ( NUMBERS_10[idx10] * 5 ) == NUMBERS[index] || ( NUMBERS_10[idx10] * 10 ) == NUMBERS[index] )
                            return LETTERS_10[idx10].ToString() + LETTERS[index].ToString();
                        else
                            break;
                    }
                    else if ( possibleDigit < digit )
                        break;	// Have to do the additive rule
                }
            }

            // Didn't find the subtractive rule, so need to do the additive rule

            index--;	// Index is currently at one larger than my number, so go back one roman numeral.
            string strReturn = LETTERS[index].ToString();	// Start off my numeral
            digit -= NUMBERS[index];						// Subtract from my running digit

            // Now add on as much as I need to in order to get to the rest of my number
            while ( digit > 0 )
            {
                int idxAdd;

                // Can only tack on numbers that are powers of 10
                for ( idxAdd = 0; idxAdd < NUMBERS_10.Length; idxAdd++ )
                {
                    if ( NUMBERS_10[idxAdd] == digit )
                        // Found an exact match
                        break;
                    else if ( NUMBERS_10[idxAdd] > digit )
                    {
                        // Went too far, grab the previous numeral
                        idxAdd--;
                        break;
                    }
                }

                // Catch the end of array case
                if ( idxAdd == NUMBERS_10.Length )
                    idxAdd--;

                strReturn += LETTERS_10[idxAdd];	// Tack on the numeral
                digit     -= NUMBERS_10[idxAdd];	// Subtract from my running digit
            }

            return strReturn;
        }
    }
}