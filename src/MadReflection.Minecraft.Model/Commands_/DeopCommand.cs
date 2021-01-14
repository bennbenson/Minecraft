using System.Text;

namespace Minecraft.Model
{
	public class DeopCommand : Command
	{
		public DeopCommand()
			: base("deop")
		{
		}


		protected override string GetCommandTextImpl(MinecraftEdition edition)
		{
			StringBuilder result = new();

			return result.ToString();
		}
	}
}
