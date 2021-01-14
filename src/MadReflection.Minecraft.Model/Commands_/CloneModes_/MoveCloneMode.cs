namespace Minecraft.Model
{
	public class MoveCloneMode : CloneMode
	{
		protected override string GetArgumentTextImpl(MinecraftEdition edition) => "move";
	}
}
