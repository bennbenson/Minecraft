using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class FillCommand_Tests
	{
		[TestCase(0, 65, 10, BlockID.StoneBricks.Normal, "/fill 0 65 10 10 65 0 stone_bricks")]
		public void GetCommandText_Replace(int axis1, int ground, int axis2, string id, string expected)
		{
			// Arrange
			Position from = new Position(axis1, ground, axis2);
			Position to = new Position(axis2, ground, axis1);
			Block block = Block.Get(id);
			FillCommand command = new FillCommand(from, to, block, FillMode.Replace);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 65, 10, BlockID.StoneBricks.Mossy, BlockID.StoneBricks.Chiseled, "/fill 0 65 10 10 65 0 mossy_stone_bricks replace chiseled_stone_bricks")]
		public void GetCommandText_Replace_Filtered(int axis1, int ground, int axis2, string fromID, string toID, string expected)
		{
			// Arrange
			Position from = new Position(axis1, ground, axis2);
			Position to = new Position(axis2, ground, axis1);
			Block fromBlock = Block.Get(fromID);
			Block toBlock = Block.Get(toID);
			FillCommand command = new FillCommand(from, to, fromBlock, FillMode.Replace.With(toBlock));

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 65, 10, BlockID.StoneBricks.Normal, "/fill 0 65 10 10 65 0 stone_bricks hollow")]
		public void GetCommandText_Hollow(int axis1, int ground, int axis2, string id, string expected)
		{
			// Arrange
			Position from = new Position(axis1, ground, axis2);
			Position to = new Position(axis2, ground, axis1);
			Block block = Block.Get(id);
			FillCommand command = new FillCommand(from, to, block, FillMode.Hollow);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
