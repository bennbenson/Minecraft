using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class SetBlockCommand_Tests
	{
		[TestCase(0, 100, 0, BlockID.StoneBricks.Chiseled)]
		public void GetCommandText_Constructs_Correct_Command(int x, int y, int z, string id)
		{
			// Arrange
			Block block = Block.Get(id);
			SetBlockCommand command = new SetBlockCommand(Position.Absolute(x, y, z), block);

			string expected = "/setblock 0 100 0 chiseled_stone_bricks";

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
