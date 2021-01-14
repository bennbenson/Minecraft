namespace Minecraft.Model
{
	public class NormalCloneMode : CloneMode
	{
		protected override string GetArgumentTextImpl(MinecraftEdition edition) => "normal";
	}
}
