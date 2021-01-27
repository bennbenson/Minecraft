using System;
using System.Reflection;

namespace Minecraft.Model
{
	internal static class EnumExtensions
	{
		public static string GetArgumentText<TEnum>(this TEnum value)
			where TEnum : struct, Enum
		{
			return typeof(TEnum).GetField(value.ToString())?.GetCustomAttribute<ArgumentTextAttribute>()?.ArgumentText ?? "";
		}
	}
}
