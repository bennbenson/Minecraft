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

		public override string CommandText => $"/{Name} {Message}";
	}
}
