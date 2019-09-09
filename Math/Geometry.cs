using System;
using System.Drawing;

namespace TheCodingMonkey.Math
{
    public static class Geometry
    {
        public static double Length(Point p1, Point p2)
        {
            return System.Math.Sqrt(System.Math.Pow(p2.X - p1.X, 2) + System.Math.Pow(p2.Y - p1.Y, 2));
        }

        public static double Length(PointF p1, PointF p2)
        {
            return System.Math.Sqrt(System.Math.Pow(p2.X - p1.X, 2) + System.Math.Pow(p2.Y - p1.Y, 2));
        }

        /// <summary>Calculates the area of a Polygon given an array of Points.</summary>
        /// <param name="points">Points which define the vertices of the polygon.</param>
        /// <returns>A double representing the area inside the polygon.</returns>
        /// <remarks>The polygon is assumed to be closed, with the last point in the array <i>not</i> 
        /// being the same as the first point in the array.</remarks>
        public static double PolygonArea(this Point[] points)
        {
            if (points.Length < 3)
                throw new ArgumentException("Polygon must have at least three points", "points");

            double area = 0;
            for (int i = 0; i < points.Length; i++) 
            {
              int j = (i + 1) % points.Length;
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
        public static double PolygonArea(this PointF[] points)
        {
            if (points.Length < 3)
                throw new ArgumentException("Polygon must have at least three points", "points");

            double area = 0;
            for (int i = 0; i < points.Length; i++) 
            {
              int j = (i + 1) % points.Length;
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
        public static PointF Centroid(this Point[] points)
        {
            double areaFactor = points.PolygonArea() * 6d;
            double cX = 0;
            double cY = 0;

            for (int i = 0; i < points.Length; i++)
            {
                int j = (i + 1) % points.Length;
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
        public static PointF Centroid(this PointF[] points)
        {
            double areaFactor = points.PolygonArea() * 6d;
            double cX = 0;
            double cY = 0;

            for (int i = 0; i < points.Length; i++)
            {
                int j = (i + 1) % points.Length;
                double mult = (points[i].X * points[j].Y) - (points[j].X * points[i].Y);
                cX += (points[i].X + points[j].X) * mult;
                cY += (points[i].Y + points[j].Y) * mult;
            }

            cX /= areaFactor;
            cY /= areaFactor;

            return new PointF((float)cX, (float)cY);
        }

        public static bool InsidePolygon(this Point testPoint, Point[] polygon)
        {
            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((((polygon[i].Y <= testPoint.Y) && (testPoint.Y < polygon[j].Y)) ||
                     ((polygon[j].Y <= testPoint.Y) && (testPoint.Y < polygon[i].Y))) &&
                    (testPoint.X < (polygon[j].X - polygon[i].X) * (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    inside = !inside;
            }
            return inside;
        }

        public static bool InsidePolygon(this PointF testPoint, PointF[] polygon)
        {
            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((((polygon[i].Y <= testPoint.Y) && (testPoint.Y < polygon[j].Y)) ||
                     ((polygon[j].Y <= testPoint.Y) && (testPoint.Y < polygon[i].Y))) &&
                    (testPoint.X < (polygon[j].X - polygon[i].X) * (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    inside = !inside;
            }
            return inside;
        }

        public static bool? Convex(this Point[] points)
        {
            if (points.Length < 3)
                throw new ArgumentException("Polygon must contain at least three points", "points");

            int flag = 0;
            for (int i = 0; i < points.Length; i++)
            {
                int j = (i + 1) % points.Length;
                int k = (i + 2) % points.Length;
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

        public static bool? Convex(this PointF[] points)
        {
            if (points.Length < 3)
                throw new ArgumentException("Polygon must contain at least three points", "points");

            int flag = 0;
            for (int i = 0; i < points.Length; i++)
            {
                int j = (i + 1) % points.Length;
                int k = (i + 2) % points.Length;
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

        public static bool? Concave(this Point[] points)
        {
            return !points.Convex();
        }

        public static bool? Concave(this PointF[] points)
        {
            return !points.Convex();
        }
    }
}