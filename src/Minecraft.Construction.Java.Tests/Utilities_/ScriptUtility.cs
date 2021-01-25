using System.Collections.Generic;
using System.Linq;
using Minecraft.Model;

namespace Minecraft.Construction.Java.Tests
{
	public static class ScriptUtility
	{
		public static string GenerateScriptVariable(params IEnumerable<Command>[] commandSets)
		{
			return string.Join("\n", GetScriptLines(commandSets)) + "\n";
		}

		private static IEnumerable<string> GetScriptLines(IEnumerable<Command>[] commandSets)
		{
			yield return "let commands = [";
			foreach (string expression in IterateCommands(commandSets))
				yield return expression;
			yield return "];";
		}

		private static IEnumerable<string> IterateCommands(IEnumerable<Command>[] commandSets)
		{
			for (int setIndex = 0; setIndex < commandSets.Length; ++setIndex)
			{
				bool lastSet = setIndex == commandSets.Length - 1;

				if (setIndex > 0)
					yield return "";

				Command[] commandSet = commandSets[setIndex] as Command[] ?? commandSets[setIndex].ToArray();

				for (int index = 0; index < commandSet.Length; ++index)
				{
					bool lastCommand = lastSet && index == commandSet.Length - 1;

					Command command = commandSet[index];

					string line = "\t\"" + EscapeString(command.GetCommandText()) + "\"";
					if (!lastSet || !lastCommand)
						line += ",";

					yield return line;
				}
			}
		}

		private static string EscapeString(string value)
		{
			// This is horribly inefficient, and terribyl incomplete.

			if (value.Contains('\\'))
				value = value.Replace(@"\", @"\\");
			if (value.Contains('\"'))
				value = value.Replace(@"""", @"""""");

			return value;
		}
	}
}
