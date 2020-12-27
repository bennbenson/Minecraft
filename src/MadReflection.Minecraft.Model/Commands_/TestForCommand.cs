using System;

namespace Minecraft.Model
{
	// https://minecraft.gamepedia.com/Commands/testfor
	[Obsolete("Incomplete!")]
	public class TestForCommand : Command
	{
		public TestForCommand()
			: base("testfor")
		{
		}


		protected override Type EqualityContract => typeof(TestForCommand);


		protected override string GetCommandTextImpl(MinecraftEdition edition)
		{
			throw new NotImplementedException();
		}
	}
}
