using System;

namespace Minecraft.Model.Bedrock
{
	public sealed class FilteredMaskMode : MaskMode
	{
		internal FilteredMaskMode()
		{
			Block = Block.Unspecified;
		}

		private FilteredMaskMode(Block block)
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			Block = block;
		}


		public Block Block { get; }


		public override string GetArgumentText() => $"filtered";

		public FilteredMaskMode By(Block block)
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			return new FilteredMaskMode(block);
		}
	}
}
