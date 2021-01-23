using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Minecraft.Model.Bedrock.Data;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class Block : IEquatable<Block>, IArgumentText
	{
		public static readonly Block Unspecified = new Block(false, "", 0);
		public static readonly Block Default = new Block(false, "air", 0);
		public static readonly Block Air = Default;

		private readonly string _id;
		private readonly int _dataValue;


		private protected Block(string id, int dataValue)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (id is "")
				throw new ArgumentException("ID cannot be blank.", nameof(id));
			if (dataValue < 0)
				throw new ArgumentOutOfRangeException(nameof(dataValue), "Data Value cannot be negative.");

			_id = id;
			_dataValue = dataValue;
		}

		private Block(bool _, string id, int dataValue)
		{
			_id = id;
			_dataValue = dataValue;
		}


		public virtual string ID => _id ?? "air";

		public virtual int DataValue => _dataValue;

		public bool IsUnspecified => _id == "";

		protected virtual Type EqualityContract => typeof(Block);

		private string DebuggerDisplay => ToString();


		public Block WithDataValue(int dataValue) => new Block(ID, dataValue);

		public static Block Get(string id, int dataValue = 0)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentException("Block ID cannot be empty.", nameof(id));
			if (dataValue < 0)
				throw new ArgumentOutOfRangeException(nameof(dataValue), "Data value cannot be negative.");

			if (id.EndsWith("command_block"))
				return GetCommandBlock(id, dataValue);

			BlockData? blockData = BlockData.Find(id, dataValue);
			if (blockData is not null)
				return new Block(blockData.ID, blockData.DataValue);

			throw new ArgumentException("Unknown block ID.", nameof(id));
		}

		public static Block Create(string id, int dataValue)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentException("Block ID cannot be empty.", nameof(id));
			if (dataValue < 0)
				throw new ArgumentOutOfRangeException(nameof(dataValue), "Data value cannot be negative.");

			if (id.EndsWith("command_block"))
				return GetCommandBlock(id, dataValue);

			BlockData? blockData = BlockData.Find(id, 0);
			if (blockData is not null)
				return new Block(blockData.ID, dataValue);

			throw new ArgumentException("Unknown block ID.", nameof(id));
		}

		private static CommandBlock GetCommandBlock(string id, int dataValue)
		{
			CommandBlockType type = id switch
			{
				"command_block" => CommandBlockType.Impulse,
				"chain_command_block" => CommandBlockType.Chain,
				"repeating_command_block" => CommandBlockType.Repeating,
				_ => throw new ArgumentException("Unknown block ID.", nameof(id))
			};

			return new CommandBlock(dataValue, Command.Empty, type);
		}


		#region Object members
		public override int GetHashCode() => (_id ?? "").GetHashCode();

		public override bool Equals(object? obj) => obj is Block other && Equals(other);

		public override string ToString() => $"{_id}:{_dataValue}";
		#endregion


		#region IEquatable<Block> members
		public bool Equals(Block? other) => other is not null && _id == other.ID && _dataValue == other.DataValue;
		#endregion


		#region IArgumentText members
		public virtual string GetArgumentText() => $"{ID} {DataValue}";
		#endregion
	}
}
