namespace Minecraft.Model
{
	public class OutlineFillMode : FillMode
	{
		internal OutlineFillMode()
		{
		}


		protected override string GetArgumentTextImpl(Edition edition) => "outline";
	}
}
