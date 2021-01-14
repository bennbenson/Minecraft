using System.Text;

namespace Minecraft.Model
{
	public class OpCommand : Command
	{
		public OpCommand()
			: base("op")
		{
		}


		protected override string GetCommandTextImpl(Edition edition)
		{
			StringBuilder sb = new();

			sb.Append("/op ");
			//sb.Append()

			return sb.ToString();
		}
	}
}
