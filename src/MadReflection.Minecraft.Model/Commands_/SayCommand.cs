using System;

namespace Minecraft.Model
{
	public class SayCommand : Command
	{
		public SayCommand(string message)
			: base("say")
		{
			Message = message ?? "";
		}


		public string Message { get; }

		protected override Type EqualityContract => typeof(SayCommand);


		protected override string GetCommandTextImpl(Edition edition)
		{
			throw new NotImplementedException();
		}
	}
}
