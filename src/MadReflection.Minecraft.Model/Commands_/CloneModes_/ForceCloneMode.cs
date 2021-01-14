namespace Minecraft.Model
{
	public class ForceCloneMode : CloneMode
	{
		protected override string GetArgumentTextImpl(Edition edition) => "force";
	}
}
