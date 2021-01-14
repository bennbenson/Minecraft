using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	internal readonly struct BrigadierBool : IEquatable<BrigadierBool>
	{
		public BrigadierBool(bool value)
		{
			Value = value;
		}


		public bool Value { get; }

		private string DebuggerDisplay => ToString();


		public static bool operator ==(BrigadierBool left, BrigadierBool right) => left.Equals(right);

		public static bool operator !=(BrigadierBool left, BrigadierBool right) => !left.Equals(right);

		public static implicit operator bool(BrigadierBool value) => value.Value;

		public static explicit operator BrigadierBool(bool value) => new BrigadierBool(value);

		public static bool operator true(BrigadierBool value) => value.Value;

		public static bool operator false(BrigadierBool value) => !value.Value;


		#region Object members
		public override int GetHashCode() => Value.GetHashCode();

		public override bool Equals(object? obj) => obj is BrigadierBool other && Equals(other);

		public override string ToString() => Value ? "true" : "false";
		#endregion


		#region IEquatable<BrigadierBool> members
		public bool Equals(BrigadierBool other) => Value == other.Value;
		#endregion
	}
}
