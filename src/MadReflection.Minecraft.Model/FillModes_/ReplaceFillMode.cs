using System;

namespace Minecraft.Model
{
	public class ReplaceFillMode : FillMode
	{
		internal ReplaceFillMode()
		{
			Block = Block.Default;
		}

		internal ReplaceFillMode(Block block)
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			Block = block;
		}


		public override string ArgumentText
		{
			get
			{
				string result = "replace";

				if (!Block.IsUnspecified)
				{
					result += " " + Block.ID;
					if (Block.Data > 0)
						result += $" {Block.Data}";
				}

				return result;
			}
		}

		public Block Block { get; }


		// Suppressing CA1822 because the compiler does not understand the intended usage.
#pragma warning disable CA1822 // Mark members as static
		public ReplaceFillMode With(Block block)
#pragma warning restore CA1822 // Mark members as static
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			return new ReplaceFillMode(block);
		}
	}
}
