using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct PositionValue : IEquatable<PositionValue>, IArgumentText
	{
		public PositionValue(int value)
			: this(PositionType.Absolute, value)
		{
		}

		public PositionValue(PositionType type, int value)
		{
			if (type < PositionType.Absolute || type > PositionType.Local)
				throw new ArgumentOutOfRangeException(nameof(type), $"Invalid {nameof(PositionType)} value.");

			Type = type;
			Value = value;
		}


		public PositionType Type { get; }

		public int Value { get; }

		private string DebuggerDisplay => ToString();


		public static PositionValue Parse(string s) => InternalTryParse(s, out PositionValue result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, out PositionValue result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, out PositionValue result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^((?<p>[~^])|(?<p>[~^])?(?<v>[+-]?[0-9]+))$");
			if (match.Success)
			{
				Group valueGroup = match.Groups["v"];
				int value = valueGroup.Success ? int.Parse(valueGroup.Value) : 0;

				Group prefixGroup = match.Groups["p"];
				PositionType type = !prefixGroup.Success ? PositionType.Absolute : prefixGroup.Value[0] switch
				{
					'^' => PositionType.Local,
					'~' => PositionType.Relative,
					_ => PositionType.Absolute
				};

				result = new PositionValue(type, value);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static PositionValue Absolute(int value) => new PositionValue(PositionType.Absolute, value);

		public static PositionValue Relative(int value) => new PositionValue(PositionType.Relative, value);

		public static PositionValue Local(int value) => new PositionValue(PositionType.Local, value);


		public static bool operator ==(PositionValue left, PositionValue right) => left.Equals(right);

		public static bool operator !=(PositionValue left, PositionValue right) => !left.Equals(right);

		public static implicit operator PositionValue(int value) => new PositionValue(PositionType.Absolute, value);

		public static explicit operator int(PositionValue value) => value.Type == PositionType.Absolute ? value.Value : throw new InvalidCastException($"Cannot cast a relative or local {nameof(PositionValue)} instance to Int32.");


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


		#region IArgumentText members
		public string GetArgumentText() => ToString();
		#endregion
	}
}
