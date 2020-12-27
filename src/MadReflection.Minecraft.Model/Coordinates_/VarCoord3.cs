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
			X = x;
			Y = y;
			Z = z;
		}


		public int? X { get; }

		public int? Y { get; }

		public int? Z { get; }


		public VarCoord3 WithX(int? x) => new VarCoord3(x, Y, Z);

		public VarCoord3 WithY(int? y) => new VarCoord3(X, y, Z);

		public VarCoord3 WithZ(int? z) => new VarCoord3(X, Y, z);

		private string DebuggerDisplay => ToString();


		public static VarCoord3 Parse(string s) => InternalTryParse(s, out VarCoord3 result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out VarCoord3 result) => InternalTryParse(s, out result, out _, false);

		public static bool InternalTryParse(string s, out VarCoord3 result, out Exception? exception, bool needException)
		{
			Match match = Regex.Match(s, @"^\((?<x>-?[0-9]+|\*),(?<y>-?[0-9]+|\*),(?<z>-?[0-9]+|\*)\)$");
			if (match.Success)
			{
				string? xValue = match.Groups["x"].Value;
				int? x = xValue == "*" ? null : int.Parse(xValue);

				string? yValue = match.Groups["y"].Value;
				int? y = yValue == "*" ? null : int.Parse(yValue);

				string? zValue = match.Groups["z"].Value;
				int? z = zValue == "*" ? null : int.Parse(zValue);

				result = new VarCoord3(x, y, z);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

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


		#region Object members
		public override int GetHashCode() => (X ?? 0) ^ ((Y ?? 0) << 8) ^ ((Z ?? 0) << 16);

		public override bool Equals(object? obj) => obj is VarCoord3 other && Equals(other);

		public override string ToString() => $"({X?.ToString() ?? "*"},{Y?.ToString() ?? "*"},{Z?.ToString() ?? "*"})";
		#endregion


		#region IEquatable<VarCoord> members
		public bool Equals(VarCoord3 other) => X == other.X && Y == other.Y && Z == other.Z;
		#endregion


		#region IArgumentText members
		public string ArgumentText => $"{X?.ToString() ?? "x?"} {Y?.ToString() ?? "y?"} {Z?.ToString() ?? "z?"}";
		#endregion
	}
}
