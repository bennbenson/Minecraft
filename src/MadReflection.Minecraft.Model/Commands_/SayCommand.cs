using System;
using System.Diagnostics;

namespace Minecraft.Model
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


		protected override string GetCommandTextImpl(Edition edition)
		{
			return $"/say {Message}";
		}
	}
}
