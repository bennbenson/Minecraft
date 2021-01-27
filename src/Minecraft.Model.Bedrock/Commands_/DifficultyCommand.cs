using System;
using System.Diagnostics;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class DifficultyCommand : Command
	{
		private const string CommandName = "difficulty";

		private readonly byte _argumentForm;


		public DifficultyCommand()
			: base(CommandName)
		{
			Difficulty = null;
			_argumentForm = 0;
		}

		public DifficultyCommand(Difficulty difficulty)
			: base(CommandName)
		{
			if (difficulty < Model.Difficulty.Peaceful || difficulty > Model.Difficulty.Hard)
				throw new ArgumentOutOfRangeException(nameof(difficulty), "Difficulty level is out of range.");

			Difficulty = difficulty;
			_argumentForm = 1;
		}

		public DifficultyCommand(int difficulty)
			: base(CommandName)
		{
			if (difficulty < 0 || difficulty > 3)
				throw new ArgumentOutOfRangeException(nameof(difficulty), "Difficulty level is out of range.");

			Difficulty = (Difficulty)difficulty;
			_argumentForm = 2;
		}

		public DifficultyCommand(char ch)
			: base(CommandName)
		{
			Difficulty = ch switch
			{
				'p' => Model.Difficulty.Peaceful,
				'e' => Model.Difficulty.Easy,
				'n' => Model.Difficulty.Normal,
				'h' => Model.Difficulty.Hard,
				_ => throw new ArgumentOutOfRangeException(nameof(ch), "Invalid difficulty level character.")
			};
			_argumentForm = 3;
		}


		public Difficulty? Difficulty { get; }

		protected override Type EqualityContract => typeof(DifficultyCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			if (_argumentForm == 3)
			{
				string level = Difficulty switch
				{
					Model.Difficulty.Peaceful => "p",
					Model.Difficulty.Easy => "e",
					Model.Difficulty.Normal => "n",
					Model.Difficulty.Hard => "h",
					_ => throw null!
				};
				return $"/difficulty {level}";
			}

			if (_argumentForm == 2)
				return $"/difficulty {(int?)Difficulty}";

			if (_argumentForm == 1)
				return $"/difficulty {Difficulty?.GetArgumentText()}";

			return "/difficulty";
		}
	}
}
