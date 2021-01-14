using System;

namespace Minecraft.Model.Tests
{
	// This enables the fill mode to be specified in the TestCase attribute since
	// FillMode is a reference type.  It's only needed in unit testing.

	public enum FillModeType
	{
		Destroy,
		Hollow,
		Keep,
		Outline,
		Replace
	}

	public static class FillModeTypeExtensions
	{
		public static FillMode Translate(this FillModeType type)
		{
			return type switch
			{
				FillModeType.Destroy => FillMode.Destroy,
				FillModeType.Hollow => FillMode.Hollow,
				FillModeType.Keep => FillMode.Keep,
				FillModeType.Outline => FillMode.Outline,
				FillModeType.Replace => FillMode.Replace,
				_ => throw new ArgumentOutOfRangeException(nameof(type), $"Invalid {nameof(FillModeType)} value.")
			};
		}
	}
}
