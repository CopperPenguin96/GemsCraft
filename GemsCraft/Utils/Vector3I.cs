﻿// Copyright 2009-2012 Matvei Stefarov <me@matvei.org>
// With contributions by Conrad "Redshift" Morgan
// Modified by apotter96 for GemsCraft

using System;
using GemsCraft.AppSystem.Types;
using GemsCraft.Drawing;
using GemsCraft.Players;
using GemsCraft.Utils;

namespace GemsCraft.Utils
{
    /// <summary>
    /// Integer 3D vector.
    /// </summary>
    public struct Vector3I : IEquatable<Vector3I>, IComparable<Vector3I>, IComparable<Vector3F>
    {
        public static readonly Vector3I Zero = new Vector3I(0, 0, 0);
        public static readonly Vector3I Up = new Vector3I(0, 0, 1);
        public static readonly Vector3I Down = new Vector3I(0, 0, -1);

        /// <summary>
        /// Depth and Length Axis
        /// </summary>
        public int X;

        /// <summary>
        /// Depth and Length Axis
        /// </summary>
        public int Y;

        /// <summary>
        /// Height Axis
        /// </summary>
        public int Z;

        public int X2 => X * X;
        public int Y2 => Y * Y;
        public int Z2 => Z * Z;

        public Vector3I(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3I(long x, long y, long z)
        {
            X = (int) x;
            Y = (int) y;
            Z = (int) z;
        }

        public Vector3I(Vector3I other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }

        public Vector3I(Vector3F other)
        {
            X = (int)other.X;
            Y = (int)other.Y;
            Z = (int)other.Z;
        }


        public float Length => (float)Math.Sqrt((double)X * X + (double)Y * Y + (double)Z * Z);

        public int LengthSquared => X * X + Y * Y + Z * Z;


        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return X;
                    case 1: return Y;
                    default: return Z;
                }
            }
            set
            {
                switch (i)
                {
                    case 0: X = value; return;
                    case 1: Y = value; return;
                    default: Z = value; return;
                }
            }
        }

        public int this[Axis i]
        {
            get
            {
                switch (i)
                {
                    case Axis.X: return X;
                    case Axis.Y: return Y;
                    default: return Z;
                }
            }
            set
            {
                switch (i)
                {
                    case Axis.X: X = value; return;
                    case Axis.Y: Y = value; return;
                    default: Z = value; return;
                }
            }
        }


        #region Operations

        public static Vector3I operator +(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }


        public static Vector3I operator -(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }


        public static Vector3I operator *(Vector3I a, int scalar)
        {
            return new Vector3I(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        public static Vector3I operator *(int scalar, Vector3I a)
        {
            return new Vector3I(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        public static Vector3F operator *(Vector3I a, float scalar)
        {
            return new Vector3F(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        public static Vector3F operator *(float scalar, Vector3I a)
        {
            return new Vector3F(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }


        /// <summary> Integer division! </summary>
        public static Vector3I operator /(Vector3I a, int scalar)
        {
            return new Vector3I(a.X / scalar, a.Y / scalar, a.Z / scalar);
        }

        public static Vector3F operator /(Vector3I a, float scalar)
        {
            return new Vector3F(a.X / scalar, a.Y / scalar, a.Z / scalar);
        }


        #endregion


        #region Equality

        public override bool Equals(object obj)
        {
            return obj is Vector3I i ? Equals(i) : base.Equals(obj);
        }

        public bool Equals(Vector3I other)
        {
            return (X == other.X) && (Y == other.Y) && (Z == other.Z);
        }


        public static bool operator ==(Vector3I a, Vector3I b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3I a, Vector3I b)
        {
            return !a.Equals(b);
        }


        public override int GetHashCode()
        {
            return X + Z * 1625 + Y * 2642245;
        }

        #endregion


        #region Comparison

        public int CompareTo(Vector3I other)
        {
            return Math.Sign(LengthSquared - other.LengthSquared);
        }

        public int CompareTo(Vector3F other)
        {
            return Math.Sign(LengthSquared - other.LengthSquared);
        }


        public static bool operator >(Vector3I a, Vector3I b)
        {
            return a.LengthSquared > b.LengthSquared;
        }

        public static bool operator <(Vector3I a, Vector3I b)
        {
            return a.LengthSquared < b.LengthSquared;
        }

        public static bool operator >=(Vector3I a, Vector3I b)
        {
            return a.LengthSquared >= b.LengthSquared;
        }

        public static bool operator <=(Vector3I a, Vector3I b)
        {
            return a.LengthSquared <= b.LengthSquared;
        }

        #endregion


        public int Dot(Vector3I b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z);
        }

        public float Dot(Vector3F b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z);
        }

        public Vector3I Cross(Vector3I b)
        {
            return new Vector3I((Y * b.Z) - (Z * b.Y),
                                 (Z * b.X) - (X * b.Z),
                                 (X * b.Y) - (Y * b.X));
        }

        public Vector3F Cross(Vector3F b)
        {
            return new Vector3F((Y * b.Z) - (Z * b.Y),
                                 (Z * b.X) - (X * b.Z),
                                 (X * b.Y) - (Y * b.X));
        }


        public Axis LongestAxis
        {
            get
            {
                int maxVal = Math.Max(Math.Abs(X), Math.Max(Math.Abs(Y), Math.Abs(Z)));
                if (maxVal == Math.Abs(X)) return Axis.X;
                if (maxVal == Math.Abs(Y)) return Axis.Y;
                return Axis.Z;
            }
        }

        public Axis ShortestAxis
        {
            get
            {
                int maxVal = Math.Min(Math.Abs(X), Math.Min(Math.Abs(Y), Math.Abs(Z)));
                if (maxVal == Math.Abs(X)) return Axis.X;
                if (maxVal == Math.Abs(Y)) return Axis.Y;
                return Axis.Z;
            }
        }


        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }

        public Vector3I Abs()
        {
            return new Vector3I(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        }

        public Vector3F Normalize()
        {
            if (X == 0 && Y == 0 && Z == 0) return Vector3F.Zero;
            float len = Length;
            return new Vector3F(X / len, Y / len, Z / len);
        }


        #region Conversion

        public static explicit operator Position(Vector3I a)
        {
            return new Position(a.X, a.Y, a.Z);
        }

        public static explicit operator Vector3F(Vector3I a)
        {
            return new Vector3F(a.X, a.Y, a.Z);
        }

        public Position ToPlayerCoords()
        {
            return new Position(X * 32 + 16, Y * 32 + 16, Z * 32 + 16);
        }

        #endregion
    }
}
