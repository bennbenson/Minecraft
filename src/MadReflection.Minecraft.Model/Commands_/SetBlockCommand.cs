using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
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

		private string DebuggerDisplay => ToString();


		protected override string GetCommandTextImpl(Edition edition)
		{
			StringBuilder result = new($"/setblock {Position.GetArgumentText(edition)}");

			if (edition == Edition.Java)
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
