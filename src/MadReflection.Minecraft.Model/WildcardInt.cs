using System;

namespace Minecraft.Model
{
	internal readonly struct WildcardInt : IEquatable<WildcardInt>, IArgumentText
	{
		public static readonly WildcardInt Wildcard = new WildcardInt(false);

		private readonly bool _isWildcard;
		private readonly int _value;


		public WildcardInt(int value)
		{
			_value = value;
			_isWildcard = false;
		}

		private WildcardInt(bool _)
		{
			_value = 0;
			_isWildcard = true;
		}


		public int Value => !_isWildcard ? _value : throw new InvalidOperationException("Instance is a wildcard.");

		public bool IsWildcard => _isWildcard;


		public static implicit operator WildcardInt(int value) => new WildcardInt(value);

		public static explicit operator int(WildcardInt value) => !value._isWildcard ? value.Value : throw new InvalidCastException();


		#region Object members
		public override int GetHashCode() => _isWildcard ? -1 : _value.GetHashCode();

		public override bool Equals(object? obj) => obj is WildcardInt other && Equals(other);

		public override string ToString() => _isWildcard ? "*" : _value.ToString();
		#endregion


		#region IEquatable<WildcardInt> members
		public bool Equals(WildcardInt other) => _isWildcard == other._isWildcard && _value == other._value;
		#endregion


		#region IArgumentText members
		public string GetArgumentText(Edition edition) => _isWildcard ? "*" : _value.ToString();
		#endregion
	}
}
