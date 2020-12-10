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

		public override string CommandText
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
