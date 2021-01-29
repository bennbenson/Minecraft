using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct Position : IEquatable<Position>, IArgumentText
	{
		public Position(int x, int y, int z)
		{
			if (y < 0)
				throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be negative.");
			if (y > 255)
				throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be greater than 255.");

			X = PositionValue.Absolute(x);
			Y = PositionValue.Absolute(y);
			Z = PositionValue.Absolute(z);
		}

		public Position(PositionValue x, PositionValue y, PositionValue z)
		{
			if (x.Type == PositionType.Local || y.Type == PositionType.Local || z.Type == PositionType.Local)
			{
				string? badTypeName = null;
				if (x.Type != PositionType.Local)
					badTypeName = nameof(x);
				if (y.Type != PositionType.Local)
					badTypeName = nameof(y);
				if (z.Type != PositionType.Local)
					badTypeName = nameof(z);
				if (badTypeName is not null)
					throw new ArgumentException("If any coordinate is local, all must be local.", badTypeName);
			}

			if (y.Type == PositionType.Absolute)
			{
				if (y.Value < 0)
					throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be negative.");
				if (y.Value > 255)
					throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be greater than 255.");
			}

			X = x;
			Y = y;
			Z = z;
		}


		public PositionValue X { get; }

		public PositionValue Y { get; }

		public PositionValue Z { get; }

		private string DebuggerDisplay => ToString();


		public Position WithX(PositionValue x) => new Position(x, Y, Z);

		public Position WithY(PositionValue y) => new Position(X, y, Z);

		public Position WithZ(PositionValue z) => new Position(X, Y, z);

		public void Deconstruct(out PositionValue x, out PositionValue y, out PositionValue z) => (x, y, z) = (X, Y, Z);

		public static Position Get(PositionValue x, PositionValue y, PositionValue z) => new Position(x, y, z);

		public static Position Absolute(int x, int y, int z) => new Position(PositionValue.Absolute(x), PositionValue.Absolute(y), PositionValue.Absolute(z));

		public static Position Relative(int x, int y, int z) => new Position(PositionValue.Relative(x), PositionValue.Relative(y), PositionValue.Relative(z));

		public static Position Local(int x, int y, int z) => new Position(PositionValue.Local(x), PositionValue.Local(y), PositionValue.Local(z));

		public static Position Parse(string s) => InternalTryParse(s, out Position result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out Position result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, out Position result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^(?<x>[~^]|[~^]?[+-]?[0-9]+), ?(?<y>[~^]|[~^]?[+-]?[0-9]+), ?(?<z>[~^]|[~^]?[+-]?[0-9]+)$");
			if (match.Success)
			{
				PositionValue x = PositionValue.Parse(match.Groups["x"].Value);
				PositionValue y = PositionValue.Parse(match.Groups["y"].Value);
				PositionValue z = PositionValue.Parse(match.Groups["z"].Value);

				result = new Position(x, y, z);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static bool operator ==(Position left, Position right) => left.Equals(right);

		public static bool operator !=(Position left, Position right) => !left.Equals(right);

		public static implicit operator Position(Coord3 coord) => Absolute(coord.X, coord.Y, coord.Z);

		public static implicit operator Position((int x, int y, int z) coord) => Absolute(coord.x, coord.y, coord.z);

		public static implicit operator Position((PositionValue x, PositionValue y, PositionValue z) coord) => new Position(coord.x, coord.y, coord.z);

		public static explicit operator Coord3(Position position)
		{
			if (position.X.Type != PositionType.Absolute || position.Y.Type != PositionType.Absolute || position.Z.Type != PositionType.Absolute)
				throw new InvalidCastException($"Unable to cast non-absolute {nameof(Position)} instance to {nameof(Coord3)}.");

			return new Coord3(position.X.Value, position.Y.Value, position.Z.Value);
		}


		#region Object members
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);

		public override bool Equals(object? obj) => obj is Position other && Equals(other);

		public override string ToString() => $"{X}, {Y}, {Z}";
		#endregion


		#region IEquatable<Position> members
		public bool Equals(Position other) => X == other.X && Y == other.Y && Z == other.Z;
		#endregion


		#region IArgumentText members
		public string GetArgumentText() => $"{X.GetArgumentText()} {Y.GetArgumentText()} {Z.GetArgumentText()}";
		#endregion
	}

	//[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	//public readonly struct ColumnPosition : IEquatable<ColumnPosition>
	//{
	//	// ColumnPosition is X Z, like Coord2, but supporting PositionValue.
	//}
}
