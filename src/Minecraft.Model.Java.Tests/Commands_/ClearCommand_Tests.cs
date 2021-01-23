using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class ClearCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			Block block = Block.Get("gold_block");
			ClearCommand command = new ClearCommand("@a", block);

			string expected = "/clear @a gold_block";

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
