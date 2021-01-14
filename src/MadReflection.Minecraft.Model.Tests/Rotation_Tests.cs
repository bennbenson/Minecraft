using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Rotation_Tests
	{
		[TestCase]
		public void Default_Is_No_Rotation()
		{
			// Arrange

			// Act
			Rotation result = new Rotation();

			// Assert
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Y.Value, Is.EqualTo(0.0f));
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.X.Value, Is.EqualTo(0.0f));
		}

		[TestCase(PositionType.Absolute, 0.0f, PositionType.Absolute, 0.0f, "0 0")]
		[TestCase(PositionType.Absolute, 1.2f, PositionType.Absolute, 7.1f, "1.2 7.1")]
		[TestCase(PositionType.Absolute, 1.2f, PositionType.Absolute, 7.1f, "1.2 7.1")]
		public void ToString_Creates_Correct_Value(PositionType yType, float yValue, PositionType xType, float xValue, string expected)
		{
			// Arrange
			Rotation input = new Rotation(new RelativeFloat(yType, yValue), new RelativeFloat(xType, xValue));

			// Act
			string result = input.ToString();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(PositionType.Absolute, 0.0f, PositionType.Absolute, 0.0f, MinecraftEdition.Bedrock, "0 0")]
		[TestCase(PositionType.Absolute, 1.2f, PositionType.Absolute, 7.1f, MinecraftEdition.Bedrock, "1.2 7.1")]
		[TestCase(PositionType.Absolute, 1.2f, PositionType.Absolute, 7.1f, MinecraftEdition.Java, "1.2 7.1")]
		public void GetArgumentText_Returns_Correct_Value(PositionType yType, float yValue, PositionType xType, float xValue, MinecraftEdition edition, string expected)
		{
			// Arrange
			Rotation input = new Rotation(new RelativeFloat(yType, yValue), new RelativeFloat(xType, xValue));

			// Act
			string result = input.GetArgumentText(edition);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
