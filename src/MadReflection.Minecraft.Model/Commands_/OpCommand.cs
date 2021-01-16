using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class OpCommand : Command
	{
		public OpCommand(TargetPlayer target)
			: base("op")
		{
			if (target is null)
				throw new ArgumentNullException(nameof(target));

			Target = target;
		}


		public TargetPlayer Target { get; }

		protected override Type EqualityContract => typeof(OpCommand);

		private string DebuggerDisplay => ToString();


		protected override string GetCommandTextImpl(Edition edition) => "/op " + Target.GetArgumentText(edition);
	}
}
