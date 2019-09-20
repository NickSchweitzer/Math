using System;
using System.Collections.Generic;
using System.Linq;

namespace TheCodingMonkey.Math
{
    /// <summary>Contains various useful extension methods for numeric equality, and simple statistical calculations.</summary>
    public static class MathExtensions
    {
        /// <summary>Extension method used to determine if an item is between the other two, inclusive.</summary>
        /// <typeparam name="T">Type of item to compare. This type must implement IComparable.</typeparam>
        /// <param name="test">The item being tested.</param>
        /// <param name="minValue">The lesser item to compare to.</param>
        /// <param name="maxValue">The greater item to compare to.</param>
        /// <remarks>The item test is considered between if it is greater than or equal to first and less than or 
        /// equal to second.</remarks>
        public static bool Between<T>(this T test, T minValue, T maxValue)
            where T : IComparable<T>
        {
            // If minValue is greater than maxValue, simply reverse the comparision.
            if (minValue.CompareTo(maxValue) >= 0)
                return (test.CompareTo(maxValue) >= 0 && test.CompareTo(minValue) <= 0);
            else
                return (test.CompareTo(minValue) >= 0 && test.CompareTo(maxValue) <= 0);
        }

        /// <summary>Extension method used to determine if two values are very close to equal, based on
        /// the Epsilon, the .NET defined potential error between equal floating point numbers.</summary>
        /// <param name="left">First item to be compared.</param>
        /// <param name="right">Second item to be compared.</param>
        /// <returns>Returns true if left - right is less than or equal to Epsilon, and false otherwise.</returns>
        public static bool AlmostEqual(this float left, float right)
        {
            return left.AlmostEqual(right, float.Epsilon);
        }

        /// <summary>Extension method used to determine if two values are very close to equal, based on
        /// the Epsilon, the .NET defined potential error between equal floating point numbers.</summary>
        /// <param name="left">First item to be compared.</param>
        /// <param name="right">Second item to be compared.</param>
        /// <returns>Returns true if left - right is less than or equal to Epsilon, and false otherwise.</returns>
        public static bool AlmostEqual(this double left, double right)
        {
            return left.AlmostEqual(right, double.Epsilon);
        }

        /// <summary>Extension method used to determine if two values are very close to equal, based on
        /// the specified precision, or error amount.</summary>
        /// <param name="left">First item to be compared.</param>
        /// <param name="right">Second item to be compared.</param>
        /// <param name="precision">Amount of error allowed in the comparison.</param>
        /// <returns>Returns true if left - right is less than or equal to precision, and false otherwise.</returns>
        public static bool AlmostEqual(this float left, float right, float precision)
        {
            return (System.Math.Abs(left - right) <= precision);
        }

        /// <summary>Extension method used to determine if two values are very close to equal, based on
        /// the specified precision, or error amount.</summary>
        /// <param name="left">First item to be compared.</param>
        /// <param name="right">Second item to be compared.</param>
        /// <param name="precision">Amount of error allowed in the comparison.</param>
        /// <returns>Returns true if left - right is less than or equal to precision, and false otherwise.</returns>
        public static bool AlmostEqual(this double left, double right, double precision)
        {
            return (System.Math.Abs(left - right) <= precision);
        }

        /// <summary>Extension method used to determine if two values are very close to equal, based on
        /// the specified precision, or error amount.</summary>
        /// <param name="left">First item to be compared.</param>
        /// <param name="right">Second item to be compared.</param>
        /// <param name="precision">Amount of error allowed in the comparison.</param>
        /// <returns>Returns true if left - right is less than or equal to precision, and false otherwise.</returns>
        public static bool AlmostEqual(this decimal left, decimal right, decimal precision)
        {
            return (System.Math.Abs(left - right) <= precision);
        }

        /// <summary>Extension method used to calculate the mode of a list of numbers.</summary>
        /// <typeparam name="T">Type parameters of the list being analayzed</typeparam>
        /// <param name="items">IEnumerable of items to determine the statistical mode of.</param>
        /// <returns>An IList of Tuples where Value is the item that has the most items, and Count is the count of that item.</returns>
        public static IList<(T Value, int Count)> Mode<T>(this IEnumerable<T> items)
        {            
            Dictionary<T, int> counts = new Dictionary<T, int>();
            foreach (T item in items)
            {
                int count = counts.ContainsKey(item) ? counts[item] : 0;
                counts[item] = count + 1;
            }

            var returnList = new List<(T Value, int Count)>();
            foreach (KeyValuePair<T, int> tuple in counts)
            {
                if (returnList.Count == 0)
                    returnList.Add((tuple.Key, tuple.Value));
                else if (tuple.Value > returnList.First().Count)
                {
                    returnList.Clear();
                    returnList.Add((tuple.Key, tuple.Value));
                }
                else if (tuple.Value == returnList.First().Count)
                    returnList.Add((tuple.Key, tuple.Value));
            }

            return returnList;
        }

        /// <summary>Calculates the Median of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the average of</param>
        /// <returns>Median value of all the items in the list</returns>
        /// <remarks>The median is the value seperating the higher half from the lower half of a data sample. If there are an
        /// even number of values in the list, such that an exact center can't be determined, then the average of the two
        /// middle numbers is returned.</remarks>
        public static double Median(this IEnumerable<int> items)
        {
            List<int> sortedList = new List<int>(items);
            sortedList.Sort();
            if (sortedList.Count % 2 == 1)
                return sortedList[sortedList.Count / 2];
            else
            {
                int first = sortedList[sortedList.Count / 2 - 1];
                int second = sortedList[sortedList.Count / 2];
                if (first == second)
                    return first;
                else
                    return (first + second) / 2d;
            }
        }

        /// <summary>Calculates the Median of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the average of</param>
        /// <returns>Median value of all the items in the list</returns>
        /// <remarks>The median is the value seperating the higher half from the lower half of a data sample. If there are an
        /// even number of values in the list, such that an exact center can't be determined, then the average of the two
        /// middle numbers is returned.</remarks>
        public static double Median(this IEnumerable<float> items)
        {
            List<float> sortedList = new List<float>(items);
            sortedList.Sort();
            if (sortedList.Count % 2 == 1)
                return sortedList[sortedList.Count / 2];
            else
            {
                float first = sortedList[sortedList.Count / 2 - 1];
                float second = sortedList[sortedList.Count / 2];
                if (first == second)
                    return first;
                else
                    return (first + second) / 2;
            }
        }

        /// <summary>Calculates the Median of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the average of</param>
        /// <returns>Median value of all the items in the list</returns>
        /// <remarks>The median is the value seperating the higher half from the lower half of a data sample. If there are an
        /// even number of values in the list, such that an exact center can't be determined, then the average of the two
        /// middle numbers is returned.</remarks>
        public static double Median(this IEnumerable<double> items)
        {
            List<double> sortedList = new List<double>(items);
            sortedList.Sort();
            if (sortedList.Count % 2 == 1)
                return sortedList[sortedList.Count / 2];
            else
            {
                double first = sortedList[sortedList.Count / 2 - 1];
                double second = sortedList[sortedList.Count / 2];
                if (first == second)
                    return first;
                else
                    return (first + second) / 2;
            }
        }

        /// <summary>Calculates the Median of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the average of</param>
        /// <returns>Median value of all the items in the list</returns>
        /// <remarks>The median is the value seperating the higher half from the lower half of a data sample. If there are an
        /// even number of values in the list, such that an exact center can't be determined, then the average of the two
        /// middle numbers is returned.</remarks>
        public static decimal Median(this IEnumerable<decimal> items)
        {
            List<decimal> sortedList = new List<decimal>(items);
            sortedList.Sort();
            if (sortedList.Count % 2 == 1)
                return sortedList[sortedList.Count / 2];
            else
            {
                decimal first = sortedList[sortedList.Count / 2 - 1];
                decimal second = sortedList[sortedList.Count / 2];
                if (first == second)
                    return first;
                else
                    return (first + second) / 2;
            }
        }

        /// <summary>Calculates the Standard Deviation (Sigma) of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the standard deviation of</param>
        /// <returns>Standard deviaion of all the items passed in.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="items"/> doesn't contain at least 2 items.</exception>
        public static double StdDeviation(this IEnumerable<int> items)
        {
            double mean = items.Average();
            double deltaSum = 0;
            int count = 0;
            foreach (int item in items)
            {
                deltaSum += System.Math.Pow((item - mean), 2);
                count++;
            }

            if (count < 2)
                throw new ArgumentException("Collection must have at least two items", "items");

            double sigmaSquared = deltaSum / (count - 1);
            return System.Math.Sqrt(sigmaSquared);
        }

        /// <summary>Calculates the Standard Deviation (Sigma) of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the standard deviation of</param>
        /// <returns>Standard deviaion of all the items passed in.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="items"/> doesn't contain at least 2 items.</exception>
        public static double StdDeviation(this IEnumerable<float> items)
        {
            double mean = items.Average();
            double deltaSum = 0;
            int count = 0;
            foreach (float item in items)
            {
                deltaSum += System.Math.Pow((item - mean), 2);
                count++;
            }

            if (count < 2)
                throw new ArgumentException("Collection must have at least two items", "items");

            double sigmaSquared = deltaSum / (count - 1);
            return System.Math.Sqrt(sigmaSquared);
        }

        /// <summary>Calculates the Standard Deviation (Sigma) of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the standard deviation of</param>
        /// <returns>Standard deviaion of all the items passed in.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="items"/> doesn't contain at least 2 items.</exception>
        public static double StdDeviation(this IEnumerable<double> items)
        {
            double mean = items.Average();
            double deltaSum = 0;
            int count = 0;
            foreach (double item in items)
            {
                deltaSum += System.Math.Pow((item - mean), 2);
                count++;
            }

            if (count < 2)
                throw new ArgumentException("Collection must have at least two items", "items");

            double sigmaSquared = deltaSum / (count - 1);
            return System.Math.Sqrt(sigmaSquared);
        }

        /// <summary>Calculates the Standard Deviation (Sigma) of all the passed in values.</summary>
        /// <param name="items">IEnumerable of items to calculate the standard deviation of</param>
        /// <returns>Standard deviaion of all the items passed in.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="items"/> doesn't contain at least 2 items.</exception>
        public static double StdDeviation(this IEnumerable<decimal> items)
        {
            decimal mean = items.Average();
            double deltaSum = 0;
            int count = 0;
            foreach (decimal item in items)
            {
                deltaSum += System.Math.Pow((double)(item - mean), 2);
                count++;
            }

            if (count < 2)
                throw new ArgumentException("Collection must have at least two items", "items");

            double sigmaSquared = deltaSum / (count - 1);
            return System.Math.Sqrt(sigmaSquared);
        }
    }
}