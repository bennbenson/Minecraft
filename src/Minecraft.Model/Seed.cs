using System;
using System.Linq;

namespace Minecraft.Model
{
	public static class Seed
	{
		public static int GetFromString(string input)
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
