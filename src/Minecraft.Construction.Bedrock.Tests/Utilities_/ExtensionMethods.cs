using System;
using System.Collections.Generic;
using System.Linq;
using Minecraft.Model;

namespace Minecraft.Construction.Bedrock.Tests
{
	public static class ExtensionMethods
	{
		public static IEnumerable<string> ProjectCommandText(this IEnumerable<Command> commands)
		{
			if (commands is null)
				throw new ArgumentNullException(nameof(commands));

			return commands.Select(c => c.GetCommandText());
		}
	}
}
