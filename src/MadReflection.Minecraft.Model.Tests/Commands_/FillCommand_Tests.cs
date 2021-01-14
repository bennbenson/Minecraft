using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class FillCommand_Tests
	{
		private static Block GetBlock(string? jeID, string? beID, int beDV)
		{
			if (jeID is not null)
				return Block.GetByJavaID(jeID);

			if (beID is not null)
				return Block.GetByBedrockID(beID, beDV);

			throw new ArgumentException("Test provides neither Java nor Bedrock ID.");
		}

		[TestCase(0, 65, 10, null, "stonebrick", StoneBrick.Normal, Edition.Bedrock, "/fill 0 65 10 10 65 0 stonebrick")]
		[TestCase(0, 65, 10, null, "stonebrick", StoneBrick.Normal, Edition.Java, "/fill 0 65 10 10 65 0 stone_bricks")]
		[TestCase(0, 65, 10, "stone_bricks", null, 0, Edition.Bedrock, "/fill 0 65 10 10 65 0 stonebrick")]
		[TestCase(0, 65, 10, "stone_bricks", null, 0, Edition.Java, "/fill 0 65 10 10 65 0 stone_bricks")]
		public void CommandText_0_Data(int axis1, int ground, int axis2, string? jeID, string? beID, int beDataValue, Edition edition, string expected)
		{
			// Arrange
			FillCommand command = new FillCommand(new Position(axis1, ground, axis2), new Position(axis2, ground, axis1), GetBlock(jeID, beID, beDataValue), FillMode.Replace);

			// Act
			string result = command.GetCommandText(edition);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 65, 10, "stone_bricks", null, 0, FillModeType.Replace, Edition.Java, "/fill 0 65 10 10 65 0 stone_bricks")]
		[TestCase(0, 65, 10, "stone_bricks", null, 0, FillModeType.Replace, Edition.Java, "/fill 0 65 10 10 65 0 stone_bricks")]
		[TestCase(0, 65, 10, null, "stonebrick", StoneBrick.Normal, FillModeType.Replace, Edition.Bedrock, "/fill 0 65 10 10 65 0 stonebrick")]
		[TestCase(0, 65, 10, null, "stonebrick", StoneBrick.Normal, FillModeType.Hollow, Edition.Bedrock, "/fill 0 65 10 10 65 0 stonebrick 0 hollow")]
		[TestCase(0, 65, 10, null, "stonebrick", StoneBrick.Mossy, FillModeType.Hollow, Edition.Bedrock, "/fill 0 65 10 10 65 0 stonebrick 1 hollow")]
		public void CommandText_Non_0_Data(int axis1, int ground, int axis2, string? jeID, string beID, int beDataValue, FillModeType fillModeType, Edition edition, string expected)
		{
			// Arrange
			FillCommand command = new FillCommand(new Position(axis1, ground, axis2), new Position(axis2, ground, axis1), GetBlock(jeID, beID, beDataValue), fillModeType.Translate());

			// Act
			string result = command.GetCommandText(edition);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void CommandText_Non_0_Data_Replace_0_Data()
		{
			// Arrange
			FillCommand command = new FillCommand(
				new Position(0, 65, 10),
				new Position(10, 65, 0),
				Block.GetByBedrockID("stonebrick", StoneBrick.Mossy),
				FillMode.Replace.With(Block.GetByBedrockID("stonebrick", StoneBrick.Normal))
				);

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick 1 replace stonebrick"));
		}

		[TestCase]
		public void CommandText_Non_0_Data_Replace_Non_0_Data()
		{
			// Arrange
			FillCommand command = new FillCommand(
				new Position(0, 65, 10),
				new Position(10, 65, 0),
				Block.GetByBedrockID("stonebrick", StoneBrick.Mossy),
				FillMode.Replace.With(Block.GetByBedrockID("stonebrick", StoneBrick.Chiseled))
				);

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick 1 replace stonebrick 3"));
		}

		[TestCase]
		public void CommandText_0_Data_Hollow()
		{
			// Arrange
			FillCommand command = new FillCommand(
				new Position(0, 65, 10),
				new Position(10, 65, 0),
				Block.GetByBedrockID("stonebrick"),
				FillMode.Hollow
				);

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo("/fill 0 65 10 10 65 0 stonebrick 0 hollow"));
		}
	}
}
