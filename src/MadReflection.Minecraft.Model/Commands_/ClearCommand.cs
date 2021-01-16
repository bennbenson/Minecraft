using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model
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


		protected override string GetCommandTextImpl(Edition edition)
		{
			StringBuilder result = new StringBuilder();

			result.Append("/clear ");
			result.Append(Target.GetArgumentText(edition));

			if (Block is not null)
			{
				if (edition == Edition.Java)
				{
					IJavaBlock block = Block;
					result.Append($" {block.ID}");
				}
				else
				{
					IBedrockBlock block = Block;

					result.Append($" {block.ID}");
					if (block.DataValue != 0 || MaxCount is not null)
						result.Append($" {block.DataValue}");
				}

				if (MaxCount is int maxCount)
					result.Append($" {maxCount}");
			}

			return result.ToString();
		}
	}
}
