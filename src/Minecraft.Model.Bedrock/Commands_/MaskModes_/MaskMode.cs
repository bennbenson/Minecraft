namespace Minecraft.Model.Bedrock
{
	public abstract class MaskMode
	{
		public static readonly ReplaceMaskMode Replace = new ReplaceMaskMode();
		public static readonly MaskedMaskMode Masked = new MaskedMaskMode();
		public static readonly FilteredMaskMode Filtered = new FilteredMaskMode();


		private protected MaskMode()
		{
		}


		public abstract string GetArgumentText();
	}
}
