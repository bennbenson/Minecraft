using System;

namespace Minecraft.Model.Java
{
	public class PredicateBlock : Block
	{
		public PredicateBlock(Block block)
			: base("_")
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			while (block is PredicateBlock predicateBlock)
				block = predicateBlock;

			Block = block;
		}


		public Block Block { get; }

		public override string ID => Block.ID;

		protected override Type EqualityContract => typeof(PredicateBlock);

		public string DebuggerDisplay => ToString();


		#region Object members
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		public override string ToString()
		{
			return base.ToString();
		}
		#endregion


		#region IEquatable<PredicateBlock> members
		public bool Equals(PredicateBlock? other)
		{
			return other is not null && EqualityContract == other.EqualityContract;
		}
		#endregion
	}
}




//protected internal static bool DictionariesAreEqual(Lazy<Dictionary<string, string>> first, Lazy<Dictionary<string, string>> second)
//{
//	if (!first.IsValueCreated)
//		return !second.IsValueCreated || second.Value.Count == 0;

//	return DictionariesAreEqual(first.Value, second.Value);
//}

//protected internal static bool DictionariesAreEqual(Dictionary<string, string> first, Dictionary<string, string> second)
//{
//	if (first.Count != second.Count)
//		return false;

//	IEnumerable<(string, string)> joined =
//		from x in first
//		join y in second on x.Key equals y.Key
//		select (x.Value, y.Value);

//	int count = 0;
//	using (var enumerator = joined.GetEnumerator())
//	{
//		for (; enumerator.MoveNext(); ++count)
//		{
//			var (x, y) = enumerator.Current;
//			if (x != y)
//				return false;
//		}
//	}

//	return count == first.Count;
//}
