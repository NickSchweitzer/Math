using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheCodingMonkey.Math
{
    /// <summary>Helper class which contains basic Geometric calculations for lines, polygons, and other shapes.</summary>
    public static class Geometry
    {
        /// <summary>Calculates the linear distance between two points</summary>
        /// <param name="p1">Start of the line</param>
        /// <param name="p2">End of the line</param>
        /// <returns>Linear distance</returns>
        public static double Length(Point p1, Point p2)
        {
            return System.Math.Sqrt(System.Math.Pow(p2.X - p1.X, 2) + System.Math.Pow(p2.Y - p1.Y, 2));
        }

        /// <summary>Calculates the linear distance between two points</summary>
        /// <param name="p1">Start of the line</param>
        /// <param name="p2">End of the line</param>
        /// <returns>Linear distance</returns>
        public static double Length(PointF p1, PointF p2)
        {
            return System.Math.Sqrt(System.Math.Pow(p2.X - p1.X, 2) + System.Math.Pow(p2.Y - p1.Y, 2));
        }

        /// <summary>Calculates the area of a Polygon given an array of Points.</summary>
        /// <param name="points">Points which define the vertices of the polygon.</param>
        /// <returns>A double representing the area inside the polygon.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        public static double PolygonArea(this IList<Point> points)
        {
            if (points.Count < 3)
                throw new ArgumentException("Polygon must have at least three points", "points");

            double area = 0;
            for (int i = 0; i < points.Count; i++) 
            {
              int j = (i + 1) % points.Count;
              area += points[i].X * points[j].Y;
              area -= points[i].Y * points[j].X;
            }
            return System.Math.Abs(area / 2);
        }

        /// <summary>Calculates the area of a Polygon given an array of Points.</summary>
        /// <param name="points">Points which define the vertices of the polygon.</param>
        /// <returns>A double representing the area inside the polygon.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        /// <exception cref="ArgumentException">Thrown if the IList of points contains fewer than three elements.</exception>
        public static double PolygonArea(this IList<PointF> points)
        {
            if (points.Count < 3)
                throw new ArgumentException("Polygon must have at least three points", "points");

            double area = 0;
            for (int i = 0; i < points.Count; i++) 
            {
              int j = (i + 1) % points.Count;
              area += points[i].X * points[j].Y;
              area -= points[i].Y * points[j].X;
            }
            return System.Math.Abs(area / 2);
        }

        /// <summary>Calculates the center of gravity for a Polygon given an array of Points.</summary>
        /// <param name="points">Points which define the vertices of the polygon.</param>
        /// <returns>The Point which represents the center of gravity.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array. This method assumes the polygon has a 
        /// homogeneous density.</remarks>
        /// <exception cref="ArgumentException">Thrown if the IList of points contains fewer than three elements.</exception>
        public static PointF Centroid(this IList<Point> points)
        {
            double areaFactor = points.PolygonArea() * 6d;
            double cX = 0;
            double cY = 0;

            for (int i = 0; i < points.Count; i++)
            {
                int j = (i + 1) % points.Count;
                double mult = (points[i].X * points[j].Y) - (points[j].X * points[i].Y);
                cX += (points[i].X + points[j].X) * mult;
                cY += (points[i].Y + points[j].Y) * mult;
            }

            cX /= areaFactor;
            cY /= areaFactor;

            return new PointF((float)cX, (float)cY);
        }

        /// <summary>Calculates the center of gravity for a Polygon given an array of Points.</summary>
        /// <param name="points">Points which define the vertices of the polygon.</param>
        /// <returns>The Point which represents the center of gravity.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array. This method assumes the polygon has a 
        /// homogeneous density.</remarks>
        /// <exception cref="ArgumentException">Thrown if the IList of points contains fewer than three elements.</exception>
        public static PointF Centroid(this IList<PointF> points)
        {
            double areaFactor = points.PolygonArea() * 6d;
            double cX = 0;
            double cY = 0;

            for (int i = 0; i < points.Count; i++)
            {
                int j = (i + 1) % points.Count;
                double mult = (points[i].X * points[j].Y) - (points[j].X * points[i].Y);
                cX += (points[i].X + points[j].X) * mult;
                cY += (points[i].Y + points[j].Y) * mult;
            }

            cX /= areaFactor;
            cY /= areaFactor;

            return new PointF((float)cX, (float)cY);
        }

        /// <summary>Determines whether a given point is inside a polygon defined by a list of points</summary>
        /// <param name="testPoint">Test Point</param>
        /// <param name="polygon">List of points which define the boundaries of the polygon</param>
        /// <returns>True if <paramref name="testPoint"/> is in <paramref name="polygon"/>. False otherwise.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        public static bool InsidePolygon(this Point testPoint, IList<Point> polygon)
        {
            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((((polygon[i].Y <= testPoint.Y) && (testPoint.Y < polygon[j].Y)) ||
                     ((polygon[j].Y <= testPoint.Y) && (testPoint.Y < polygon[i].Y))) &&
                    (testPoint.X < (polygon[j].X - polygon[i].X) * (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    inside = !inside;
            }
            return inside;
        }

        /// <summary>Determines whether a given point is inside a polygon defined by a list of points</summary>
        /// <param name="testPoint">Test Point</param>
        /// <param name="polygon">List of points which define the boundaries of the polygon</param>
        /// <returns>True if <paramref name="testPoint"/> is in <paramref name="polygon"/>. False otherwise.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        public static bool InsidePolygon(this PointF testPoint, IList<PointF> polygon)
        {
            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((((polygon[i].Y <= testPoint.Y) && (testPoint.Y < polygon[j].Y)) ||
                     ((polygon[j].Y <= testPoint.Y) && (testPoint.Y < polygon[i].Y))) &&
                    (testPoint.X < (polygon[j].X - polygon[i].X) * (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    inside = !inside;
            }
            return inside;
        }

        /// <summary>Determines whether a polygon is considered Convex, where all interior angles are less than 180 degrees.</summary>
        /// <param name="points">List of points which define the boundaries of the polygon</param>
        /// <returns>True if the polygon is Convex, false if it is Concave, and null if it cannot be computed.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        /// <exception cref="ArgumentException">Thrown if the IList of points contains fewer than three elements.</exception>
        public static bool? Convex(this IList<Point> points)
        {
            if (points.Count < 3)
                throw new ArgumentException("Polygon must contain at least three points", "points");

            int flag = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int j = (i + 1) % points.Count;
                int k = (i + 2) % points.Count;
                int z = (points[j].X - points[i].X) * (points[k].Y - points[j].Y);
                z -= (points[j].Y - points[i].Y) * (points[k].X - points[j].X);

                if (z < 0)
                    flag |= 1;
                else if (z > 0)
                    flag |= 2;

                if (flag == 3)
                    return false;
            }

            if (flag != 0)
                return true;
            else
                return null;    // Incomputable
        }

        /// <summary>Determines whether a polygon is considered Convex, where all interior angles are less than 180 degrees.</summary>
        /// <param name="points">List of points which define the boundaries of the polygon</param>
        /// <returns>True if the polygon is Convex, false if it is Concave, and null if it cannot be computed.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        /// <exception cref="ArgumentException">Thrown if the IList of points contains fewer than three elements.</exception>
        public static bool? Convex(this IList<PointF> points)
        {
            if (points.Count < 3)
                throw new ArgumentException("Polygon must contain at least three points", "points");

            int flag = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int j = (i + 1) % points.Count;
                int k = (i + 2) % points.Count;
                double z = (points[j].X - points[i].X) * (points[k].Y - points[j].Y);
                z -= (points[j].Y - points[i].Y) * (points[k].X - points[j].X);

                if (z < 0)
                    flag |= 1;
                else if (z > 0)
                    flag |= 2;
                if (flag == 3)
                    return false;
            }

            if (flag != 0)
                return true;
            else
                return null;    // Incomputable
        }

        /// <summary>Determines whether a polygon is considered Concave, where at least one interior angle is between 180 and 360 degrees.</summary>
        /// <param name="points">List of points which define the boundaries of the polygon</param>
        /// <returns>True if the polygon is Concave, false if it is Convex, and null if it cannot be computed.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        /// <exception cref="ArgumentException">Thrown if the IList of points contains fewer than three elements.</exception>
        public static bool? Concave(this IList<Point> points)
        {
            bool? convex = points.Convex();
            if (convex == null)
                return null;
            else
                return !convex.Value;
        }

        /// <summary>Determines whether a polygon is considered Concave, where at least one interior angle is between 180 and 360 degrees.</summary>
        /// <param name="points">List of points which define the boundaries of the polygon</param>
        /// <returns>True if the polygon is Concave, false if it is Convex, and null if it cannot be computed.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        /// <exception cref="ArgumentException">Thrown if the IList of points contains fewer than three elements.</exception>
        public static bool? Concave(this IList<PointF> points)
        {
            bool? convex = points.Convex();
            if (convex == null)
                return null;
            else
                return !convex.Value;
        }
    }
}