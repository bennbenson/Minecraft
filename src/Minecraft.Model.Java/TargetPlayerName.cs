using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TargetPlayerName : TargetPlayer, IEquatable<TargetPlayerName>
	{
		public TargetPlayerName(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (name is "")
				throw new ArgumentException("Name cannot be empty", nameof(name));

			Name = name;
		}


		public string Name { get; }

		protected override Type EqualityContract => typeof(TargetPlayerName);

		private string DebuggerDisplay => ToString();


		public static new TargetPlayerName Parse(string s) => InternalTryParse(s, out TargetPlayerName? result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, [NotNullWhen(true)] out TargetPlayerName? result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, [NotNullWhen(true)] out TargetPlayerName? result, out Exception? exception, bool needException)
		{
			if (s is null)
			{
				result = null;
				exception = needException ? new ArgumentNullException(nameof(s)) : null;
				return false;
			}

			Match match = Regex.Match(s, @"^\w+$");
			if (match.Success)
			{
				result = new TargetPlayerName(s);
				exception = null;
				return true;
			}

			result = null;
			exception = needException ? new FormatException() : null;
			return false;
		}

		public override string GetArgumentText() => Name;


		#region Object members
		public override int GetHashCode() => Name.GetHashCode();

		public override bool Equals(object? obj) => obj is TargetPlayerName other && Equals(other);

		public override string ToString() => Name;
		#endregion


		#region IEquatable<Player> members
		public bool Equals(TargetPlayerName? other) => other is not null && EqualityContract == other.EqualityContract && Name == other.Name;
		#endregion
	}
}
