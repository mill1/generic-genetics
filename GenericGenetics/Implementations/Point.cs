using System;

namespace GenericGenetics.Implementations
{
    public class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double DistanceToCenter { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Value
        {
            get
            {
                return Math.Abs(X) + Math.Abs(Y);
            }
        }

        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Point);
        }

        public bool Equals(Point p)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(p, null))
                return false;

            // Optimization for a common success case.
            if (ReferenceEquals(this, p))
                return true;

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
                return false;

            // Return true if the fields match.
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return X * 0x00010000 + Y;
        }

        public static bool operator ==(Point lhs, Point rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                    return true; // null == null = true.

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Point lhs, Point rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"X: {X, 3:##0}  Y: {Y,3:##0}  Value: {Value,4:###0}";
        }
    }
}