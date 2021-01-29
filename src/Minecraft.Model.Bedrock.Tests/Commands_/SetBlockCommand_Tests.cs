using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class SetBlockCommand_Tests
	{
		[TestCase(0, 100, 0, BlockID.StoneBrick, DataValue.StoneBrick.Chiseled)]
		public void GetCommandText_Constructs_Correct_Command(int x, int y, int z, string id, int dataValue)
		{
			// Arrange
			Block block = Block.Get(id, dataValue);
			SetBlockCommand command = new SetBlockCommand(Position.Absolute(x, y, z), block);

			string expected = "/setblock 0 100 0 stonebrick 3";

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
