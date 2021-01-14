namespace Minecraft.Model
{
	public sealed class ReplaceMaskMode : MaskMode
	{
		internal ReplaceMaskMode()
		{
		}


		protected override string GetArgumentTextImpl(MinecraftEdition edition) => "replace";
	}
}
