namespace Minecraft.Model
{
	public class KeepFillMode : FillMode
	{
		internal KeepFillMode()
		{
		}


		protected override string GetArgumentTextImpl(MinecraftEdition edition) => "keep";
	}
}
