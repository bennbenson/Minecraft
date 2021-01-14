using System.Text;

namespace Minecraft.Model
{
	public class DeopCommand : Command
	{
		public DeopCommand()
			: base("deop")
		{
		}


		protected override string GetCommandTextImpl(Edition edition)
		{
			StringBuilder result = new();

			return result.ToString();
		}
	}
}
