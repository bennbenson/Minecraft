using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct Coord3 : IEquatable<Coord3>, IArgumentText
	{
		public Coord3(int x, int y, int z)
		{
			if (y < 0 || y > 255)
				throw new ArgumentOutOfRangeException(nameof(y), "Y cannot be negative or greater than 255.");

			X = x;
			Y = y;
			Z = z;
		}


		public int X { get; }

		public int Y { get; }

		public int Z { get; }

		private string DebuggerDisplay => ToString();


		public Coord3 WithX(int x) => new Coord3(x, Y, Z);

		public Coord3 WithY(int y) => new Coord3(X, y, Z);

		public Coord3 WithZ(int z) => new Coord3(X, Y, z);

		public Coord3 Add(int dx, int dy, int dz) => new Coord3(X + dx, Y + dy, Z + dz);

		public Coord3 AddX(int dx) => new Coord3(X + dx, Y, Z);

		public Coord3 AddY(int dy) => new Coord3(X, CheckYOverflow(Y + dy), Z);

		public Coord3 AddZ(int dz) => new Coord3(X, Y, Z + dz);

		public void Deconstruct(out int x, out int y, out int z) => (x, y, z) = (X, Y, Z);

		public static Coord3 Parse(string s) => InternalTryParse(s, out Coord3 result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out Coord3 result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, out Coord3 result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^\((?<x>-?[0-9]+), ?(?<y>-?[0-9]+), ?(?<z>-?[0-9]+)\)$");
			if (match.Success)
			{
				int x = int.Parse(match.Groups["x"].Value);
				int y = int.Parse(match.Groups["y"].Value);
				int z = int.Parse(match.Groups["z"].Value);

				result = new Coord3(x, y, z);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static Coord3 At(int x, int y, int z) => new Coord3(x, y, z);

		private static int CheckYOverflow(int y) => y is >= 0 and <= 255 ? y : throw new OverflowException("Y coordinate range overflow.");


		public static bool operator ==(Coord3 a, Coord3 b) => a.Equals(b);

		public static bool operator !=(Coord3 a, Coord3 b) => !a.Equals(b);

		public static Coord3 operator +(Coord3 coord, (int x, int y, int z) delta) => new Coord3(coord.X + delta.x, CheckYOverflow(coord.Y + delta.y), coord.Z + delta.z);

		public static Coord3 operator -(Coord3 coord, (int x, int y, int z) delta) => new Coord3(coord.X - delta.x, CheckYOverflow(coord.Y - delta.y), coord.Z - delta.z);


		#region Object members
		public override int GetHashCode() => X ^ (Y << 8) ^ (Z << 16);

		public override bool Equals(object? obj) => obj is Coord3 other && Equals(other);

		public override string ToString() => $"({X},{Y},{Z})";
		#endregion


		#region IEquatable<Coordinate> members
		public bool Equals(Coord3 other) => X == other.X && Y == other.Y && Z == other.Z;
		#endregion


		#region IArgumentText members
		public string GetArgumentText(MinecraftEdition edition) => $"{X} {Y} {Z}";
		#endregion
	}
}
