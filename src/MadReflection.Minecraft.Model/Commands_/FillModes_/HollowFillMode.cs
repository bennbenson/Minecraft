namespace Minecraft.Model
{
	public class HollowFillMode : FillMode
	{
		internal HollowFillMode()
		{
		}


		protected override string GetArgumentTextImpl(MinecraftEdition edition) => "hollow";
	}
}
