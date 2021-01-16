using NUnit.Framework;

namespace Minecraft.Model.Tests
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
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
