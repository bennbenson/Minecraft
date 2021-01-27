using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class MsgCommand : Command
	{
		public MsgCommand(string message)
			: base("msg")
		{
			Message = message ?? "";
		}

		private protected MsgCommand(string name, string message)
			: base(name)
		{
			Message = message ?? "";
		}


		public string Message { get; }

		protected override Type EqualityContract => typeof(MsgCommand);

		private protected string DebuggerDisplay => ToString();


		public override string GetCommandText() => $"/{Name} {Message}";
	}
}
