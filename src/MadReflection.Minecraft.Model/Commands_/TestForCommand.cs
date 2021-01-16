using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TestForCommand : Command
	{
		public TestForCommand(TargetPlayer victim)
			: base("testfor")
		{
			if (victim is null)
				throw new ArgumentNullException(nameof(victim));

			Victim = victim;
		}


		public TargetPlayer Victim { get; }

		protected override Type EqualityContract => typeof(TestForCommand);

		private string DebuggerDisplay => ToString();


		protected override string GetCommandTextImpl(Edition edition)
		{
			StringBuilder result = new StringBuilder();
			result.Append("/testfor ");
			result.Append(Victim.GetArgumentText(edition));
			return result.ToString();
		}
	}
}
