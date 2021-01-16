using System;

namespace Minecraft.Model
{
	public abstract class Target : IEquatable<Target>, IArgumentText
	{
		private protected Target()
		{
		}


		protected abstract Type EqualityContract { get; }


		public abstract string GetArgumentText(Edition edition);


		#region Object members
		public override int GetHashCode() => 0;

		public override bool Equals(object? obj) => obj is Target other && Equals(other);
		#endregion


		#region IEquatable<Target> members
		public bool Equals(Target? other) => other is not null && EqualityContract == other.EqualityContract;
		#endregion


		#region IArgumentText members
		string IArgumentText.GetArgumentText(Edition edition) => GetArgumentText(edition);
		#endregion
	}
}
