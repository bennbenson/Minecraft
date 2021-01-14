using System;

namespace Minecraft.Model
{
	public class CloneCommand : Command
	{
		public CloneCommand(Position begin, Position end, Position destination, MaskMode maskMode, CloneMode cloneMode)
			: base("clone")
		{
		}

		protected override string GetCommandTextImpl(MinecraftEdition edition)
		{
			throw new NotImplementedException();
		}
	}
}
