﻿namespace Minecraft.Model
{
	public sealed class MaskedMaskMode : MaskMode
	{
		internal MaskedMaskMode()
		{
		}


		protected override string GetArgumentTextImpl(Edition edition) => "masked";
	}
}
