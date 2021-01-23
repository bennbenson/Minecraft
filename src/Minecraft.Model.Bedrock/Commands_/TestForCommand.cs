using System;
using System.Diagnostics;

namespace Minecraft.Model.Bedrock
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


		public override string GetCommandText() => $"/testfor {Victim.GetArgumentText()}";
	}
}
