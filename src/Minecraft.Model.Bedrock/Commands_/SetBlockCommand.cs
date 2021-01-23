using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Bedrock
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


		public override string GetCommandText()
		{
			StringBuilder result = new($"/setblock {Position.GetArgumentText()}");

			result.Append($" {Block.ID}");
			if (Block.DataValue != 0)
				result.Append($" {Block.DataValue}");

			return result.ToString();
		}
	}
}
