using System;

namespace Minecraft.Model
{
	public class SetBlockCommand : Command
	{
		public SetBlockCommand(Coord3 point, Block block)
			: base("setblock")
		{
			Point = point;
			Block = block;
		}


		public Coord3 Point { get; }

		public Block Block { get; }

		protected override Type EqualityContract => typeof(SetBlockCommand);


		protected override string GetCommandTextImpl(MinecraftEdition edition)
		{
			if (edition == MinecraftEdition.Java)
			{
				IJavaBlock block = Block;

				string result = $"/setblock ";
				return result;
			}
			else
			{
				IBedrockBlock block = Block;

				string result = $"/setblock {Point.GetArgumentText(edition)} {block.ID}";
				if (block.DataValue > 0)
					result += $" {block.DataValue}";
				return result;
			}
		}
	}
}
