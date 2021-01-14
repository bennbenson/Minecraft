using System;
using System.Text;

namespace Minecraft.Model
{
	public class TestForCommand : Command
	{
		public TestForCommand(VictimEntity victim)
			: base("testfor")
		{
			if (victim is null)
				throw new ArgumentNullException(nameof(victim));

			Victim = victim;
		}


		public VictimEntity Victim { get; }

		protected override Type EqualityContract => typeof(TestForCommand);


		protected override string GetCommandTextImpl(MinecraftEdition edition)
		{
			StringBuilder result = new StringBuilder();
			result.Append("/testfor ");
			result.Append(Victim.ToString());
			return result.ToString();
		}
	}
}
