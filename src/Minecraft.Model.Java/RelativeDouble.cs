using System;
using System.Diagnostics;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct RelativeDouble : IEquatable<RelativeDouble>, IArgumentText
	{
		public RelativeDouble(double value)
			: this(PositionType.Absolute, value)
		{
		}

		public RelativeDouble(PositionType type, double value)
		{
			if (type < PositionType.Absolute || type > PositionType.Local)
				throw new ArgumentOutOfRangeException(nameof(type), "Invalid PositionType value.");

			Value = value;
			Type = type;
		}


		public PositionType Type { get; }

		public double Value { get; }

		private string DebuggerDisplay => ToString();


		public static RelativeDouble Parse(string s) => InternalTryParse(s, out RelativeDouble result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out RelativeDouble result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, out RelativeDouble result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			if (s.Length > 0)
			{
				char ch0 = s[0];
				if (ch0 is '~' or '^')
				{
					s = s[1..];

					if (double.TryParse(s, out double value))
					{
						result = new RelativeDouble(ch0 == '~' ? PositionType.Relative : PositionType.Local, value);
						exception = null;
						return true;
					}
				}
				else
				{
					if (double.TryParse(s, out double value))
					{
						result = new RelativeDouble(PositionType.Absolute, value);
						exception = null;
						return true;
					}
				}
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static RelativeDouble Absolute(double value) => new RelativeDouble(PositionType.Absolute, value);

		public static RelativeDouble Relative(double value) => new RelativeDouble(PositionType.Relative, value);

		public static RelativeDouble Local(double value) => new RelativeDouble(PositionType.Local, value);


		public static bool operator ==(RelativeDouble left, RelativeDouble right) => left.Equals(right);

		public static bool operator !=(RelativeDouble left, RelativeDouble right) => !left.Equals(right);

		public static implicit operator RelativeDouble(double value) => new RelativeDouble(value);

		public static explicit operator double(RelativeDouble value) => value.Type == PositionType.Absolute ? value.Value : throw new InvalidCastException($"Cannot cast a relative or local {nameof(RelativeDouble)} instance to {nameof(Double)}.");


		#region Object members
		public override int GetHashCode() => HashCode.Combine(Type, Value);

		public override bool Equals(object? obj) => obj is RelativeDouble other && Equals(other);

		public override string ToString()
		{
			return Type switch
			{
				PositionType.Local => Value == 0.0f ? "^" : $"^{Value:0.##}",
				PositionType.Relative => Value == 0.0f ? "~" : $"~{Value:0.##}",
				_ => Value.ToString("0.##"),
			};
		}
		#endregion


		#region IEquatable<RelativeDouble> members
		public bool Equals(RelativeDouble other) => Type == other.Type && Value == other.Value;
		#endregion


		#region IArgumentText members
		public string GetArgumentText() => ToString();
		#endregion
	}
}
