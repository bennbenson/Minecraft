using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class ClearCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			Block block = Block.GetByBedrockID("gold_block");
			ClearCommand command = new ClearCommand("@a", block);

			string expected = "/clear @a gold_block";

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
