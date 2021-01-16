using System;

namespace Minecraft.Model.Tests
{
	public enum CloneModeType
	{
		Force,
		Move,
		Normal
	}

	public static class CloneModeTypeExtension
	{
		public static CloneMode Translate(this CloneModeType type)
		{
			return type switch
			{
				CloneModeType.Force => CloneMode.Force,
				CloneModeType.Move => CloneMode.Move,
				CloneModeType.Normal => CloneMode.Normal,
				_ => throw new ArgumentOutOfRangeException(nameof(type), $"Invalid {nameof(CloneModeType)} value.")
			};
		}
	}
}
