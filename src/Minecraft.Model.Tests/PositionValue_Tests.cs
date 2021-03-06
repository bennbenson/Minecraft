﻿using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class PositionValue_Tests
	{
		[TestCase]
		public void Default_Is_Absolute_Zero()
		{
			// Arrange

			// Act
			PositionValue result = new PositionValue();

			// Assert
			Assert.That(result.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Value, Is.EqualTo(0));
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
		[TestCase("+100", PositionType.Absolute, 100)]
		[TestCase("~", PositionType.Relative, 0)]
		[TestCase("~0", PositionType.Relative, 0)]
		[TestCase("^", PositionType.Local, 0)]
		[TestCase("^0", PositionType.Local, 0)]
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
			PositionValue result = PositionValue.Parse(input);

			// Assert
			Assert.That(result.Type, Is.EqualTo(type));
			Assert.That(result.Value, Is.EqualTo(value));
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(FormatException))]
		[TestCase("x", typeof(FormatException))]
		public void Parse_Throws_On_Invalid_Inputs(string input, Type expectedException)
		{
			// Arrange

			// Act
			TestDelegate test = () => Position.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf(expectedException));
		}

		[TestCase("0", true, PositionType.Absolute, 0)]
		[TestCase("51", true, PositionType.Absolute, 51)]
		[TestCase("-16", true, PositionType.Absolute, -16)]
		[TestCase("+100", true, PositionType.Absolute, 100)]
		[TestCase("~", true, PositionType.Relative, 0)]
		[TestCase("^", true, PositionType.Local, 0)]
		[TestCase("~1", true, PositionType.Relative, 1)]
		[TestCase("~-1", true, PositionType.Relative, -1)]
		[TestCase("~+1", true, PositionType.Relative, 1)]
		[TestCase("^1", true, PositionType.Local, 1)]
		[TestCase("^-1", true, PositionType.Local, -1)]
		[TestCase("^+1", true, PositionType.Local, 1)]
		[TestCase(null, false, default(PositionType), default(int))]
		[TestCase("", false, default(PositionType), default(int))]
		[TestCase("x", false, default(PositionType), default(int))]
		public void TryParse_Succeeds(string input, bool expected, PositionType expectedType, int expectedValue)
		{
			// Arrange

			// Act
			bool result = PositionValue.TryParse(input, out PositionValue output);

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
			PositionValue input = PositionValue.Absolute(0);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase]
		public void Relative_Creates_Relative_Value()
		{
			// Arrange
			PositionValue input = PositionValue.Relative(0);

			// Act
			PositionType result = input.Type;

			// Assert
			Assert.That(result, Is.EqualTo(PositionType.Relative));
		}

		[TestCase]
		public void Local_Creates_Local_Value()
		{
			// Arrange
			PositionValue input = PositionValue.Local(0);

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
