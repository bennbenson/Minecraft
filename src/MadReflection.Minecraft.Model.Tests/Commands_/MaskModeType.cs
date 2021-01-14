using System;

namespace Minecraft.Model.Tests
{
	public enum MaskModeType
	{
		Replace,
		Masked,
		Filtered
	}

	public static class MaskModeTypeExtensions
	{
		public static MaskMode Translate(this MaskModeType type)
		{
			return type switch
			{
				MaskModeType.Replace => MaskMode.Replace,
				MaskModeType.Masked => MaskMode.Masked,
				MaskModeType.Filtered => MaskMode.Filtered,
				_ => throw new ArgumentOutOfRangeException(nameof(type), $"Invalid {nameof(MaskModeType)} value.")
			};
		}
	}
}
