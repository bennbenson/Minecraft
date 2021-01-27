using System;
using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class DifficultyCommand_Tests
	{
		[TestCase(-1)]
		[TestCase(4)]
		public void Constructor_Validates_Difficulty_Enum_Range(int input)
		{
			// Arrange

			// Act
			TestDelegate test = () => new DifficultyCommand((Difficulty)input);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase("/difficulty")]
		public void GetCommandText_for_query(string expected)
		{
			// Arrange
			DifficultyCommand command = new DifficultyCommand();

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(Difficulty.Peaceful, "/difficulty peaceful")]
		[TestCase(Difficulty.Easy, "/difficulty easy")]
		[TestCase(Difficulty.Normal, "/difficulty normal")]
		[TestCase(Difficulty.Hard, "/difficulty hard")]
		public void GetCommandText_with_enum(Difficulty input, string expected)
		{
			// Arrange
			DifficultyCommand command = new DifficultyCommand(input);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
