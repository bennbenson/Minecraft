namespace Minecraft.Model
{
	public class NormalCloneMode : CloneMode
	{
		protected override string GetArgumentTextImpl(Edition edition) => "normal";
	}
}
