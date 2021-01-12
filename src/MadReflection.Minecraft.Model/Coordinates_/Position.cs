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

		private Position(bool _, PositionValue x, PositionValue y, PositionValue z)
		{
			X = x;
			Y = y;
			Z = z;
		}


		public PositionValue X { get; }

		public PositionValue Y { get; }

		public PositionValue Z { get; }

		public string DebuggerDisplay => ToString();


		public static Position Get(PositionValue x, PositionValue y, PositionValue z) => new Position(x, y, z);

		public static Position Absolute(int x, int y, int z) => new Position(false, PositionValue.Absolute(x), PositionValue.Absolute(y), PositionValue.Absolute(z));

		public static Position Relative(int x, int y, int z) => new Position(false, PositionValue.Relative(x), PositionValue.Relative(y), PositionValue.Relative(z));

		public static Position Local(int x, int y, int z) => new Position(false, PositionValue.Local(x), PositionValue.Local(y), PositionValue.Local(z));

		public static Position Parse(string s) => InternalTryParse(s, out Position result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out Position result) => InternalTryParse(s, out result, out _, false);

		public static bool InternalTryParse(string s, out Position result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^(?<x>(?<xp>\^|\~)|(?<xv>0)|(?<xp>[~^])?(?<xv>[-+]?[1-9][0-9]*)), ?(?<y>(?<yp>\^|\~)|(?<yv>0)|(?<yp>[~^])?(?<yv>[-+]?[1-9][0-9]*)), ?(?<z>(?<zp>\^|\~)|(?<zv>0)|(?<zp>[~^])?(?<zv>[-+]?[1-9][0-9]*))$");
			if (match.Success)
			{
				PositionValue x = PositionValue.Parse(match.Groups["x"].Value);
				PositionValue y = PositionValue.Parse(match.Groups["y"].Value);
				PositionValue z = PositionValue.Parse(match.Groups["z"].Value);

				result = new Position(x, y, z);
				exception = null;
				return true;
			}

			//Match match = Regex.Match(s, @"^((?<xp>\^|\~)|(?<xv>0)|(?<xp>[~^])?(?<xv>[-+]?[1-9][0-9]*)), ?((?<yp>\^|\~)|(?<yv>0)|(?<yp>[~^])?(?<yv>[-+]?[1-9][0-9]*)), ?((?<zp>\^|\~)|(?<zv>0)|(?<zp>[~^])?(?<zv>[-+]?[1-9][0-9]*))$");
			//if (match.Success)
			//{
			//	PositionValue xValue = GetPositionValue(match.Groups["xp"], match.Groups["xv"]);
			//	PositionValue yValue = GetPositionValue(match.Groups["yp"], match.Groups["yv"]);
			//	PositionValue zValue = GetPositionValue(match.Groups["zp"], match.Groups["zv"]);
			//	result = new Position(xValue, yValue, zValue);
			//	exception = null;
			//	return true;
			//}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;

			//static PositionValue GetPositionValue(Group prefixGroup, Group valueGroup)
			//{
			//	int value = valueGroup.Success ? int.Parse(valueGroup.Value) : 0;
			//	char prefix = prefixGroup.Success ? prefixGroup.Value[0] : default;

			//	return prefix switch
			//	{
			//		'~' => PositionValue.Relative(value),
			//		'^' => PositionValue.Local(value),
			//		_ => PositionValue.Absolute(value)
			//	};
			//}
		}

		public static bool operator ==(Position left, Position right) => left.Equals(right);

		public static bool operator !=(Position left, Position right) => !left.Equals(right);

		public static implicit operator Position(Coord3 coord) => Absolute(coord.X, coord.Y, coord.Z);

		public static explicit operator Coord3(Position position)
		{
			if (position.X.Type != PositionType.Absolute || position.Y.Type != PositionType.Absolute || position.Z.Type != PositionType.Absolute)
				throw new InvalidCastException($"Unable to cast non-absolute {nameof(Position)} instance to {nameof(Coord3)}.");

			return new Coord3(position.X.Value, position.Y.Value, position.Z.Value);
		}

		public static explicit operator Position(VarCoord3 coord)
		{
			if (coord.X is not int x || coord.Y is not int y || coord.Z is not int z)
				throw new InvalidCastException($"Unable to cast inexact {nameof(VarCoord3)} instance to {nameof(Position)}.");

			return new Position(x, y, z);
		}

		public static explicit operator VarCoord3(Position position)
		{
			if (position.X.Type != PositionType.Absolute || position.Y.Type != PositionType.Absolute || position.Z.Type != PositionType.Absolute)
				throw new InvalidCastException($"Unable to cast non-absolute {nameof(Position)} instance to {nameof(Coord3)}.");

			return new VarCoord3(position.X.Value, position.Y.Value, position.Z.Value);
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
		public string GetArgumentText(MinecraftEdition edition) => $"{X} {Y} {Z}";
		#endregion
	}
}
