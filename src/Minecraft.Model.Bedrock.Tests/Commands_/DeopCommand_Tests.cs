using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class DeopCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			DeopCommand command = new DeopCommand("@r");

			string expected = "/deop @r";

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
