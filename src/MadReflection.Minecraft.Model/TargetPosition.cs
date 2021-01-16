using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TargetPosition : Target, IEquatable<TargetPosition>
	{
		public TargetPosition(Position position)
		{
			Position = position;
		}

		public TargetPosition(PositionValue x, PositionValue y, PositionValue z)
		{
			Position = new Position(x, y, z);
		}


		public Position Position { get; }

		public PositionValue X => Position.X;

		public PositionValue Y => Position.Y;

		public PositionValue Z => Position.Z;

		protected override Type EqualityContract => typeof(TargetPosition);

		private string DebuggerDisplay => ToString();


		public override string GetArgumentText(Edition edition) => Position.GetArgumentText(edition);


		public static implicit operator TargetPosition(Position position) => new TargetPosition(position);

		public static implicit operator TargetPosition((int x, int y, int z) coord) => new TargetPosition(Position.Absolute(coord.x, coord.y, coord.z));

		public static explicit operator Position(TargetPosition position) => position?.Position ?? throw new InvalidCastException("Cannot cast null to a Position instance.");


		#region Object members
		public override int GetHashCode() => Position.GetHashCode();

		public override bool Equals(object? obj) => obj is TargetPosition other && Equals(other);

		public override string ToString() => Position.ToString();
		#endregion


		#region IEquatable<TargetLocation> members
		public bool Equals(TargetPosition? other) => other is not null && EqualityContract == other.EqualityContract && X == other.X && Y == other.Y && Z == other.Z;
		#endregion
	}
}
