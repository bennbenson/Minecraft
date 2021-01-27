using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class WCommand_Tests
	{
		[TestCase("come with me", "/w come with me")]
		public void GetCommandText_Returns_Correct_Command(string message, string expected)
		{
			// Arrange
			WCommand command = new WCommand(message);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
