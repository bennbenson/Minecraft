using System;
using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
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

		[TestCase(-1)]
		[TestCase(4)]
		public void Constructor_Validates_int_Range(int input)
		{
			// Arrange

			// Act
			TestDelegate test = () => new DifficultyCommand(input);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase('a')]
		[TestCase('d')]
		[TestCase('f')]
		[TestCase('g')]
		[TestCase('i')]
		[TestCase('m')]
		[TestCase('o')]
		[TestCase('q')]
		[TestCase('z')]
		public void Constructor_Validates_char_range(char input)
		{
			// Arrange

			// Act
			TestDelegate test = () => new DifficultyCommand(input);

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

		[TestCase(0, "/difficulty 0")]
		[TestCase(1, "/difficulty 1")]
		[TestCase(2, "/difficulty 2")]
		[TestCase(3, "/difficulty 3")]
		public void GetCommandText_with_int(int input, string expected)
		{
			// Arrange
			DifficultyCommand command = new DifficultyCommand(input);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase('p', "/difficulty p")]
		[TestCase('e', "/difficulty e")]
		[TestCase('n', "/difficulty n")]
		[TestCase('h', "/difficulty h")]
		public void GetCommandText_with_char(char input, string expected)
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
