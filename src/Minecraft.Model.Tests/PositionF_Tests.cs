using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class PositionF_Tests
	{
		[TestCase]
		public void Default_Is_Absolute_Origin()
		{
			// Arrange

			// Act
			PositionF result = default;

			// Assert
			Assert.That(result.X.Value, Is.EqualTo(0));
			Assert.That(result.Y.Value, Is.EqualTo(0));
			Assert.That(result.Z.Value, Is.EqualTo(0));
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase(0, -1, 0)]
		[TestCase(0, 256, 0)]
		public void Constructor_Single_Validates_Argument_Ranges(float x, float y, float z)
		{
			// Arrange

			// Act
			TestDelegate test = () => new PositionF(x, y, z);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(0, -1, 0)]
		[TestCase(0, 256, 0)]
		public void Constructor_Absolete_Y_Validates_Argument_Ranges(float x, float y, float z)
		{
			// Arrange

			// Act
			TestDelegate test = () => new PositionF(PositionFValue.Absolute(x), PositionFValue.Absolute(y), PositionFValue.Absolute(z));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(0, PositionType.Absolute, -1, PositionType.Relative, 0, PositionType.Absolute, null)]
		[TestCase(0, PositionType.Absolute, 256, PositionType.Relative, 0, PositionType.Absolute, null)]
		[TestCase(0, PositionType.Absolute, -1, PositionType.Absolute, 0, PositionType.Absolute, typeof(ArgumentOutOfRangeException))]
		[TestCase(0, PositionType.Absolute, 256, PositionType.Absolute, 0, PositionType.Absolute, typeof(ArgumentOutOfRangeException))]
		public void Constructor_PositionValue_Validates_ArgumentRanges(float x, PositionType xType, float y, PositionType yType, float z, PositionType zType, Type? expectedExceptionType)
		{
			// Arrange

			// Act
			TestDelegate test = () => new PositionF(new PositionFValue(xType, x), new PositionFValue(yType, y), new PositionFValue(zType, z));

			// Assert
			if (expectedExceptionType is not null)
			{
				Assert.That(test, Throws.TypeOf(expectedExceptionType));
			}
			else
			{
				Assert.That(test, Throws.Nothing);
			}
		}

		[TestCase(PositionType.Absolute, PositionType.Absolute, PositionType.Local)]
		[TestCase(PositionType.Absolute, PositionType.Relative, PositionType.Local)]
		[TestCase(PositionType.Absolute, PositionType.Local, PositionType.Absolute)]
		[TestCase(PositionType.Absolute, PositionType.Local, PositionType.Relative)]
		[TestCase(PositionType.Relative, PositionType.Absolute, PositionType.Local)]
		[TestCase(PositionType.Relative, PositionType.Relative, PositionType.Local)]
		[TestCase(PositionType.Relative, PositionType.Local, PositionType.Absolute)]
		[TestCase(PositionType.Relative, PositionType.Local, PositionType.Relative)]
		[TestCase(PositionType.Local, PositionType.Absolute, PositionType.Absolute)]
		[TestCase(PositionType.Local, PositionType.Absolute, PositionType.Relative)]
		[TestCase(PositionType.Local, PositionType.Relative, PositionType.Absolute)]
		[TestCase(PositionType.Local, PositionType.Relative, PositionType.Relative)]
		public void Constructor_Disallows_Mixed_Local_and_Non_Local(PositionType xType, PositionType yType, PositionType zType)
		{
			// Arrange

			// Act
			TestDelegate test = () => new PositionF(new PositionFValue(xType, 0), new PositionFValue(yType, 0), new PositionFValue(zType, 0));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}

		[TestCase]
		public void Constructor_Single_Is_Absoluete()
		{
			// Arrange

			// Act
			PositionF result = new PositionF(0, 100, 0);

			// Assert
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Absolute));
		}

		// WithX
		// WithY
		// WithZ

		[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, PositionType.Absolute, 0)]
		//[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, PositionType.Absolute, 0)]
		//[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Absolute, 0)]
		//[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Absolute, 0)]
		public void Get_Returns_Position(PositionType xType, float xValue, PositionType yType, float yValue, PositionType zType, float zValue)
		{
			// Arrange

			// Act
			PositionF result = PositionF.Get(new PositionFValue(xType, xValue), new PositionFValue(yType, yValue), new PositionFValue(zType, zValue));

			// Assert
			Assert.That(result.X.Type, Is.EqualTo(xType));
			Assert.That(result.Y.Type, Is.EqualTo(yType));
			Assert.That(result.Z.Type, Is.EqualTo(zType));
			Assert.That(result.X.Value, Is.EqualTo(xValue));
			Assert.That(result.Y.Value, Is.EqualTo(yValue));
			Assert.That(result.Z.Value, Is.EqualTo(zValue));
		}

		[TestCase]
		public void Absolute_Sets_Type_To_Absoluete()
		{
			// Arrange

			// Act
			PositionF result = PositionF.Absolute(0, 0, 0);

			// Assert
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase]
		public void Relative_Sets_Type_To_Relative()
		{
			// Arrange

			// Act
			PositionF result = PositionF.Relative(0, 0, 0);

			// Assert
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Relative));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Relative));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Relative));
		}

		[TestCase]
		public void Local_Sets_Type_To_Local()
		{
			// Arrange

			// Act
			PositionF result = PositionF.Local(0, 0, 0);

			// Assert
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Local));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Local));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Local));
		}

		[TestCase("1, 2, 4", PositionType.Absolute, 1, PositionType.Absolute, 2, PositionType.Absolute, 4)]
		[TestCase("~, ~, ~", PositionType.Relative, 0, PositionType.Relative, 0, PositionType.Relative, 0)]
		[TestCase("^, ^, ^", PositionType.Local, 0, PositionType.Local, 0, PositionType.Local, 0)]
		[TestCase("~0.0,~0.0,~0.0", PositionType.Relative, 0.0f, PositionType.Relative, 0.0f, PositionType.Relative, 0.0f)]
		[TestCase("^0.0,^0.0,^0.0", PositionType.Local, 0.0f, PositionType.Local, 0.0f, PositionType.Local, 0.0f)]
		public void Parse_Succeeds_On_Valid_Input(string input, PositionType expectedXType, float expectedXValue, PositionType expectedYType, float expectedYValue, PositionType expectedZType, float expectedZValue)
		{
			// Arrange

			// Act
			PositionF result = PositionF.Parse(input);

			// Assert
			Assert.That(result.X.Value, Is.EqualTo(expectedXValue));
			Assert.That(result.Y.Value, Is.EqualTo(expectedYValue));
			Assert.That(result.Z.Value, Is.EqualTo(expectedZValue));
			Assert.That(result.X.Type, Is.EqualTo(expectedXType));
			Assert.That(result.Y.Type, Is.EqualTo(expectedYType));
			Assert.That(result.Z.Type, Is.EqualTo(expectedZType));
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(FormatException))]
		[TestCase("1, y?, 4", typeof(FormatException))]
		public void Parse_Fails_On_Invalid_Input(string input, Type expectedExceptionType)
		{
			// Arrange

			// Act
			TestDelegate test = () => PositionF.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf(expectedExceptionType));
		}

		[TestCase("1, 2, 4", PositionType.Absolute, 1, PositionType.Absolute, 2, PositionType.Absolute, 4)]
		public void TryParse_Succeeds_On_Valid_Input(string input, PositionType outputXType, float outputXValue, PositionType outputYType, float outputYValue, PositionType outputZType, float outputZValue)
		{
			// Arrange

			// Act
			bool result = PositionF.TryParse(input, out PositionF output);

			// Assert
			Assert.That(result, Is.True);
			Assert.That(output.X.Value, Is.EqualTo(outputXValue));
			Assert.That(output.Y.Value, Is.EqualTo(outputYValue));
			Assert.That(output.Z.Value, Is.EqualTo(outputZValue));
			Assert.That(output.X.Type, Is.EqualTo(outputXType));
			Assert.That(output.Y.Type, Is.EqualTo(outputYType));
			Assert.That(output.Z.Type, Is.EqualTo(outputZType));
		}

		[TestCase(null)]
		[TestCase("")]
		[TestCase("1, y?, 4")]
		public void TryParse_Fails_On_Invalid_Input(string input)
		{
			// Arrange

			// Act
			bool result = PositionF.TryParse(input, out _);

			// Assert
			Assert.That(result, Is.False);
		}

		[TestCase("0, 100, 0", "0, 100, 0", true)]
		[TestCase("0, 100, 0", "0, 50, 0", false)]
		[TestCase("0, 100, 0", "1, 100, 0", false)]
		[TestCase("0, 100, 0", "0, 100, -1", false)]
		public void OpEquality_Compares_Equal(string position1, string position2, bool expected)
		{
			// Arrange
			PositionF input1 = PositionF.Parse(position1);
			PositionF input2 = PositionF.Parse(position2);

			// Act
			bool result = input1 == input2;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("0, 100, 0", "0, 100, 0", false)]
		[TestCase("0, 100, 0", "0, 50, 0", true)]
		[TestCase("0, 100, 0", "1, 100, 0", true)]
		[TestCase("0, 100, 0", "0, 100, -1", true)]
		public void OpInequality_Compares_Not_Equal(string position1, string position2, bool expected)
		{
			// Arrange
			PositionF input1 = PositionF.Parse(position1);
			PositionF input2 = PositionF.Parse(position2);

			// Act
			bool result = input1 != input2;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 10, 0)]
		[TestCase(2938, 64, -7387)]
		public void Cast_From_Coord3_Preserves_Values(int x, int y, int z)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			PositionF result = input;

			// Assert
			Assert.That(result.X.Value, Is.EqualTo(x));
			Assert.That(result.Y.Value, Is.EqualTo(y));
			Assert.That(result.Z.Value, Is.EqualTo(z));
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase(0, 10, 0)]
		[TestCase(2938, 64, -7387)]
		public void Cast_To_Coord3_Preserves_Values(float x, float y, float z)
		{
			// Arrange
			PositionF input = new PositionF(x, y, z);

			// Act
			Coord3 result = (Coord3)input;

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(PositionType.Relative, 0, PositionType.Absolute, 0, PositionType.Absolute, 0)]
		[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, PositionType.Absolute, 0)]
		[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Relative, 0)]
		public void Cast_To_Coord3_Throws_On_NonAbsolute(PositionType xType, float xValue, PositionType yType, float yValue, PositionType zType, float zValue)
		{
			// Arrange
			PositionF input = new PositionF(new PositionFValue(xType, xValue), new PositionFValue(yType, yValue), new PositionFValue(zType, zValue));

			// Act
			Func<Coord3> test = () => (Coord3)input;

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}

		[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Absolute, 0, "0, 0, 0")]
		[TestCase(PositionType.Relative, 0, PositionType.Relative, 0, PositionType.Relative, 0, "~, ~, ~")]
		[TestCase(PositionType.Local, 0, PositionType.Local, 0, PositionType.Local, 0, "^, ^, ^")]
		[TestCase(PositionType.Absolute, 10, PositionType.Absolute, 39, PositionType.Absolute, -100, "10, 39, -100")]
		public void ToString_Constructs_Correct_String(PositionType xType, float xValue, PositionType yType, float yValue, PositionType zType, float zValue, string expected)
		{
			// Arrange
			PositionF temp = new PositionF(new PositionFValue(xType, xValue), new PositionFValue(yType, yValue), new PositionFValue(zType, zValue));

			// Act
			string result = temp.ToString();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
