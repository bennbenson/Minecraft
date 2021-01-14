using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct Rotation : IEquatable<Rotation>, IArgumentText
	{
		public Rotation(RelativeFloat y, RelativeFloat x)
		{
			Y = y;
			X = x;
		}


		public RelativeFloat Y { get; }

		public RelativeFloat X { get; }

		private string DebuggerDisplay => ToString();


		public void Deconstruct(out RelativeFloat y, out RelativeFloat x) => (x, y) = (X, Y);

		public static implicit operator Rotation((float x, float y) value) => new Rotation(value.y, value.x);

		public static implicit operator Rotation((RelativeFloat y, RelativeFloat x) value) => new Rotation(value.y, value.x);

		public static bool operator ==(Rotation left, Rotation right) => left.Equals(right);

		public static bool operator !=(Rotation left, Rotation right) => !left.Equals(right);


		#region Object members
		public override int GetHashCode() => HashCode.Combine(Y, X);

		public override bool Equals(object? obj) => obj is Rotation other && Equals(other);

		public override string ToString() => $"{Y} {X}";
		#endregion


		#region IEquatable<Rotation> members
		public bool Equals(Rotation other) => Y == other.Y && X == other.X;
		#endregion


		#region IArgumentText members
		public string GetArgumentText(Edition edition) => ToString();
		#endregion
	}
}
