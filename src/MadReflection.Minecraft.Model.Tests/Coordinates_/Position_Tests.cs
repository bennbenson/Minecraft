using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Position_Tests
	{
		[TestCase]
		public void Default_Is_Absolute_Origin()
		{
			// Arrange

			// Act
			Position result = default;

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
		public void Constructor_Int32_Validates_Argument_Ranges(int x, int y, int z)
		{
			// Arrange

			// Act
			TestDelegate test = () => new Position(x, y, z);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(0, -1, 0)]
		[TestCase(0, 256, 0)]
		public void Constructor_Absolete_Y_Validates_Argument_Ranges(int x, int y, int z)
		{
			// Arrange

			// Act
			TestDelegate test = () => new Position(PositionValue.Absolute(x), PositionValue.Absolute(y), PositionValue.Absolute(z));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(0, PositionType.Absolute, -1, PositionType.Relative, 0, PositionType.Absolute, null)]
		[TestCase(0, PositionType.Absolute, 256, PositionType.Relative, 0, PositionType.Absolute, null)]
		[TestCase(0, PositionType.Absolute, -1, PositionType.Absolute, 0, PositionType.Absolute, typeof(ArgumentOutOfRangeException))]
		[TestCase(0, PositionType.Absolute, 256, PositionType.Absolute, 0, PositionType.Absolute, typeof(ArgumentOutOfRangeException))]
		public void Constructor_PositionValue_Validates_ArgumentRanges(int x, PositionType xType, int y, PositionType yType, int z, PositionType zType, Type? expectedExceptionType)
		{
			// Arrange

			// Act
			TestDelegate test = () => new Position(new PositionValue(xType, x), new PositionValue(yType, y), new PositionValue(zType, z));

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
			TestDelegate test = () => new Position(new PositionValue(xType, 0), new PositionValue(yType, 0), new PositionValue(zType, 0));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}

		[TestCase]
		public void Constructor_Int32_Is_Absoluete()
		{
			// Arrange

			// Act
			Position result = new Position(0, 100, 0);

			// Assert
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, PositionType.Absolute, 0)]
		//[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, PositionType.Absolute, 0)]
		//[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Absolute, 0)]
		//[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Absolute, 0)]
		public void Get_Returns_Position(PositionType xType, int xValue, PositionType yType, int yValue, PositionType zType, int zValue)
		{
			// Arrange

			// Act
			Position.Get(new PositionValue(xType, xValue), new PositionValue(yType, yValue), new PositionValue(zType, zValue));

			// Assert
			//Assert.That();
			//Assert.That();
			//Assert.That();
		}

		[TestCase]
		public void Absolute_Sets_Type_To_Absoluete()
		{
			// Arrange

			// Act
			Position result = Position.Absolute(0, 0, 0);

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
			Position result = Position.Relative(0, 0, 0);

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
			Position result = Position.Local(0, 0, 0);

			// Assert
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Local));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Local));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Local));
		}

		[TestCase("1, 2, 4", PositionType.Absolute, 1, PositionType.Absolute, 2, PositionType.Absolute, 4)]
		[TestCase("~, ~, ~", PositionType.Relative, 0, PositionType.Relative, 0, PositionType.Relative, 0)]
		[TestCase("^, ^, ^", PositionType.Local, 0, PositionType.Local, 0, PositionType.Local, 0)]
		public void Parse_Succeeds_On_Valid_Input(string input, PositionType expectedXType, int expectedXValue, PositionType expectedYType, int expectedYValue, PositionType expectedZType, int expectedZValue)
		{
			// Arrange

			// Act
			var result = Position.Parse(input);

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
		[TestCase("~0,0,0", typeof(FormatException))]
		public void Parse_Fails_On_Invalid_Input(string input, Type expectedExceptionType)
		{
			// Arrange

			// Act
			TestDelegate test = () => Position.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf(expectedExceptionType));
		}

		[TestCase("1, 2, 4", PositionType.Absolute, 1, PositionType.Absolute, 2, PositionType.Absolute, 4)]
		public void TryParse_Succeeds_On_Valid_Input(string input, PositionType outputXType, int outputXValue, PositionType outputYType, int outputYValue, PositionType outputZType, int outputZValue)
		{
			// Arrange

			// Act
			bool result = Position.TryParse(input, out Position output);

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
			bool result = Position.TryParse(input, out _);

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
			var input1 = Position.Parse(position1);
			var input2 = Position.Parse(position2);

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
			var input1 = Position.Parse(position1);
			var input2 = Position.Parse(position2);

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
			Position result = input;

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
		public void Cast_To_Coord3_Preserves_Values(int x, int y, int z)
		{
			// Arrange
			Position input = new Position(x, y, z);

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
		public void Cast_To_Coord3_Throws_On_NonAbsolute(PositionType xType, int xValue, PositionType yType, int yValue, PositionType zType, int zValue)
		{
			// Arrange
			var input = new Position(new PositionValue(xType, xValue), new PositionValue(yType, yValue), new PositionValue(zType, zValue));

			// Act
			Func<Coord3> test = () => (Coord3)input;

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}

		[TestCase(0, 10, 0)]
		[TestCase(2938, 64, -7387)]
		public void Cast_From_VarCoord3_Preserves_Values(int x, int y, int z)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			var result = (Position)input;

			// Assert
			Assert.That(result.X.Value, Is.EqualTo(x));
			Assert.That(result.Y.Value, Is.EqualTo(y));
			Assert.That(result.Z.Value, Is.EqualTo(z));
			Assert.That(result.X.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Y.Type, Is.EqualTo(PositionType.Absolute));
			Assert.That(result.Z.Type, Is.EqualTo(PositionType.Absolute));
		}

		[TestCase(null, 0, 0)]
		[TestCase(0, null, 0)]
		[TestCase(0, 0, null)]
		public void Cast_From_VarCoord3_Throws_On_Unknown(int? x, int? y, int? z)
		{
			// Arrange
			var input = new VarCoord3(x, y, z);

			// Act
			Func<Position> test = () => (Position)input;

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}

		[TestCase(0, 10, 0)]
		[TestCase(2938, 64, -7387)]
		public void Cast_To_VarCoord3_Preserves_Values(int x, int y, int z)
		{
			// Arrange
			Position input = new Position(x, y, z);

			// Act
			VarCoord3 result = (VarCoord3)input;

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(PositionType.Relative, 0, PositionType.Absolute, 0, PositionType.Absolute, 0)]
		[TestCase(PositionType.Absolute, 0, PositionType.Relative, 0, PositionType.Absolute, 0)]
		[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Relative, 0)]
		public void Cast_To_VarCoord3_Throws_On_NonAbsolute(PositionType xType, int xValue, PositionType yType, int yValue, PositionType zType, int zValue)
		{
			// Arrange
			var input = new Position(new PositionValue(xType, xValue), new PositionValue(yType, yValue), new PositionValue(zType, zValue));

			// Act
			Func<VarCoord3> test = () => (VarCoord3)input;

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}

		[TestCase(PositionType.Absolute, 0, PositionType.Absolute, 0, PositionType.Absolute, 0, "0, 0, 0")]
		[TestCase(PositionType.Relative, 0, PositionType.Relative, 0, PositionType.Relative, 0, "~, ~, ~")]
		[TestCase(PositionType.Local, 0, PositionType.Local, 0, PositionType.Local, 0, "^, ^, ^")]
		[TestCase(PositionType.Absolute, 10, PositionType.Absolute, 39, PositionType.Absolute, -100, "10, 39, -100")]
		public void ToString_Constructs_Correct_String(PositionType xType, int xValue, PositionType yType, int yValue, PositionType zType, int zValue, string expected)
		{
			// Arrange
			Position temp = new Position(new PositionValue(xType, xValue), new PositionValue(yType, yValue), new PositionValue(zType, zValue));

			// Act
			string result = temp.ToString();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
