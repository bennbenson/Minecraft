using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Minecraft.Model.Java
{
	public sealed class BlockAttributes : IEquatable<BlockAttributes>, IEnumerable<(string, string)>, IArgumentText
	{
		private readonly Dictionary<string, string> _items;


		public BlockAttributes()
		{
			_items = new();
		}


		#region Object members
		public override int GetHashCode() => _items.Count;

		public override bool Equals(object? obj) => obj is BlockAttributes other && Equals(other);

		public override string ToString() => _items.Count == 0 ? "" : $"[{string.Join(",", _items.Select(i => $"{i.Key}={i.Value}"))}]";
		#endregion


		#region IEnumerable<(string, string)> members
		public IEnumerator<(string, string)> GetEnumerator()
		{
			foreach (var item in _items)
				yield return (item.Key, item.Value);
		}
		#endregion


		#region IEnumerable members
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		#endregion


		#region IEquatable<BlockAttributes> members
		public bool Equals(BlockAttributes? other) => other is not null && DictionariesAreEqual(_items, other._items);

		private static bool DictionariesAreEqual(Dictionary<string, string> first, Dictionary<string, string> second)
		{
			if (first.Count != second.Count)
				return false;

			IEnumerable<(string, string)> joined =
				from x in first
				join y in second on x.Key equals y.Key
				select (x.Value, y.Value);

			int count = 0;
			using (var enumerator = joined.GetEnumerator())
			{
				for (; enumerator.MoveNext(); ++count)
				{
					var (x, y) = enumerator.Current;
					if (x != y)
						return false;
				}
			}

			return count == first.Count;
		}
		#endregion


		#region IArgumentText members
		public string GetArgumentText() => _items.Count == 0 ? "" : $"[{string.Join(",", _items.Select(i => $"{i.Key}={i.Value}"))}]";
		#endregion
	}
}
