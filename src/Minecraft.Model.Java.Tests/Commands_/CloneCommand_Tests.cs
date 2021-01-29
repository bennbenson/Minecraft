using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class CloneCommand_Tests
	{
		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, "/clone 5 103 9 10 105 12 5 80 9")]
		public void GetCommandText_Replace_Normal(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3,string expected)
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

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.GoldBlock, "/clone 5 103 9 10 105 12 5 80 9 filtered gold_block")]
		public void GetCommandText_Filtered_Normal(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string filterBlockID, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			Block filterBlock = Block.Get(filterBlockID);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Filtered.By(filterBlock), CloneMode.Normal);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.GoldBlock, "/clone 5 103 9 10 105 12 5 80 9 filtered gold_block force")]
		public void GetCommandText_Filtered_Force(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string filterBlockID, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			Block filterBlock = Block.Get(filterBlockID);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Filtered.By(filterBlock), CloneMode.Force);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(5, 103, 9, 10, 105, 12, 5, 80, 9, BlockID.GoldBlock, "/clone 5 103 9 10 105 12 5 80 9 filtered gold_block move")]
		public void GetCommandText_Filtered_Move(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, string filterBlockID, string expected)
		{
			// Arrange
			Position begin = Position.Absolute(x1, y1, z1);
			Position end = Position.Absolute(x2, y2, z2);
			Position destination = Position.Absolute(x3, y3, z3);
			Block filterBlock = Block.Get(filterBlockID);
			CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Filtered.By(filterBlock), CloneMode.Move);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		//[TestCase]
		//public void GetCommandText_Filtered_With_Tag()
		//{
		//	// Arrange
		//	//Block block = Block.Get(BlockID.Stairs.Oak).WithPredicate(new Dictionary() { });
		//	PredicateBlock block = new PredicateBlock(Block.Get(BlockID.Stairs.Oak).With(...) /* with [] and {} tacked on.  How? */);
		//	Position begin = Position.Absolute(x1, y1, z1);
		//	Position end = Position.Absolute(x2, y2, z2);
		//	Position destination = Position.Absolute(x3, y3, z3);
		//	CloneCommand command = new CloneCommand(begin, end, destination, MaskMode.Filtered.By(block), CloneMode.Normal);

		//	string expected = "/clone 5 103 9 10 105 12 5 80 9 filtered oak_stairs{} normal";

		//	// Act
		//	string result = command.GetCommandText();

		//	// Assert
		//	Assert.That(result, Is.EqualTo(expected));
		//}
	}
}
