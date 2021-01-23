using System;

namespace Minecraft.Model.Java
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


		public override string GetArgumentText()
		{
			if (Block.IsUnspecified)
				return "";

			string result = "replace";

			if (!Block.IsUnspecified)
			{
				result += " " + Block.ID;
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
