﻿using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct Coord2 : IEquatable<Coord2>, IArgumentText
	{
		public Coord2(int x, int z)
		{
			X = x;
			Z = z;
		}


		public int X { get; }

		public int Z { get; }

		private string DebuggerDisplay => ToString();


		public Coord2 WithX(int x) => new Coord2(x, Z);

		public Coord2 WithZ(int z) => new Coord2(X, z);

		public Coord3 AtY(int y) => new Coord3(X, y, Z);

		public Coord2 Add(int dx, int dz) => new Coord2(X + dx, Z + dz);

		public Coord2 AddX(int dx) => new Coord2(X + dx, Z);

		public Coord2 AddZ(int dz) => new Coord2(X, Z + dz);

		public void Deconstruct(out int x, out int z) => (x, z) = (X, Z);

		public static Coord2 Parse(string s) => InternalTryParse(s, out Coord2 result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out Coord2 result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, out Coord2 result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^\((?<x>-?[0-9]+), ?(?<z>-?[0-9]+)\)$");
			if (match.Success)
			{
				int x = int.Parse(match.Groups["x"].Value);
				int z = int.Parse(match.Groups["z"].Value);

				result = new Coord2(x, z);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static Coord2 At(int x, int z) => new Coord2(x, z);

		public static Coord2 Min(Coord2 a, Coord2 b) => new Coord2(Math.Min(a.X, b.X), Math.Min(a.Z, b.Z));

		public static Coord2 Max(Coord2 a, Coord2 b) => new Coord2(Math.Max(a.X, b.X), Math.Max(a.Z, b.Z));


		public static bool operator ==(Coord2 a, Coord2 b) => a.Equals(b);

		public static bool operator !=(Coord2 a, Coord2 b) => !a.Equals(b);

		public static Coord2 operator +(Coord2 coord, (int x, int z) delta) => new Coord2(coord.X + delta.x, coord.Z + delta.z);

		public static Coord2 operator -(Coord2 coord, (int x, int z) delta) => new Coord2(coord.X - delta.x, coord.Z - delta.z);

		public static explicit operator Coord2(Coord3 coord) => new Coord2(coord.X, coord.Z);  // Inverse of this is At(y).


		#region Object members
		public override int GetHashCode() => X ^ (Z << 16);

		public override bool Equals(object? obj) => obj is Coord2 other && Equals(other);

		public override string ToString() => $"({X},{Z})";
		#endregion


		#region IEquatable<Coord2> members
		public bool Equals(Coord2 other) => X == other.X && Z == other.Z;
		#endregion


		#region IArgumentText members
		public string GetArgumentText() => $"{X} ~ {Z}";  // Not sure this is correct.
		#endregion
	}
}
