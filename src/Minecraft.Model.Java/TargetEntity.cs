using System;
using System.Diagnostics.CodeAnalysis;

namespace Minecraft.Model.Java
{
	public abstract class TargetEntity : Target, IEquatable<TargetEntity>
	{
		private protected TargetEntity()
		{
		}


		public static TargetEntity Parse(string s) => InternalTryParse(s, out TargetEntity? result, out Exception? exception, true) ? result : throw exception!;

		public static bool TryParse(string s, [NotNullWhen(true)] out TargetEntity? result) => InternalTryParse(s, out result, out _, false);

		private static bool InternalTryParse(string s, [NotNullWhen(true)] out TargetEntity? result, out Exception? exception, bool needExcep)
		{
			throw new NotImplementedException();
		}


		public static bool operator ==(TargetEntity? left, TargetEntity? right) => left is null && right is null || left is not null && left.Equals(right);

		public static bool operator !=(TargetEntity? left, TargetEntity? right) => !(left == right);

		[return: NotNullIfNotNull("value")]
		public static implicit operator TargetEntity?(string value)
		{
			if (value is null)
				return null;
			if (value.Length == 0)
				throw new InvalidCastException("Cannot cast empty string to TargetEntity.");

			if (TargetPlayer.TryParse(value, out TargetPlayer? targetPlayer))
				return targetPlayer;

			if (TargetUuid.TryParse(value, out TargetUuid? targetUuid))
				return targetUuid;

			throw new InvalidCastException("Unable to cast string to TargetEntity.");
		}

		[return: NotNullIfNotNull("value")]
		public static explicit operator string?(TargetEntity? entity) => entity?.ToString();


		#region Object members
		public override int GetHashCode() => 0;

		public override bool Equals(object? obj) => false;
		#endregion


		#region IEquatable<TargetEntity> members
		public bool Equals(TargetEntity? other) => other is not null && EqualityContract == other.EqualityContract;
		#endregion
	}
}
