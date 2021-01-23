using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Java
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


		public override string GetCommandText() => $"/setblock {Position.GetArgumentText()} {Block.GetArgumentText()}";
	}
}
