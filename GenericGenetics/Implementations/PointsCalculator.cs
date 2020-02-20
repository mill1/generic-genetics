using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics.Implementations
{
    public class PointsCalculator
    {
        public double Roundness(Point[] points)
        {
            Point center = Center(points);

            foreach (Point point in points)
                point.DistanceToCenter = GetDistance(point, center);

            IEnumerable<double> distancesToCenter = points.Select(p => p.DistanceToCenter);

            double roundness = GetStandardDeviation(distancesToCenter);

            return roundness;
        }

        public Point Center(Point[] points)
        {
            int x = (int)points.Select(p => p.X).Average();
            int y = (int)points.Select(p => p.Y).Average();

            return new Point(x, y);
        }

        private double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow((point2.X - point1.X), 2) + Math.Pow((point2.Y - point1.Y), 2));
        }

        private double GetStandardDeviation(IEnumerable<double> distancesToCenter)
        {
            double average = distancesToCenter.Average();
            double sumOfSquaresOfDifferences = distancesToCenter.Select(val => (val - average) * (val - average)).Sum();
            return Math.Sqrt(sumOfSquaresOfDifferences / distancesToCenter.Count());
        }
    }
}
