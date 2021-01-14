using System;
using System.Text;

namespace Minecraft.Model
{
	public class SetBlockCommand : Command
	{
		public SetBlockCommand(Position position, Block block)
			: base("setblock")
		{
			Position = position;
			Block = block;
		}


		public Position Position { get; }

		public Block Block { get; }

		protected override Type EqualityContract => typeof(SetBlockCommand);


		protected override string GetCommandTextImpl(MinecraftEdition edition)
		{
			StringBuilder result = new($"/setblock {Position.GetArgumentText(edition)}");

			if (edition == MinecraftEdition.Java)
			{
				IJavaBlock block = Block;

				result.Append($" {block.ID}");
			}
			else
			{
				IBedrockBlock block = Block;

				result.Append($" {block.ID}");
				if (block.DataValue > 0)
					result.Append($" {block.DataValue}");
			}

			return result.ToString();
		}
	}
}
