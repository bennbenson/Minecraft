using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct PositionF : IEquatable<PositionF>, IArgumentText
	{
		public PositionF(int x, int y, int z)
		{
			if (y < 0)
				throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be negative.");
			if (y > 255)
				throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be greater than 255.");

			X = PositionFValue.Absolute(x);
			Y = PositionFValue.Absolute(y);
			Z = PositionFValue.Absolute(z);
		}

		public PositionF(PositionFValue x, PositionFValue y, PositionFValue z)
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
				if (y.Value < 0f)
					throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be negative.");
				if (y.Value > 255f)
					throw new ArgumentOutOfRangeException(nameof(y), "Absolute Y coordinate cannot be greater than 255.");
			}

			X = x;
			Y = y;
			Z = z;
		}


		public PositionFValue X { get; }

		public PositionFValue Y { get; }

		public PositionFValue Z { get; }

		private string DebuggerDisplay => ToString();


		public PositionF WithX(PositionFValue x) => new PositionF(x, Y, Z);

		public PositionF WithY(PositionFValue y) => new PositionF(X, y, Z);

		public PositionF WithZ(PositionFValue z) => new PositionF(X, Y, z);

		public void Deconstruct(out PositionFValue x, out PositionFValue y, out PositionFValue z) => (x, y, z) = (X, Y, Z);

		public static PositionF Get(PositionFValue x, PositionFValue y, PositionFValue z) => new PositionF(x, y, z);

		public static PositionF Absolute(float x, float y, float z) => new PositionF(PositionFValue.Absolute(x), PositionFValue.Absolute(y), PositionFValue.Absolute(z));

		public static PositionF Relative(float x, float y, float z) => new PositionF(PositionFValue.Relative(x), PositionFValue.Relative(y), PositionFValue.Relative(z));

		public static PositionF Local(float x, float y, float z) => new PositionF(PositionFValue.Local(x), PositionFValue.Local(y), PositionFValue.Local(z));

		public static PositionF Parse(string s) => InternalTryParse(s, out PositionF result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out PositionF result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, out PositionF result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^(?<x>(?<xp>\^|\~)|(?<xv>0)|(?<xp>[~^])?(?<xv>[-+]?[0-9]+(\.[0-9]+)?)), ?(?<y>(?<yp>\^|\~)|(?<yv>0)|(?<yp>[~^])?(?<yv>[-+]?[0-9]+(\.[0-9]+)?)), ?(?<z>(?<zp>\^|\~)|(?<zv>0)|(?<zp>[~^])?(?<zv>[-+]?[0-9]+(\.[0-9]+)?))$");
			if (match.Success)
			{
				PositionFValue x = PositionFValue.Parse(match.Groups["x"].Value);
				PositionFValue y = PositionFValue.Parse(match.Groups["y"].Value);
				PositionFValue z = PositionFValue.Parse(match.Groups["z"].Value);

				result = new PositionF(x, y, z);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static bool operator ==(PositionF left, PositionF right) => left.Equals(right);

		public static bool operator !=(PositionF left, PositionF right) => !left.Equals(right);

		public static implicit operator PositionF(Coord3 coord) => Absolute(coord.X, coord.Y, coord.Z);

		public static implicit operator PositionF((float x, float y, float z) coord) => Absolute(coord.x, coord.y, coord.z);

		public static implicit operator PositionF((PositionFValue x, PositionFValue y, PositionFValue z) coord)=> new PositionF(coord.x, coord.y,coord.z);

		public static explicit operator Coord3(PositionF position)
		{
			if (position.X.Type != PositionType.Absolute || position.Y.Type != PositionType.Absolute || position.Z.Type != PositionType.Absolute)
				throw new InvalidCastException($"Unable to cast non-absolute {nameof(PositionF)} instance to {nameof(Coord3)}.");

			return new Coord3((int)position.X.Value, (int)position.Y.Value, (int)position.Z.Value);
		}


		#region Object members
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);

		public override bool Equals(object? obj) => obj is PositionF other && Equals(other);

		public override string ToString() => $"{X}, {Y}, {Z}";
		#endregion


		#region IEquatable<PositionF> members
		public bool Equals(PositionF other) => X == other.X && Y == other.Y && Z == other.Z;
		#endregion


		#region IArgumentText members
		public string GetArgumentText() => $"{X.GetArgumentText()} {Y.GetArgumentText()} {Z.GetArgumentText()}";
		#endregion
	}
}
