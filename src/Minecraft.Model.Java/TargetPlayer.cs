using System;
using System.Diagnostics.CodeAnalysis;

namespace Minecraft.Model.Java
{
	public abstract class TargetPlayer : TargetEntity, IEquatable<TargetPlayer>
	{
		private protected TargetPlayer()
		{
		}


		public static new TargetPlayer Parse(string s) => InternalTryParse(s, out TargetPlayer? result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, [NotNullWhen(true)] out TargetPlayer? result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, [NotNullWhen(true)] out TargetPlayer? result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = null;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			if (s.Length > 0)
			{
				if (s[0] == '@')
				{
					if (TargetPlayerSelector.TryParse(s, out TargetPlayerSelector? tps))
					{
						result = tps;
						exception = null;
						return true;
					}
				}
				else
				{
					if (TargetPlayerName.TryParse(s, out TargetPlayerName? tpn))
					{
						result = tpn;
						exception = null;
						return true;
					}
				}
			}

			result = null;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public static bool operator ==(TargetPlayer? left, TargetPlayer? right) => left is null && right is null || left is not null && left.Equals(right);

		public static bool operator !=(TargetPlayer? left, TargetPlayer? right) => !(left == right);

		[return: NotNullIfNotNull("value")]
		public static implicit operator TargetPlayer?(string value)
		{
			if (value is null)
				return null;
			if (value.Length == 0)
				throw new InvalidCastException("Cannot cast empty string to TargetPlayer.");

			if (!InternalTryParse(value, out TargetPlayer? result, out _, false))
				throw new InvalidCastException("Unable to cast string to TargetPlayer. " + (value[0] == '@' ? "Invalid target selector." : "Invalid player name."));

			return result;
		}

		[return: NotNullIfNotNull("value")]
		public static explicit operator string?(TargetPlayer? player) => player?.ToString();


		#region Object members
		public override int GetHashCode() => 0;

		public override bool Equals(object? obj) => obj is Target other && Equals(other);
		#endregion


		#region IEquatable<TargetPlayer> members
		public bool Equals(TargetPlayer? other) => other is not null && EqualityContract == other.EqualityContract;
		#endregion
	}
}
