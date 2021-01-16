using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TargetPlayerSelector : TargetPlayer, IEquatable<TargetPlayerSelector>
	{
		public static readonly TargetPlayerSelector All = new TargetPlayerSelector('a');
		public static readonly TargetPlayerSelector Entity = new TargetPlayerSelector('e');
		public static readonly TargetPlayerSelector Proximate = new TargetPlayerSelector('p');
		public static readonly TargetPlayerSelector Random = new TargetPlayerSelector('r');
		public static readonly TargetPlayerSelector Self = new TargetPlayerSelector('s');

		private readonly Dictionary<string, string>? _conditions = new();


		public TargetPlayerSelector(char t)
		{
			if (t is not 'a' and not 'e' and not 'p' and not 'r' and not 's')
				throw new ArgumentOutOfRangeException(nameof(t), "Invalid selector character.");

			T = t;
			_conditions = null;
		}

		private TargetPlayerSelector(char t, Dictionary<string, string>? conditions)
			: this(t)
		{
			_conditions = conditions;
		}


		public char T { get; }

		protected override Type EqualityContract => typeof(TargetPlayerSelector);

		private string DebuggerDisplay => $"@{T}";


		public TargetPlayerSelector WithRadius(float radius) => WithKvp("r", radius.ToString("0.##"));

		public TargetPlayerSelector WithX(int x) => WithKvp("x", x.ToString());

		public TargetPlayerSelector WithY(int y)
		{
			if (y < 0 || y > 255)
				throw new ArgumentOutOfRangeException(nameof(y), "Must be between 0 and 255, inclusive.");

			return WithKvp("y", y.ToString());
		}

		public TargetPlayerSelector WithZ(int z) => WithKvp("z", z.ToString());

		public TargetPlayerSelector WithKvp(string key, string value)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));
			if (key.Length == 0)
				throw new ArgumentException("Key cannot be empty.", nameof(key));
			if (value is null)
				throw new ArgumentNullException(nameof(value));
			if (value.Length == 0)
				throw new ArgumentException("Value cannot be empty.", nameof(key));

			Dictionary<string, string> newConditions = _conditions is not null ? new(_conditions) : new();
			newConditions[key] = value;

			return new TargetPlayerSelector(T, newConditions);
		}


		public static new TargetPlayerSelector Parse(string s) => InternalTryParse(s, out TargetPlayerSelector? result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, [NotNullWhen(true)] out TargetPlayerSelector? result) => InternalTryParse(s, out result, out _, false);

		public static bool InternalTryParse(string s, [NotNullWhen(true)] out TargetPlayerSelector? result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = null;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			if (s.Length > 1 && s[0] == '@')
			{
				char t = s[1];
				if (t is not 'a' and not 'e' and not 'p' and not 'r' and not 's' and not 'c' and not 'v')
				{
					result = null;
					exception = needException ? new FormatException() : null;
					return false;
				}

				string extra = "";
				if (s.Length > 2)
				{
					// TODO: Parse the attributes.

					extra = s[2..];
				}

				result = new TargetPlayerSelector(t);
				exception = null;
				return true;
			}

			result = null;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public override string GetArgumentText(Edition edition)
		{
			if (_conditions is not null)
				return $"@{T}[{string.Join(",", _conditions.Select(kvp => $"{kvp.Key}={kvp.Value}"))}]";

			return $"@{T}";
		}


		public static bool operator ==(TargetPlayerSelector? left, TargetPlayerSelector? right) => left is null && right is null || left is not null && left.Equals(right);

		public static bool operator !=(TargetPlayerSelector? left, TargetPlayerSelector? right) => !(left == right);


		#region Object members
		public override int GetHashCode() => T.GetHashCode();

		public override bool Equals(object? obj) => obj is TargetPlayerSelector other && Equals(other);

		public override string ToString()
		{
			//if (conditions.Any())
			//    return $"@{T}[{string.Join(",", conditions.Select(x=>$"{x.Name}={x.Value}"))}]"

			return $"@{T}";
		}
		#endregion


		#region IEquatable<TargetSelector> members
		public bool Equals(TargetPlayerSelector? other) => other is not null && EqualityContract == other.EqualityContract && T == other.T;
		#endregion
	}
}
