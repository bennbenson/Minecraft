﻿using System;

namespace Minecraft.Model
{
	public abstract class MaskMode
	{
		public static readonly ReplaceMaskMode Replace = new ReplaceMaskMode();
		public static readonly MaskedMaskMode Masked = new MaskedMaskMode();
		public static readonly FilteredMaskMode Filtered = new FilteredMaskMode();


		private protected MaskMode()
		{
		}

		public string GetArgumentText(MinecraftEdition edition)
		{
			if (edition < MinecraftEdition.Java || edition > MinecraftEdition.Bedrock)
				throw new ArgumentOutOfRangeException(nameof(edition), "Invalid MinecraftEdition value.");

			return GetArgumentTextImpl(edition);
		}

		protected abstract string GetArgumentTextImpl(MinecraftEdition edition);
	}
}