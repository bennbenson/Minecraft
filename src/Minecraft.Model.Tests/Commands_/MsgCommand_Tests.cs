using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class MsgCommand_Tests
	{
		[TestCase("come with me", "/msg come with me")]
		public void GetCommandText_Returns_Correct_Command(string message, string expected)
		{
			// Arrange
			MsgCommand command = new MsgCommand(message);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
