using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	internal sealed class MCName : IEquatable<MCName>
	{
		internal MCName(MCNamespace ns, string name)
		{
			Namespace = ns;
			Name = name;
		}


		public string Name { get; }

		public MCNamespace Namespace { get; }

		public string NamespaceName => Namespace.NamespaceName;

		private string DebuggerDisplay => ToString();


		public static MCName Get(string fullName)
		{
			if (fullName is null)
				throw new ArgumentNullException(nameof(fullName));

			int first = fullName.IndexOf(':');
			int last = fullName.LastIndexOf(':');

			if (first >= 0 && first != last)
				throw new ArgumentException("", nameof(fullName));

			return MCNamespace.Get(fullName.Substring(0, first)).GetName(fullName[(first + 1)..]);
		}

		public static MCName Get(string name, string namespaceName) => MCNamespace.Get(namespaceName).GetName(name);


		public static bool operator ==(MCName? left, MCName? right) => (object?)left == right;

		public static bool operator !=(MCName? left, MCName? right) => (object?)left != right;

		[return: NotNullIfNotNull("fullName")]
		public static implicit operator MCName?(string? fullName) => fullName is null ? null : Get(fullName, "minecraft");


		#region Object members
		public override int GetHashCode() => Namespace.GetHashCode() ^ Name.GetHashCode();

		public override bool Equals(object? obj) => obj is MCName other && Equals(other);

		public override string ToString() => NamespaceName.Length == 0 ? Name : $"{Namespace.NamespaceName}:{Name}";
		#endregion


		#region IEquatable<MCName> members
		public bool Equals(MCName? other) => other is not null && Namespace == other.Namespace && Name == other.Name;
		#endregion
	}
}
