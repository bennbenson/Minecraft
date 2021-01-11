using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class VarCoord3_Tests
	{
		[TestCase]
		public void Default_Is_Entirely_Unknown()
		{
			// Arrange

			// Act
			VarCoord3 result = default;

			// Assert
			Assert.That(result.X, Is.EqualTo(null));
			Assert.That(result.Y, Is.EqualTo(null));
			Assert.That(result.Z, Is.EqualTo(null));
		}

		[TestCase(10, 64, -13)]
		[TestCase(null, 64, -13)]
		[TestCase(10, null, -13)]
		[TestCase(10, 64, null)]
		public void Constructor_Sets_Properties(int? x, int? y, int? z)
		{
			// Arrange

			// Act
			VarCoord3 result = new VarCoord3(x, y, z);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(0, -1, 0)]
		[TestCase(0, 256, 0)]
		public void Constructor_Validates_Ranges(int? x, int? y, int? z)
		{
			// Arrange

			// Act
			TestDelegate test = () => new VarCoord3(x, y, z);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(10, 64, 21, 11)]
		public void WithX_Modifies_X(int? x, int? y, int? z, int? newX)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			VarCoord3 result = input.WithX(newX);

			// Assert
			Assert.That(result.X, Is.EqualTo(newX));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 80)]
		public void WithY_Modifies_Y(int? x, int? y, int? z, int? newY)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			VarCoord3 result = input.WithY(newY);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(newY));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 25)]
		public void WithZ_Modifies_Z(int? x, int? y, int? z, int? newZ)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			VarCoord3 result = input.WithZ(newZ);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(newZ));
		}

#if false
		[TestCase(10, 64, 21, 3, 4, 5)]
		public void Add_Modifies_Properties(int? x, int? y, int? z, int dx, int dy, int dz)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			VarCoord3 result = input.Add(dx, dy, dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(x + dx));
			Assert.That(result.Y, Is.EqualTo(y + dy));
			Assert.That(result.Z, Is.EqualTo(z + dz));
		}

		[TestCase(10, 64, 21, 8)]
		public void AddX_Modifies_X(int x, int y, int z, int dx)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			VarCoord3 result = input.AddX(dx);

			// Assert
			Assert.That(result.X, Is.EqualTo(x + dx));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 36)]
		public void AddY_Modifies_Y(int? x, int? y, int? z, int dy)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			VarCoord3 result = input.AddY(dy);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y + dy));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 7)]
		public void AddZ_Modifies_Z(int x, int y, int z, int dz)
		{
			// Arrange
			VarCoord3 input = new VarCoord3(x, y, z);

			// Act
			VarCoord3 result = input.AddZ(dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z + dz));
		}
#endif

		[TestCase("(3,1,9)", 3, 1, 9)]
		[TestCase("(3, 1, 9)", 3, 1, 9)]
		public void Parse_Succeeds_On_Valid_Input(string input, int? expectedX, int? expectedY, int? expectedZ)
		{
			// Arrange

			// Act
			VarCoord3 result = VarCoord3.Parse(input);

			// Assert
			Assert.That(result.X, Is.EqualTo(expectedX));
			Assert.That(result.Y, Is.EqualTo(expectedY));
			Assert.That(result.Z, Is.EqualTo(expectedZ));
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(FormatException))]
		[TestCase("3,1,9", typeof(FormatException))]
		public void Parse_Fails_On_Invalid_Input(string input, Type expectedException)
		{
			// Arrange

			// Act
			TestDelegate test = () => Coord3.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf(expectedException));
		}

		[TestCase("(10,15,11)", true, 10, 15, 11)]
		public void TryParse_Succeeds(string input, bool expectedResult, int expectedX, int expectedY, int expectedZ)
		{
			// Arrange

			// Act
			bool result = Coord3.TryParse(input, out Coord3 output);

			// Assert
			Assert.That(result, Is.EqualTo(expectedResult));
			Assert.That(output.X, Is.EqualTo(expectedX));
			Assert.That(output.Y, Is.EqualTo(expectedY));
			Assert.That(output.Z, Is.EqualTo(expectedZ));
		}

		[TestCase(-11, 80, 16)]
		public void At_Returns_VarCoord3(int? x, int? y, int? z)
		{
			// Arrange

			// Act
			VarCoord3 result = VarCoord3.At(x, y, z);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		// Operator_Equality
		// Operator_Inequality

		// Implicit_Cast_From_Coord3
		// Explicit_Cast_To_Coord3

		// ToString
	}
}
