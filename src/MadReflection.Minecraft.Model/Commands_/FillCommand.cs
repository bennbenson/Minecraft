using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class FillCommand : Command, IEquatable<FillCommand>
	{
		public FillCommand(Position from, Position to, Block block)
			: base("fill")
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			From = from;
			To = to;
			Block = block;
			FillMode = FillMode.Replace;
		}

		public FillCommand(Position from, Position to, Block block, FillMode fillMode)
			: base("fill")
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));
			if (fillMode is null)
				throw new ArgumentNullException(nameof(fillMode));

			From = from;
			To = to;
			Block = block;
			FillMode = fillMode;
		}


		public Position From { get; }

		public Position To { get; }

		public Block Block { get; }

		public FillMode FillMode { get; init; }

		protected override Type EqualityContract => typeof(FillCommand);

		private string DebuggerDisplay => ToString();


		protected override string GetCommandTextImpl(MinecraftEdition edition)
		{
			StringBuilder result = new StringBuilder();
			result.Insert(0, FillMode.GetArgumentText(edition));

			bool hasFillMode = result.Length > 0;

			if (hasFillMode)
				result.Insert(0, " ");

			if (edition == MinecraftEdition.Java)
			{
				IJavaBlock fillBlock = Block;
				result.Insert(0, fillBlock.ID);
			}
			else
			{
				IBedrockBlock fillBlock = Block;

				if (hasFillMode || fillBlock.DataValue > 0)
					result.Insert(0, $" {fillBlock.DataValue}");
				result.Insert(0, fillBlock.ID);
			}

			result.Insert(0, $"/fill {From.GetArgumentText(edition)} {To.GetArgumentText(edition)} ");
			return result.ToString();
		}


		#region Object members
		public override int GetHashCode() => HashCode.Combine(From, To, Block, FillMode);

		public override bool Equals(object? obj) => obj is FillCommand other && Equals(other);

		public override string ToString() => GetCommandTextImpl(MinecraftEdition.Java);
		#endregion


		#region IEquatable<FillCommand> members
		public bool Equals(FillCommand? other) => other is not null && EqualityContract == other.EqualityContract && From == other.From && To == other.To && Block == other.Block && FillMode == other.FillMode;
		#endregion
	}
}
