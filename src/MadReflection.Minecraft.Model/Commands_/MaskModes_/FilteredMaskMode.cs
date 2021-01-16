using System;
using System.Collections.Generic;
using System.Linq;

namespace Minecraft.Model
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


		protected override string GetArgumentTextImpl(Edition edition) => $"filtered";

		//[Obsolete("This may need to take a Block-derived type that adds the filter/tag/whatever in [] and {}.")]
		public FilteredMaskMode By(Block block)
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			return new FilteredMaskMode(block);
		}
	}
}
