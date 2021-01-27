using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class MeCommand_Tests
	{
		[TestCase("has too much inventory", "/me has too much inventory")]
		public void GetCommandText_Returns_Correct_Command(string message, string expected)
		{
			// Arrange
			MeCommand command = new MeCommand(message);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
