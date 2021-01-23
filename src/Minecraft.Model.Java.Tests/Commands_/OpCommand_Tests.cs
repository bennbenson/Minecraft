using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class OpCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			OpCommand command = new OpCommand("@a");

			string expected = "/op @a";

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
