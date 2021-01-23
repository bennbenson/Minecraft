using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class ClearCommand : Command
	{
		public ClearCommand(TargetPlayer targetPlayer, Block? block = null, int? maxCount = null)
			: base("clear")
		{
			if (targetPlayer is null)
				throw new ArgumentNullException(nameof(targetPlayer));
			if (maxCount is int maxCountValue)
			{
				if (block is not null)
					throw new ArgumentException($"Cannot specify '{nameof(maxCount)}' if '{nameof(block)}' is not null.", nameof(maxCount));
				if (maxCountValue < -1)
					throw new ArgumentOutOfRangeException(nameof(maxCount));
			}

			Target = targetPlayer;
			Block = block;
			MaxCount = maxCount;
		}


		public Target Target { get; }

		public Block? Block { get; }

		public int? MaxCount { get; }

		protected override Type EqualityContract => typeof(ClearCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			StringBuilder result = new StringBuilder();

			result.Append($"/clear {Target.GetArgumentText()}");

			if (Block is not null)
			{
				result.Append($" {Block.ID}");
				if (Block.DataValue != 0 || MaxCount is not null)
					result.Append($" {Block.DataValue}");

				if (MaxCount is int maxCount)
					result.Append($" {maxCount}");
			}

			return result.ToString();
		}
	}
}
