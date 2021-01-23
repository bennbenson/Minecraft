using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Minecraft.Model.Java.Data;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class Block : IEquatable<Block>, IArgumentText
	{
		public static readonly Block Unspecified = new Block("", null);
		public static readonly Block Default = new Block("air", null);
		public static readonly Block Air = Default;
		private static readonly IReadOnlyDictionary<string, string> EmptyDataValues = new Dictionary<string, string>();

		private readonly string _id;
		private readonly Dictionary<string, string>? _dataValues;


		private protected Block(string id)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (id is "")
				throw new ArgumentException("ID cannot be blank.", nameof(id));

			_id = id;
			_dataValues = null;
		}

		private protected Block(string id, Dictionary<string, string>? dataValues)
		{
			_id = id;
			_dataValues = dataValues;
		}


		public virtual string ID => _id ?? "air";

		public IReadOnlyDictionary<string, string>? DataValues => _dataValues ?? EmptyDataValues;

		public bool IsUnspecified => _id == "";

		protected virtual Type EqualityContract => typeof(Block);

		private string DebuggerDisplay => ToString();


		public Block With(string key, string value) => With(new Dictionary<string, string>() { [key] = value });

		public Block With((string Key, string Value) dataValue) => With(new Dictionary<string, string>() { [dataValue.Key] = dataValue.Value });

		public Block With((string Key, string Value) dataValue1, (string Key, string Value) dataValue2) => With(new Dictionary<string, string>() { [dataValue1.Key] = dataValue1.Value, [dataValue2.Key] = dataValue2.Value });

		public Block With((string Key, string Value) dataValue1, (string Key, string Value) dataValue2, (string Key, string Value) dataValue3) => With(new Dictionary<string, string>() { [dataValue1.Key] = dataValue1.Value, [dataValue2.Key] = dataValue2.Value, [dataValue3.Key] = dataValue3.Value });

		public virtual Block With(IDictionary<string, string> dataValues)
		{
			Dictionary<string, string> existing;
			if (_dataValues is not null)
				existing = new(_dataValues);
			else
				existing = new();

			foreach (var kvp in dataValues)
				existing[kvp.Key] = kvp.Value;

			return new Block(ID, existing);
		}

		public static Block Get(string id)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentException("Block ID cannot be empty.", nameof(id));

			if (id.EndsWith("command_block"))
				return GetCommandBlock(id);

			BlockData? blockData = BlockData.Find(id);
			if (blockData is not null)
				return new Block(blockData.ID);

			throw new ArgumentException("Unknown block ID.", nameof(id));
		}

		private static CommandBlock GetCommandBlock(string id)
		{
			CommandBlockType type = id switch
			{
				"command_block" => CommandBlockType.Impulse,
				"chain_command_block" => CommandBlockType.Chain,
				"repeating_command_block" => CommandBlockType.Repeating,
				_ => throw new ArgumentException("Unknown block ID.", nameof(id))
			};

			return new CommandBlock(Command.Empty, type);
		}

		protected internal static bool DictionariesAreEqual(Lazy<Dictionary<string, string>> first, Lazy<Dictionary<string, string>> second)
		{
			if (!first.IsValueCreated)
				return !second.IsValueCreated || second.Value.Count == 0;

			return DictionariesAreEqual(first.Value, second.Value);
		}

		protected internal static bool DictionariesAreEqual(Dictionary<string, string> first, Dictionary<string, string> second)
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


		#region Object members
		public override int GetHashCode() => (_id ?? "").GetHashCode();

		public override bool Equals(object? obj) => obj is Block other && Equals(other);

		public override string ToString() => _id;
		#endregion


		#region IEquatable<Block> members
		public bool Equals(Block? other) => other is not null && EqualityContract == other.EqualityContract && _id == other._id;
		#endregion


		#region IArgumentText members
		public virtual string GetArgumentText() => _dataValues is null ? _id : $"{_id}[{string.Join(",", _dataValues.Select(kvp => $"{kvp.Key}={kvp.Value}"))}]";
		#endregion
	}
}
