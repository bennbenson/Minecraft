using System;

namespace Minecraft.Model
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


		public string GetArgumentText(Edition edition)
		{
			if (edition < Edition.Java || edition > Edition.Bedrock)
				throw new ArgumentOutOfRangeException(nameof(edition), "Invalid MinecraftEdition value.");

			return GetArgumentTextImpl(edition);
		}

		protected abstract string GetArgumentTextImpl(Edition edition);
	}
}
