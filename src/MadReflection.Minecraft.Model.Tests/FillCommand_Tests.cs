using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class FillCommand_Tests
	{
		[TestCase]
		public void CommandText_0_Data()
		{
			// Arrange
			FillCommand command = new FillCommand(new Coord3(0, 65, 10), new Coord3(10, 65, 0), Block.Get("stonebrick", StoneBrick.Normal), FillMode.Replace);

			// Act
			string result = command.CommandText;

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick"));
		}

		[TestCase]
		public void CommandText_Non_0_Data()
		{
			// Arrange
			FillCommand command = new FillCommand(new Coord3(0, 65, 10), new Coord3(10, 65, 0), Block.Get("stonebrick", StoneBrick.Mossy), FillMode.Replace);

			// Act
			string result = command.CommandText;

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick 1"));
		}

		[TestCase]
		public void CommandText_Non_0_Data_Replace_0_Data()
		{
			// Arrange
			FillCommand command = new FillCommand(
				new Coord3(0, 65, 10),
				new Coord3(10, 65, 0),
				Block.Get("stonebrick", StoneBrick.Mossy),
				FillMode.Replace.With(Block.Get("stonebrick", StoneBrick.Normal))
				);

			// Act
			string result = command.CommandText;

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick 1 replace stonebrick"));
		}

		[TestCase]
		public void CommandText_Non_0_Data_Replace_Non_0_Data()
		{
			// Arrange
			FillCommand command = new FillCommand(
				new Coord3(0, 65, 10),
				new Coord3(10, 65, 0),
				Block.Get("stonebrick", StoneBrick.Mossy),
				FillMode.Replace.With(Block.Get("stonebrick", StoneBrick.Chiseled))
				);

			// Act
			string result = command.CommandText;

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick 1 replace stonebrick 3"));
		}

		[TestCase]
		public void CommandText_0_Data_Hollow()
		{
			// Arrange
			FillCommand command = new FillCommand(
				new Coord3(0, 65, 10),
				new Coord3(10, 65, 0),
				Block.Get("stonebrick"),
				FillMode.Hollow
				);

			// Act
			string result = command.CommandText;

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick 0 hollow"));
		}
	}
}
