namespace Minecraft.Model
{
	public class DestroyFillMode : FillMode
	{
		internal DestroyFillMode()
		{
		}


		protected override string GetArgumentTextImpl(Edition edition) => "destroy";
	}
}
