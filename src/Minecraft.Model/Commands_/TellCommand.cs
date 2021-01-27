using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TellCommand : MsgCommand
	{
		public TellCommand(string message)
			: base("tell", message)
		{
		}
	}
}
