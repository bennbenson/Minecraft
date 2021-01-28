using System;
using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class ParticleCommand_Tests
	{
		[TestCase]
		public void Constructor_Throws_On_Null_Effect()
		{
			// Arrange

			// Act
			TestDelegate test = () => new ParticleCommand(null!, (0, 0, 0));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentNullException>());
		}

		[TestCase]
		public void Constructor_Throws_On_Empty_Effect()
		{
			// Arrange

			// Act
			TestDelegate test = () => new ParticleCommand("", (0, 0, 0));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}

		[TestCase("dust", 0, 120, 0, "/particle dust 0 120 0")]
		[TestCase("crazy stuff", 0, 120, 0, "/particle \"crazy stuff\" 0 120 0")]
		public void GetCommandText_Constructs_Correct_Command(string effect, int x, int y, int z, string expected)
		{
			// Arrange
			ParticleCommand command = new ParticleCommand(effect, (x, y, z));

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
