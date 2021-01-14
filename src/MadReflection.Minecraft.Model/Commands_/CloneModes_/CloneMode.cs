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

		public string GetArgumentText(Edition edition)
		{
			if (edition < Edition.Java || edition > Edition.Bedrock)
				throw new ArgumentOutOfRangeException(nameof(edition), $"Invalid {nameof(Edition)} value.");

			return GetArgumentTextImpl(edition);
		}

		protected abstract string GetArgumentTextImpl(Edition edition);
	}
}
