namespace Minecraft.Model
{
	public class MoveCloneMode : CloneMode
	{
		protected override string GetArgumentTextImpl(Edition edition) => "move";
	}
}
