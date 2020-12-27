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
				IJEBlock block = Block;

				string result = $"/setblock ";
				return result;
			}
			else
			{
				IBEBlock block = Block;

				string result = $"/setblock {Point.ArgumentText} {block.ID}";
				if (block.DV > 0)
					result += $" {block.DV}";
				return result;
			}
		}
	}
}
