using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class CloneCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			Block block = Block.GetByBedrockID("gold_block");
			CloneCommand command = new CloneCommand(
				Position.Absolute(5,103,9),
				Position.Absolute(10,105,12),
				Position.Absolute(5,80,9),
				MaskMode.Filtered.By(block),
				CloneMode.Normal
				);

			string expected = "/clone 5 103 9 10 105 12 5 80 9 filtered normal gold_block";

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
