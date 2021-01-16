using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class SetBlockCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			Block block = Block.GetByBedrockID("stonebrick", StoneBrick.Chiseled);
			SetBlockCommand command = new SetBlockCommand(Position.Absolute(0, 100, 0), block);

			string expected = "/setblock 0 100 0 stonebrick 3";

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
