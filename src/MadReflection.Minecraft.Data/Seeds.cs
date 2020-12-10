using System;
using System.Linq;

namespace Minecraft.Data
{
	public static class Seeds
	{
		public static int GetSeedFromString(string input)
		{
			if (input is null)
				throw new ArgumentNullException(nameof(input));

			input = input.Trim();

			if (int.TryParse(input, out int intResult))
				return intResult;

			return input.Aggregate(0, (h, c) => h * 31 + c);
		}
	}
}
