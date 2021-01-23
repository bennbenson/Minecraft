namespace Minecraft.Model.Java
{
	public abstract class FillMode
	{
		public static readonly DestroyFillMode Destroy = new DestroyFillMode();
		public static readonly HollowFillMode Hollow = new HollowFillMode();
		public static readonly KeepFillMode Keep = new KeepFillMode();
		public static readonly OutlineFillMode Outline = new OutlineFillMode();
		public static readonly ReplaceFillMode Replace = new ReplaceFillMode();


		private protected FillMode()
		{
		}


		public abstract string GetArgumentText();
	}
}
