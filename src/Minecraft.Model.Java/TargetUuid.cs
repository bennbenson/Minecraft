using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TargetUuid : TargetEntity
	{
		public TargetUuid(Guid id)
		{
			ID = id;
		}


		public Guid ID { get; }

		protected override Type EqualityContract => typeof(TargetUuid);

		public string DebuggerDisplay => ToString();


		public static new TargetUuid Parse(string s) => InternalTryParse(s, out TargetUuid? result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, [NotNullWhen(true)] out TargetUuid? result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, [NotNullWhen(true)] out TargetUuid? result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = default;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			if (Guid.TryParseExact(s, "D", out Guid id) || Guid.TryParseExact(s, "N", out id))
			{
				result = new TargetUuid(id);
				exception = null;
				return true;
			}

			Match match = Regex.Match(s, @"^(?<a>[0-9a-fA-F]{1,8})-(?<b>[0-9a-fA-F]{1,4})-(?<c>[0-9a-fA-F]{1,4})-(?<d>[0-9a-fA-F]{1,4})-(?<e>[0-9a-fA-F]{1,12})$");
			if (match.Success)
			{
				string a = match.Groups["a"].Value.PadLeft(8, '0');
				string b = match.Groups["b"].Value.PadLeft(4, '0');
				string c = match.Groups["c"].Value.PadLeft(4, '0');
				string d = match.Groups["d"].Value.PadLeft(4, '0');
				string e = match.Groups["e"].Value.PadLeft(12, '0');

				if (Guid.TryParseExact($"{a}{b}{c}{d}{e}", "N", out id))  // This shouldn't possibly fail.
				{
					result = new TargetUuid(id);
					exception = null;
					return true;
				}
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public override string GetArgumentText() => ID.ToString("D");


		public static bool operator ==(TargetUuid? left, TargetUuid? right) => left is null && right is null || left is not null && left.Equals(right);

		public static bool operator !=(TargetUuid? left, TargetUuid? right) => !(left == right);

		public static implicit operator TargetUuid(Guid value) => new TargetUuid(value);

		public static explicit operator Guid(TargetUuid? value) => value?.ID ?? Guid.Empty;


		#region Object members
		public override int GetHashCode() => ID.GetHashCode();

		public override bool Equals(object? obj) => obj is TargetUuid other && Equals(other);

		public override string ToString() => ID.ToString("D");
		#endregion


		#region IEquatable<TargetUuid> members
		public bool Equals(TargetUuid? other) => other is not null && ID == other.ID;
		#endregion
	}
}
