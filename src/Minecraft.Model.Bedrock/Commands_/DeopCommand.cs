using System;
using System.Diagnostics;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class DeopCommand : Command
	{
		public DeopCommand(TargetPlayer target)
			: base("deop")
		{
			if (target is null)
				throw new ArgumentNullException(nameof(target));

			Target = target;
		}


		public TargetPlayer Target { get; }

		protected override Type EqualityContract => typeof(DeopCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText() => "/deop " + Target.GetArgumentText();
	}
}
