using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class TellCommand_Tests
	{
		[TestCase("come with me", "/tell come with me")]
		public void GetCommandText_Returns_Correct_Command(string message, string expected)
		{
			// Arrange
			TellCommand command = new TellCommand(message);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
