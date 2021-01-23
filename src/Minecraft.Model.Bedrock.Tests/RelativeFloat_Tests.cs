using System;
using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class RelativeFloat_Tests
	{
		[TestCase]
		public void Default_Is_Absolute_Zero()
		{
			// Arrange

			// Act
			RelativeFloat result = new RelativeFloat();

			// Assert
			Assert.That(result.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Value, Is.EqualTo(0.0f));
		}

		[TestCase(PositionType.Absolute, 0.0f)]
		[TestCase(PositionType.Relative, 0.0f)]
		[TestCase(PositionType.Local, 0.0f)]
		public void Constructor_Retains_Type(PositionType type, float value)
		{
			// Arrange
			RelativeFloat input = new RelativeFloat(type, value);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(type));
		}

		[TestCase((PositionType)(-1))]
		[TestCase((PositionType)3)]
		public void Constructor_Validates_Type(PositionType type)
		{
			// Arrange

			// Act
			TestDelegate test = () => new RelativeFloat(type, 0.0f);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase("0", PositionType.Absolute, 0.0f)]
		[TestCase("1.1", PositionType.Absolute, 1.1f)]
		[TestCase("-4.5", PositionType.Absolute, -4.5f)]
		[TestCase("+7.2", PositionType.Absolute, 7.2f)]
		[TestCase("~1.8", PositionType.Relative, 1.8f)]
		[TestCase("~-1.3", PositionType.Relative, -1.3f)]
		[TestCase("^7.2", PositionType.Local, 7.2f)]
		[TestCase("^-4.1", PositionType.Local, -4.1f)]
		public void Parse_Succeeds_On_Valid_Inputs(string input, PositionType type, float value)
		{
			// Arrange

			// Act
			RelativeFloat result = RelativeFloat.Parse(input);

			// Assert
			Assert.That(result.Type, Is.EqualTo(type));
			Assert.That(result.Value, Is.EqualTo(value));
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(FormatException))]
		public void Parse_Throws_On_Invalid_Inputs(string input, Type expectedException)
		{
			// Arrange

			// Act
			TestDelegate test = () => RelativeFloat.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf(expectedException));
		}

		[TestCase("0", true, PositionType.Absolute, 0.0f)]
		[TestCase("1.1", true, PositionType.Absolute, 1.1f)]
		[TestCase("-4.5", true, PositionType.Absolute, -4.5f)]
		[TestCase("+7.2", true, PositionType.Absolute, 7.2f)]
		[TestCase("~1.8", true, PositionType.Relative, 1.8f)]
		[TestCase("~-1.3", true, PositionType.Relative, -1.3f)]
		[TestCase("^7.2", true, PositionType.Local, 7.2f)]
		[TestCase("^-4.1", true, PositionType.Local, -4.1f)]
		[TestCase(null, false, default(PositionType), default(float))]
		[TestCase("", false, default(PositionType), default(float))]
		[TestCase("x", false, default(PositionType), default(float))]
		public void TryParse_Succeeds(string input, bool expected, PositionType expectedType, float expectedValue)
		{
			// Arrange

			// Act
			bool result = RelativeFloat.TryParse(input, out RelativeFloat output);

			// Assert
			if (expected)
			{
				Assert.That(result, Is.True);
				Assert.That(output.Type, Is.EqualTo(expectedType));
				Assert.That(output.Value, Is.EqualTo(expectedValue));
			}
			else
			{
				Assert.That(result, Is.False);
			}
		}

		[TestCase]
		public void Absolute_Creates_Absolute_Value()
		{
			// Arrange
			RelativeFloat input = RelativeFloat.Absolute(0.0f);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase]
		public void Relative_Creates_Relative_Value()
		{
			// Arrange
			RelativeFloat input = RelativeFloat.Relative(0.0f);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Relative));
		}

		[TestCase]
		public void Local_Creates_Local_Value()
		{
			// Arrange
			RelativeFloat input = RelativeFloat.Local(0.0f);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Local));
		}


		[TestCase(PositionType.Absolute, 0, "0")]
		[TestCase(PositionType.Relative, 0, "~")]
		[TestCase(PositionType.Local, 0, "^")]
		[TestCase(PositionType.Absolute, 1.5f, "1.5")]
		[TestCase(PositionType.Relative, 1.5f, "~1.5")]
		[TestCase(PositionType.Local, 1.5f, "^1.5")]
		public void ToString_Includes_Correct_Prefix(PositionType type, float value, string expected)
		{
			// Arrange
			RelativeFloat input = new RelativeFloat(type, value);

			// Act
			string result = input.ToString();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
