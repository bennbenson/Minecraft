using System;

namespace Minecraft.Model
{
	public abstract class CloneMode
	{
		public static readonly CloneMode Force = new ForceCloneMode();
		public static readonly CloneMode Move = new MoveCloneMode();
		public static readonly CloneMode Normal = new NormalCloneMode();


		private protected CloneMode()
		{
		}

		public string GetArgumentText(MinecraftEdition edition)
		{
			if (edition < MinecraftEdition.Java || edition > MinecraftEdition.Bedrock)
				throw new ArgumentOutOfRangeException(nameof(edition), $"Invalid {nameof(MinecraftEdition)} value.");

			return GetArgumentTextImpl(edition);
		}

		protected abstract string GetArgumentTextImpl(MinecraftEdition edition);
	}
}
