using System;
using System.Reflection;
using System.Text;

namespace Minecraft.Model
{
	internal static class ArgumentTextExtensions
	{
		public static string GetArgumentText<TEnum>(this TEnum value)
			where TEnum : struct, Enum
		{
			return typeof(TEnum).GetField(value.ToString())?.GetCustomAttribute<ArgumentTextAttribute>()?.ArgumentText ?? "";
		}

		public static string GetArgumentText(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return @"""""";

			int index = 0;
			for (; index < value.Length; ++index)
			{
				char ch = value[index];

				if (ch is ' ' or '"' or '\'' or '\\' or '\r' or '\n' or '\t' or '\v' or '\a')
					break;
			}
			if (index == value.Length)
				return value;

			StringBuilder result = new StringBuilder(value.Length + 10);
			result.Append('"');
			result.Append(value[..index]);

			for (; index < value.Length; ++index)
			{
				char ch = value[index];

				if (ch == '"')
					result.Append("\\\"");
				else if (ch == '\\')
					result.Append("\\\\");
				else if (ch == '\r')
					result.Append("\\\r");
				else if (ch == '\n')
					result.Append("\\\n");
				else if (ch == '\t')
					result.Append("\\\t");
				else if (ch == '\v')
					result.Append("\\\v");
				else if (ch == '\a')
					result.Append("\\\a");
				else
					result.Append(ch);
			}

			result.Append('"');
			return result.ToString();
		}
	}
}
