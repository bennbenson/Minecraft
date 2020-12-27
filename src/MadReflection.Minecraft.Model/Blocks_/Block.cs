using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Minecraft.Model.Data;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class Block : IEquatable<Block>, IJEBlock, IBEBlock
	{
		public static readonly Block Unspecified = new Block("", "", 0, false);
		public static readonly Block Default = new Block("air", "air", 0);
		public static readonly Block Air = Default;

		private readonly string? _jeID;
		private readonly Lazy<Dictionary<string, string>> _states = new Lazy<Dictionary<string, string>>(() => new Dictionary<string, string>());
		private readonly Lazy<Dictionary<string, string>> _tags = new Lazy<Dictionary<string, string>>(() => new Dictionary<string, string>());

		private readonly string? _beID;
		private readonly int _beDV;


		private protected Block(string jeID, string beID, int beDataValue)
		{
			if (jeID is null)
				throw new ArgumentNullException(nameof(jeID));
			if (jeID is "")
				throw new ArgumentException("Java Edition ID cannot be blank.", nameof(jeID));
			if (beID is null)
				throw new ArgumentNullException(nameof(beID));
			if (beID is "")
				throw new ArgumentException("Bedrock Edition ID cannot be blank.", nameof(beID));
			if (beDataValue < 0)
				throw new ArgumentOutOfRangeException(nameof(beDataValue), "Bedrock Edition Data Value cannot be negative.");

			_jeID = jeID;
			_beID = beID;
			_beDV = beDataValue;
		}

		private Block(string jeID, string beID, int beDataValue, bool _)
		{
			_jeID = jeID;
			_beID = beID;
			_beDV = beDataValue;
		}


		public bool IsUnspecified => _jeID == "";

		protected virtual Type EqualityContract => typeof(Block);

		private string DebuggerDisplay => $"{_jeID} | {_beID}+{_beDV}";


		public static Block GetByJavaID(string id)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (id is "")
				throw new ArgumentException("Block ID cannot be empty.", nameof(id));

			if (id.EndsWith("command_block"))
				return GetCommandBlock(id);

			BlockData? blockData = BlockData.Find(id, null);
			if (blockData is not null)
				return new Block(blockData.JE_ID, blockData.BE_ID, blockData.BE_DV);


			throw new ArgumentException("Unknown block ID.", nameof(id));
		}

		public static Block GetByBedrockID(string id, int dataValue = 0)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (id is "")
				throw new ArgumentException("Block ID cannot be empty.", nameof(id));
			if (dataValue < 0)
				throw new ArgumentOutOfRangeException(nameof(dataValue), "Data value cannot be negative.");

			if (id.EndsWith("command_block"))
				return GetCommandBlock(id);

			BlockData? blockData = BlockData.Find(null, (id, dataValue));
			if (blockData is not null)
				return new Block(blockData.JE_ID, blockData.BE_ID, blockData.BE_DV);

			throw new ArgumentException("Unknown block ID.", nameof(id));
		}

		private static CommandBlock GetCommandBlock(string id)
		{
			return id switch
			{
				"command_block" => new CommandBlock(Command.Empty, CommandBlockType.Impulse, false, false),
				"chain_command_block" => new CommandBlock(Command.Empty, CommandBlockType.Chain, false, false),
				"repeating_command_block" => new CommandBlock(Command.Empty, CommandBlockType.Repeating, false, false),
				_ => throw new ArgumentException("Unknown block ID.", nameof(id))
			};
		}

		private protected static bool DictionariesAreEqual(Lazy<Dictionary<string, string>> first, Lazy<Dictionary<string, string>> second)
		{
			if (!first.IsValueCreated)
				return !second.IsValueCreated;

			Dictionary<string, string> left = first.Value;
			Dictionary<string, string> right = second.Value;

			if (left.Count != right.Count)
				return false;

			(string x, string y)[] joined = (
				from x in left
				join y in right on x.Key equals y.Key
				select (x.Value, y.Value)
				).ToArray();

			if (joined.Length != left.Count)
				return false;

			if (joined.Any(p => p.x != p.y))
				return false;

			return true;
		}


		#region IJEBlock members
		string IJEBlock.ID => _jeID ?? "air";
		#endregion


		#region IBEBlock members
		string IBEBlock.ID => _beID ?? "air";

		int IBEBlock.DV => _beDV;
		#endregion


		#region Object members
		public override int GetHashCode() => (_jeID ?? "").GetHashCode();

		public override bool Equals(object? obj) => obj is Block other && Equals(other);

		public override string ToString() => $"{_jeID} | {_beID} {_beDV}";
		#endregion


		#region IEquatable<Block> members
		public bool Equals(Block? other)
		{
			if (other is null)
				return false;

			return _jeID == other._jeID && DictionariesAreEqual(_states, other._states) && DictionariesAreEqual(_tags, other._tags);
		}
		#endregion
	}
}
