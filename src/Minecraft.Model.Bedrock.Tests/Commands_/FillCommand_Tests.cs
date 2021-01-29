using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class FillCommand_Tests
	{
		[TestCase(0, 65, 10, BlockID.StoneBrick, DataValue.StoneBrick.Normal, "/fill 0 65 10 10 65 0 stonebrick")]
		[TestCase(0, 65, 10, BlockID.StoneBrick, DataValue.StoneBrick.Mossy, "/fill 0 65 10 10 65 0 stonebrick 1")]
		public void GetCommandText_Replace(int axis1, int ground, int axis2, string id, int dataValue, string expected)
		{
			// Arrange
			Position from = new Position(axis1, ground, axis2);
			Position to = new Position(axis2, ground, axis1);
			Block block = Block.Get(id, dataValue);
			FillCommand command = new FillCommand(from, to, block, FillMode.Replace);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 65, 10, BlockID.StoneBrick, DataValue.StoneBrick.Mossy, DataValue.StoneBrick.Normal, "/fill 0 65 10 10 65 0 stonebrick 1 replace stonebrick")]
		[TestCase(0, 65, 10, BlockID.StoneBrick, DataValue.StoneBrick.Mossy, DataValue.StoneBrick.Chiseled, "/fill 0 65 10 10 65 0 stonebrick 1 replace stonebrick 3")]
		public void GetCommandText_Replace_Filtered(int axis1, int ground, int axis2, string id, int dataValueFrom, int dataValueTo, string expected)
		{
			// Arrange
			Position from = new Position(axis1, ground, axis2);
			Position to = new Position(axis2, ground, axis1);
			Block fromBlock = Block.Get(id, dataValueFrom);
			Block toBlock = Block.Get(id, dataValueTo);
			FillCommand command = new FillCommand(from, to, fromBlock, FillMode.Replace.With(toBlock));

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 65, 10, BlockID.StoneBrick, DataValue.StoneBrick.Normal, "/fill 0 65 10 10 65 0 stonebrick 0 hollow")]
		[TestCase(0, 65, 10, BlockID.StoneBrick, DataValue.StoneBrick.Mossy, "/fill 0 65 10 10 65 0 stonebrick 1 hollow")]
		public void GetCommandText_Hollow(int axis1, int ground, int axis2, string id, int dataValue, string expected)
		{
			// Arrange
			Position from = new Position(axis1, ground, axis2);
			Position to = new Position(axis2, ground, axis1);
			Block block = Block.Get(id, dataValue);
			FillCommand command = new FillCommand(from, to, block, FillMode.Hollow);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
