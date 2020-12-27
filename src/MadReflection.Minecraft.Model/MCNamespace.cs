using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Threading;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	internal sealed class MCNamespace : IEquatable<MCNamespace>
	{
		#region Lock-related classes
		private static class NamespaceLockType { }
		private static class NameLockType { }
		#endregion

		private static readonly Dictionary<string, MCNamespace> _namespaces = new();
		private static readonly Dictionary<string, MCName> _names = new();


		internal MCNamespace(string namespaceName)
		{
			NamespaceName = namespaceName;
		}


		public string NamespaceName { get; }

		private string DebuggerDisplay => NamespaceName;

		public static MCNamespace Minecraft => Get("minecraft");


		public static MCNamespace Get(string namespaceName)
		{
			if (namespaceName is null)
				throw new ArgumentNullException(nameof(namespaceName));

			if (!_namespaces.TryGetValue(namespaceName, out MCNamespace? value))
			{
				//if (namespaceName.Length == 0)
				//	throw new ArgumentException("Namespace name cannot be empty.", nameof(namespaceName));
				if (IndexOfNamespaceNameBadChar(namespaceName) is int firstBadChar)
					throw new ArgumentException($"Namespace name contains invalid character '{namespaceName[firstBadChar]}'.", nameof(namespaceName));

				lock (typeof(NamespaceLockType))
				{
					if (!_namespaces.TryGetValue(namespaceName, out value))
					{
						value = new MCNamespace(namespaceName);
						_namespaces.Add(namespaceName, value);
					}
				}
			}

			return value;
		}

		public MCName GetName(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));

			string key = $"{NamespaceName}:{name}";

			if (!_names.TryGetValue(key, out MCName? value))
			{
				if (name.Length == 0)
					throw new ArgumentException("Name cannot be empty.", nameof(name));
				if (IndexOfNameBadChar(name) is int firstBadChar)
					throw new ArgumentException($"Name contains invalid character '{name[firstBadChar]}'.", nameof(name));

				lock (typeof(NameLockType))
				{
					if (!_names.TryGetValue(key, out value))
					{
						value = new MCName(this, name);
						_names.Add(key, value);
					}
				}
			}

			return value;
		}

		#region Validation methods
		// Identifiers are validated according to the more permissive Bedrock restrictions.
		// If you need JE validation, do it "yerself".
		private static int? IndexOfNamespaceNameBadChar(string namespaceName)
		{
			for (int index = 0; index < namespaceName.Length; ++index)
			{
				char ch = namespaceName[index];
				if (ch is ':' or '/' or '\\')
					return index;
			}

			return null;
		}

		private static int? IndexOfNameBadChar(string name)
		{
			for (int index = 0; index < name.Length; ++index)
			{
				char ch = name[index];
				if (ch is ':' or '/' or '\\')
					return index;
			}

			return null;
		}
		#endregion


		public static bool operator ==(MCNamespace? left, MCNamespace? right) => (object?)left == right;

		public static bool operator !=(MCNamespace? left, MCNamespace? right) => (object?)left != right;

		[return: NotNullIfNotNull("namespaceName")]
		public static implicit operator MCNamespace?(string? namespaceName) => namespaceName is null ? null : Get(namespaceName);

		public static MCName operator +(MCNamespace ns, string name)
		{
			if (ns is null)
				throw new ArgumentNullException(nameof(ns));

			return ns.GetName(name);
		}


		#region Object members
		public override int GetHashCode() => NamespaceName.GetHashCode();

		public override bool Equals(object? obj) => obj is MCNamespace other && Equals(other);

		public override string ToString() => NamespaceName + ":";
		#endregion


		#region IEquatable<MCNamespace> members
		public bool Equals(MCNamespace? other) => other is not null && NamespaceName == other.NamespaceName;
		#endregion
	}
}
