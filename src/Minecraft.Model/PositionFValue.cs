using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct PositionFValue : IEquatable<PositionFValue>, IArgumentText
	{
		public PositionFValue(float value)
			: this(PositionType.Absolute, value)
		{
		}

		public PositionFValue(PositionType type, float value)
		{
			if (type < PositionType.Absolute || type > PositionType.Local)
				throw new ArgumentOutOfRangeException(nameof(type), $"Invalid {nameof(PositionType)} value.");

			Type = type;
			Value = value;
		}


		public PositionType Type { get; }

		public float Value { get; }

		private string DebuggerDisplay => ToString();


		public static PositionFValue Parse(string s) => InternalTryParse(s, out PositionFValue result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out PositionFValue result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, out PositionFValue result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^((?<p>\^|\~)|(?<v>0)|(?<p>[~^])?(?<v>[-+]?[0-9]+(\.[0-9]+)?))$");
			if (match.Success)
			{
				Group valueGroup = match.Groups["v"];
				float value = 0.0f;
				if (valueGroup.Success && !float.TryParse(valueGroup.Value, out value))
				{
					result = default;
					exception = needException ? new FormatException() : null;
					return false;
				}

				Group prefixGroup = match.Groups["p"];
				PositionType type = !prefixGroup.Success ? PositionType.Absolute : prefixGroup.Value[0] switch
				{
					'^' => PositionType.Local,
					'~' => PositionType.Relative,
					_ => PositionType.Absolute
				};

				result = new PositionFValue(type, value);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static PositionFValue Absolute(float value) => new PositionFValue(PositionType.Absolute, value);

		public static PositionFValue Relative(float value) => new PositionFValue(PositionType.Relative, value);

		public static PositionFValue Local(float value) => new PositionFValue(PositionType.Local, value);


		public static bool operator ==(PositionFValue left, PositionFValue right) => left.Equals(right);

		public static bool operator !=(PositionFValue left, PositionFValue right) => !left.Equals(right);

		public static implicit operator PositionFValue(float value) => new PositionFValue(PositionType.Absolute, value);

		public static explicit operator float(PositionFValue value) => value.Type == PositionType.Absolute ? value.Value : throw new InvalidCastException($"Cannot cast a relative or local {nameof(PositionFValue)} instance to Single.");


		#region Object members
		public override int GetHashCode() => HashCode.Combine(Type, Value);

		public override bool Equals(object? obj) => obj is PositionFValue other && Equals(other);

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


		#region IEquatable<PositionFValue> members
		public bool Equals(PositionFValue other) => Type == other.Type && Value == other.Value;
		#endregion


		#region IArgumentText members
		public string GetArgumentText() => ToString();
		#endregion
	}
}
