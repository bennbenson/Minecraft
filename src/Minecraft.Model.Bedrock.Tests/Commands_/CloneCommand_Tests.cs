using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class CloneCommand_Tests
	{
		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, "/clone 5 103 9 10 105 12 5 80 9")]
		public void GetCommandText_Replace_Normal(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			CloneCommand command = new CloneCommand(begin, end, destination);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, "/clone 5 103 9 10 105 12 5 80 9 force")]
		public void GetCommandText_Replace_Force(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			CloneCommand command = new CloneCommand(begin, end, destination, cloneMode: CloneMode.Force);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, "/clone 5 103 9 10 105 12 5 80 9 move")]
		public void GetCommandText_Replace_Move(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			CloneCommand command = new CloneCommand(begin, end, destination, cloneMode: CloneMode.Move);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, "/clone 5 103 9 10 105 12 5 80 9 masked")]
		public void GetCommandText_Masked_Normal(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Masked);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, "/clone 5 103 9 10 105 12 5 80 9 masked force")]
		public void GetCommandText_Masked_Force(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Masked, CloneMode.Force);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, "/clone 5 103 9 10 105 12 5 80 9 masked move")]
		public void GetCommandText_Masked_Move(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Masked, CloneMode.Move);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.GoldBlock, 0, "/clone 5 103 9 10 105 12 5 80 9 filtered normal gold_block")]
		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.StainedGlass, 15, "/clone 5 103 9 10 105 12 5 80 9 filtered normal stained_glass 15")]
		public void GetCommandText_Filtered_Normal(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string filterBlockID, int filterBlockDataValue, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			Block filterBlock = Block.Get(filterBlockID, filterBlockDataValue);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Filtered.By(filterBlock), CloneMode.Normal);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.GoldBlock, 0, "/clone 5 103 9 10 105 12 5 80 9 filtered force gold_block")]
		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.StainedGlass, 15, "/clone 5 103 9 10 105 12 5 80 9 filtered force stained_glass 15")]
		public void GetCommandText_Filtered_Force(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string filterBlockID, int filterBlockDataValue, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			Block filterBlock = Block.Get(filterBlockID, filterBlockDataValue);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Filtered.By(filterBlock), CloneMode.Force);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.GoldBlock, 0, "/clone 5 103 9 10 105 12 5 80 9 filtered move gold_block")]
		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.StainedGlass, 15, "/clone 5 103 9 10 105 12 5 80 9 filtered move stained_glass 15")]
		public void GetCommandText_Filtered_Move(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string filterBlockID, int filterBlockDataValue, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			Block filterBlock = Block.Get(filterBlockID, filterBlockDataValue);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Filtered.By(filterBlock), CloneMode.Move);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
