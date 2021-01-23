using System;
using System.Diagnostics;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class SayCommand : Command
	{
		public SayCommand(string message)
			: base("say")
		{
			Message = message ?? "";
		}


		public string Message { get; }

		protected override Type EqualityContract => typeof(SayCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText() => $"/say {Message}";
	}
}
