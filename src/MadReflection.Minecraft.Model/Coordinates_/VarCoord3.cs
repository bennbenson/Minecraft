using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct VarCoord3 : IEquatable<VarCoord3>, IArgumentText
	{
		public VarCoord3(int? x, int? y, int? z)
		{
			if (y < 0 || y > 255)
				throw new ArgumentOutOfRangeException(nameof(y), "Y cannot be negative or greater than 255.");

			X = x;
			Y = y;
			Z = z;
		}


		public int? X { get; }

		public int? Y { get; }

		public int? Z { get; }

		private string DebuggerDisplay => ToString();


		public VarCoord3 WithX(int? x) => new VarCoord3(x, Y, Z);

		public VarCoord3 WithY(int? y) => new VarCoord3(X, y, Z);

		public VarCoord3 WithZ(int? z) => new VarCoord3(X, Y, z);

		public void Deconstruct(out int? x, out int? y, out int? z) => (x, y, z) = (X, Y, Z);

		public static VarCoord3 Parse(string s) => InternalTryParse(s, out VarCoord3 result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out VarCoord3 result) => InternalTryParse(s, out result, out _, false);

		public static bool InternalTryParse(string s, out VarCoord3 result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^\((?<x>-?[1-9][0-9]*|\*|x\?), ?(?<y>-?[1-9][0-9]*|\*|y\?), ?(?<z>-?[1-9][0-9]*|\*|z\?)\)$");
			if (match.Success)
			{
				string? xValue = match.Groups["x"].Value;
				int? x = xValue is "*" or "x?" ? null : int.Parse(xValue);

				string? yValue = match.Groups["y"].Value;
				int? y = yValue is "*" or "y?" ? null : int.Parse(yValue);

				string? zValue = match.Groups["z"].Value;
				int? z = zValue is "*" or "z?" ? null : int.Parse(zValue);

				result = new VarCoord3(x, y, z);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static VarCoord3 At(int? x, int? y, int? z) => new VarCoord3(x, y, z);

		public string ToStringQ() => $"({X?.ToString() ?? "x?"},{Y?.ToString() ?? "y?"},{Z?.ToString() ?? "z?"})";


		public static bool operator ==(VarCoord3 left, VarCoord3 right) => left.Equals(right);

		public static bool operator !=(VarCoord3 left, VarCoord3 right) => !left.Equals(right);

		public static implicit operator VarCoord3(Coord3 coord) => new VarCoord3(coord.X, coord.Y, coord.Z);

		public static explicit operator Coord3(VarCoord3 coord)
		{
			if (coord.X is not int x || coord.Y is not int y || coord.Z is not int z)
				throw new InvalidCastException();

			return new Coord3(x, y, z);
		}

		public static implicit operator VarCoord3(Coord2 coord) => new VarCoord3(coord.X, null, coord.Z);

		public static explicit operator Coord2(VarCoord3 coord) => coord.X is not int x || coord.Z is not int z ? throw new InvalidCastException("Cannot cast VarCoord3 with unknown X or Z to Coord2.") : new Coord2(x, z);


		#region Object members
		public override int GetHashCode() => (X ?? 0) ^ ((Y ?? 0) << 8) ^ ((Z ?? 0) << 16);

		public override bool Equals(object? obj) => obj is VarCoord3 other && Equals(other);

		public override string ToString() => $"({X?.ToString() ?? "*"},{Y?.ToString() ?? "*"},{Z?.ToString() ?? "*"})";
		#endregion


		#region IEquatable<VarCoord> members
		public bool Equals(VarCoord3 other) => X == other.X && Y == other.Y && Z == other.Z;
		#endregion


		#region IArgumentText members
		public string GetArgumentText(MinecraftEdition edition) => $"{X?.ToString() ?? "x?"} {Y?.ToString() ?? "y?"} {Z?.ToString() ?? "z?"}";
		#endregion
	}
}
