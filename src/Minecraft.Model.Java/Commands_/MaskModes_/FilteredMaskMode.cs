using System;

namespace Minecraft.Model.Java
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


		// TODO: add attributes/tags
		public override string GetArgumentText() => $"filtered {Block.ID}";

		public FilteredMaskMode By(Block block)
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			return new FilteredMaskMode(new PredicateBlock(block));
		}

		public FilteredMaskMode By(PredicateBlock predicateBlock)
		{
			if (predicateBlock is null)
				throw new ArgumentNullException(nameof(predicateBlock));

			return new FilteredMaskMode(predicateBlock);
		}
	}
}
