using System;
using System.Text;

namespace Minecraft.Model
{
	public class ClearCommand : Command
	{
		public ClearCommand(TargetPlayer targetPlayer, Block? block, int? maxCount)
			: this((TargetEntity)targetPlayer, block, maxCount)
		{
		}

		public ClearCommand(TargetSelector targetSelector, Block? block, int? maxCount)
			: this((TargetEntity)targetSelector, block, maxCount)
		{
		}

		private ClearCommand(TargetEntity target, Block? block, int? maxCount)
			: base("clear")
		{
			if (target is null)
				throw new ArgumentNullException(nameof(target));
			if (maxCount is int && block is not null)
				throw new ArgumentException($"Cannot specify '{nameof(maxCount)}' if '{nameof(block)}' is not null.", nameof(maxCount));

			Target = target;
			Block = block;
			MaxCount = maxCount;
		}


		public TargetEntity Target { get; }

		public Block? Block { get; }

		public int? MaxCount { get; }


		protected override string GetCommandTextImpl(Edition edition)
		{
			StringBuilder result = new StringBuilder();

			result.Append("/clear ");
			result.Append(Target.ToString());

			if (Block != null)
			{
				if (edition == Edition.Java)
				{
					IJavaBlock block = (IJavaBlock)Block;
					result.Append($" {block.ID}");
				}
				else
				{
					IBedrockBlock block = (IBedrockBlock)Block;

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
