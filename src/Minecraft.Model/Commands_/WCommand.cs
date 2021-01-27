using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class WCommand : MsgCommand
	{
		public WCommand(string message)
			: base("w", message)
		{
		}
	}
}
