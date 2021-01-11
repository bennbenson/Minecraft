using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class PositionValue_Tests
	{
		[TestCase]
		public void Default_Is_Absolute()
		{
			// Arrange

			// Act
			PositionType result = new PositionValue().Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase(PositionType.Absolute, 0)]
		[TestCase(PositionType.Relative, 0)]
		[TestCase(PositionType.Local, 0)]
		public void Constructor_Retains_Type(PositionType type, int value)
		{
			// Arrange
			PositionValue input = new PositionValue(type, value);

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
			TestDelegate test = () => new PositionValue(type, 0);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(PositionType.Absolute, 50)]
		public void Constructor_Retains_Value(PositionType type, int value)
		{
			// Arrange
			PositionValue input = new PositionValue(type, value);

			// Act
			int result = input.Value;

			// Assert
			Assert.That(result, Is.EqualTo(value));
		}

		[TestCase("0", PositionType.Absolute, 0)]
		[TestCase("51", PositionType.Absolute, 51)]
		[TestCase("-16", PositionType.Absolute, -16)]
		[TestCase("~", PositionType.Relative, 0)]
		[TestCase("^", PositionType.Local, 0)]
		[TestCase("~1", PositionType.Relative, 1)]
		[TestCase("~-1", PositionType.Relative, -1)]
		[TestCase("~+1", PositionType.Relative, 1)]
		[TestCase("^1", PositionType.Local, 1)]
		[TestCase("^-1", PositionType.Local, -1)]
		[TestCase("^+1", PositionType.Local, 1)]
		public void Parse_Succeeds_On_Valid_Inputs(string input, PositionType type, int value)
		{
			// Arrange

			// Act
			var result = PositionValue.Parse(input);

			// Assert
			Assert.That(result.Type, Is.EqualTo(type));
			Assert.That(result.Value, Is.EqualTo(value));
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(FormatException))]
		[TestCase("x", typeof(FormatException))]
		[TestCase("+1", typeof(FormatException))]
		public void Parse_Throws_On_Invalid_Inputs(string input, Type expectedException)
		{
			// Arrange

			// Act
			TestDelegate test = () => Position.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf(expectedException));
		}

		//[TestCase("", true)]
		public void TryParse________(string input, bool expected)
		{
			// Arrange

			// Act

			// Assert
		}

		[TestCase]
		public void Absolute_Creates_Absolute_Value()
		{
			// Arrange
			PositionValue input = new PositionValue(PositionType.Absolute, 0);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase]
		public void Relative_Creates_Relative_Value()
		{
			// Arrange
			PositionValue input = new PositionValue(PositionType.Relative, 0);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Relative));
		}

		[TestCase]
		public void Local_Creates_Local_Value()
		{
			// Arrange
			PositionValue input = new PositionValue(PositionType.Local, 0);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Local));
		}

		[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, true)]
		[TestCase(PositionType.Relative, 0, PositionType.Relative, 0, true)]
		[TestCase(PositionType.Local, 0, PositionType.Local, 0, true)]
		[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, false)]
		[TestCase(PositionType.Absolute, 0, PositionType.Local, 0, false)]
		[TestCase(PositionType.Relative, 0, PositionType.Absolute, 0, false)]
		[TestCase(PositionType.Relative, 0, PositionType.Local, 0, false)]
		[TestCase(PositionType.Local, 0, PositionType.Absolute, 0, false)]
		[TestCase(PositionType.Local, 0, PositionType.Relative, 0, false)]
		public void Operator_Equality_Compares_Correctly(PositionType leftType, int leftValue, PositionType rightType, int rightValue, bool expected)
		{
			// Arrange
			PositionValue left = new PositionValue(leftType, leftValue);
			PositionValue right = new PositionValue(rightType, rightValue);

			// Act
			bool result = left == right;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, false)]
		[TestCase(PositionType.Relative, 0, PositionType.Relative, 0, false)]
		[TestCase(PositionType.Local, 0, PositionType.Local, 0, false)]
		[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, true)]
		[TestCase(PositionType.Absolute, 0, PositionType.Local, 0, true)]
		[TestCase(PositionType.Relative, 0, PositionType.Absolute, 0, true)]
		[TestCase(PositionType.Relative, 0, PositionType.Local, 0, true)]
		[TestCase(PositionType.Local, 0, PositionType.Absolute, 0, true)]
		[TestCase(PositionType.Local, 0, PositionType.Relative, 0, true)]
		public void Operator_Inequality_Compares_Correctly(PositionType leftType, int leftValue, PositionType rightType, int rightValue, bool expected)
		{
			// Arrange
			PositionValue left = new PositionValue(leftType, leftValue);
			PositionValue right = new PositionValue(rightType, rightValue);

			// Act
			bool result = left != right;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(100)]
		public void Implicit_Cast_From_Int32_Is_Absolute(int value)
		{
			// Arrange
			PositionValue input = value;

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase(PositionType.Absolute, 0, "0")]
		[TestCase(PositionType.Relative, 0, "~")]
		[TestCase(PositionType.Local, 0, "^")]
		[TestCase(PositionType.Absolute, 15, "15")]
		[TestCase(PositionType.Relative, 15, "~15")]
		[TestCase(PositionType.Local, 15, "^15")]
		[TestCase(PositionType.Absolute, -15, "-15")]
		[TestCase(PositionType.Relative, -15, "~-15")]
		[TestCase(PositionType.Local, -15, "^-15")]
		public void ToString_Includes_Correct_Prefix(PositionType type, int value, string expected)
		{
			// Arrange
			PositionValue input = new PositionValue(type, value);

			// Act
			string result = input.ToString();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
