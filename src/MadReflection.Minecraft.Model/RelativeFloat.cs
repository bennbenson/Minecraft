using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct RelativeFloat : IEquatable<RelativeFloat>, IArgumentText
	{
		public RelativeFloat(float value)
			: this(PositionType.Absolute, value)
		{
		}

		public RelativeFloat(PositionType type, float value)
		{
			if (type < PositionType.Absolute || type > PositionType.Local)
				throw new ArgumentOutOfRangeException(nameof(type), "Invalid PositionType value.");

			Value = value;
			Type = type;
		}


		public PositionType Type { get; }

		public float Value { get; }

		private string DebuggerDisplay => ToString();


		public static RelativeFloat Parse(string s) => InternalTryParse(s, out RelativeFloat result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out RelativeFloat result) => InternalTryParse(s, out result, out _, false);

		public static bool InternalTryParse(string s, out RelativeFloat result, out Exception? exception, bool needException)
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

					if (float.TryParse(s, out float value))
					{
						result = new RelativeFloat(ch0 == '~' ? PositionType.Relative : PositionType.Local, value);
						exception = null;
						return true;
					}
				}
				else
				{
					if (float.TryParse(s, out float value))
					{
						result = new RelativeFloat(PositionType.Absolute, value);
						exception = null;
						return true;
					}
				}
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static RelativeFloat Absolute(float value) => new RelativeFloat(PositionType.Absolute, value);

		public static RelativeFloat Relative(float value) => new RelativeFloat(PositionType.Relative, value);

		public static RelativeFloat Local(float value) => new RelativeFloat(PositionType.Local, value);


		public static bool operator ==(RelativeFloat left, RelativeFloat right) => left.Equals(right);

		public static bool operator !=(RelativeFloat left, RelativeFloat right) => !left.Equals(right);

		public static implicit operator RelativeFloat(float value) => new RelativeFloat(value);

		public static explicit operator float(RelativeFloat value) => value.Type == PositionType.Absolute ? value.Value : throw new InvalidCastException($"Cannot cast a relative or loca {nameof(RelativeFloat)} instance to Single.");


		#region Object members
		public override int GetHashCode() => HashCode.Combine(Type, Value);

		public override bool Equals(object? obj) => obj is RelativeFloat other && Equals(other);

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


		#region IEquatable<RelativeFloat> members
		public bool Equals(RelativeFloat other) => Type == other.Type && Value == other.Value;
		#endregion


		#region IArgumentText members
		public string GetArgumentText(Edition edition) => ToString();
		#endregion
	}
}
