using System;

namespace Minecraft.Model
{
	public class ReplaceFillMode : FillMode
	{
		internal ReplaceFillMode()
		{
			Block = Block.Unspecified;
		}

		internal ReplaceFillMode(Block block)
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			Block = block;
		}


		public Block Block { get; }


		protected override string GetArgumentTextImpl(MinecraftEdition edition)
		{
			if (Block.IsUnspecified)
				return "";

			string result = "replace";

			if (edition == MinecraftEdition.Java)
			{
				IJEBlock block = Block;

				if (!Block.IsUnspecified)
				{
					result += " " + block.ID;
				}
			}
			else
			{
				IBEBlock block = Block;

				if (!Block.IsUnspecified)
				{
					result += " " + block.ID;
					if (block.DV > 0)
						result += $" {block.DV}";
				}
			}

			return result;
		}

		public ReplaceFillMode With(Block block)
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			return new ReplaceFillMode(block);
		}
	}
}
