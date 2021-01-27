using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class MeCommand : Command
	{
		public MeCommand(string message)
			: base("me")
		{
			Message = message ?? "";
		}


		public string Message { get; }

		protected override Type EqualityContract => typeof(MeCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText() => $"/me {Message}";
	}
}
