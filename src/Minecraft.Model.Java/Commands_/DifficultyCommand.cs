using System;
using System.Diagnostics;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class DifficultyCommand : Command
	{
		private const string CommandName = "difficulty";


		public DifficultyCommand()
			: base(CommandName)
		{
			Difficulty = null;
		}

		public DifficultyCommand(Difficulty difficulty)
			: base(CommandName)
		{
			if (difficulty < Model.Difficulty.Peaceful || difficulty > Model.Difficulty.Hard)
				throw new ArgumentOutOfRangeException(nameof(difficulty), "Difficulty level is out of range.");

			Difficulty = difficulty;
		}


		public Difficulty? Difficulty { get; }

		protected override Type EqualityContract => typeof(DifficultyCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			if (Difficulty is Difficulty difficulty)
				return $"/difficulty {difficulty.GetArgumentText()}";

			return "/difficulty";
		}
	}
}
