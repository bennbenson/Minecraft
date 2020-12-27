using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct PositionValue : IEquatable<PositionValue>
	{
		public PositionValue(PositionType type, int value)
		{
			if (type < PositionType.Absolute || type > PositionType.Local)
				throw new ArgumentOutOfRangeException(nameof(type), "Invalid PositionType value.");

			Type = type;
			Value = value;
		}


		public PositionType Type { get; }

		public int Value { get; }


		private string DebuggerDisplay => ToString();

		public static PositionValue Absolute(int value) => new PositionValue(PositionType.Absolute, value);

		public static PositionValue Relative(int value) => new PositionValue(PositionType.Relative, value);

		public static PositionValue Local(int value) => new PositionValue(PositionType.Local, value);


		public static bool operator ==(PositionValue left, PositionValue right) => left.Equals(right);

		public static bool operator !=(PositionValue left, PositionValue right) => !left.Equals(right);

		public static implicit operator PositionValue(int value) => new PositionValue(PositionType.Absolute, value);


		#region Object members
		public override int GetHashCode() => HashCode.Combine(Type, Value);

		public override bool Equals(object? obj) => obj is PositionValue other && Equals(other);

		public override string ToString()
		{
			return Type switch
			{
				PositionType.Local => Value == 0 ? "^" : $"^{Value}",
				PositionType.Relative => Value == 0 ? "~" : $"~{Value}",
				_ => Value.ToString()
			};
		}
		#endregion


		#region IEquatable<PositionValue> members
		public bool Equals(PositionValue other) => Type == other.Type && Value == other.Value;
		#endregion
	}
}
