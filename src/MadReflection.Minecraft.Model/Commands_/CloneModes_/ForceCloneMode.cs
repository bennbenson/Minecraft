namespace Minecraft.Model
{
	public class ForceCloneMode : CloneMode
	{
		protected override string GetArgumentTextImpl(MinecraftEdition edition) => "force";
	}
}
