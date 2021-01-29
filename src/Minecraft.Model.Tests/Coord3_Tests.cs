using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Coord3_Tests
	{
		[TestCase]
		public void Default_Is_Origin()
		{
			// Arrange

			// Act
			Coord3 result = default;

			// Assert
			Assert.That(result.X, Is.EqualTo(0));
			Assert.That(result.Y, Is.EqualTo(0));
			Assert.That(result.Z, Is.EqualTo(0));
		}

		[TestCase(10, 64, -13)]
		public void Constructor_Sets_Properties(int x, int y, int z)
		{
			// Arrange

			// Act
			Coord3 result = new Coord3(x, y, z);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(0, -1, 0)]
		[TestCase(0, 256, 0)]
		public void Constructor_Validates_Argument_Ranges(int x, int y, int z)
		{
			// Arrange

			// Act
			TestDelegate test = () => new Coord3(x, y, z);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(10, 64, 21, 11)]
		public void WithX_Modifies_X(int x, int y, int z, int newX)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord3 result = input.WithX(newX);

			// Assert
			Assert.That(result.X, Is.EqualTo(newX));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 80)]
		public void WithY_Modifies_Y(int x, int y, int z, int newY)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord3 result = input.WithY(newY);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(newY));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 25)]
		public void WithZ_Modifies_Z(int x, int y, int z, int newZ)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord3 result = input.WithZ(newZ);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(newZ));
		}

		[TestCase(10, 64, 21, 3, 4, 5)]
		public void Add_Modifies_Properties(int x, int y, int z, int dx, int dy, int dz)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord3 result = input.Add(dx, dy, dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(x + dx));
			Assert.That(result.Y, Is.EqualTo(y + dy));
			Assert.That(result.Z, Is.EqualTo(z + dz));
		}

		[TestCase(10, 64, 21, 8)]
		public void AddX_Modifies_X(int x, int y, int z, int dx)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord3 result = input.AddX(dx);

			// Assert
			Assert.That(result.X, Is.EqualTo(x + dx));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 36)]
		public void AddY_Modifies_Y(int x, int y, int z, int dy)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord3 result = input.AddY(dy);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y + dy));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 64, 21, 7)]
		public void AddZ_Modifies_Z(int x, int y, int z, int dz)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord3 result = input.AddZ(dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z + dz));
		}

		[TestCase("(3,1,9)", 3, 1, 9)]
		[TestCase("(3, 1, 9)", 3, 1, 9)]
		public void Parse_Succeeds_On_Valid_Input(string input, int expectedX, int expectedY, int expectedZ)
		{
			// Arrange

			// Act
			Coord3 result = Coord3.Parse(input);

			// Assert
			Assert.That(result.X, Is.EqualTo(expectedX));
			Assert.That(result.Y, Is.EqualTo(expectedY));
			Assert.That(result.Z, Is.EqualTo(expectedZ));
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(FormatException))]
		[TestCase("3,1,9", typeof(FormatException))]
		public void Parse_Throws_On_Invalid_Input(string input, Type expectedException)
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
		public void At_Returns_Coord3(int x, int y, int z)
		{
			// Arrange

			// Act
			Coord3 result = Coord3.At(x, y, z);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(3, 100, -3, -3, 105, 3, -3, 100, -3)]
		public void Min_Returns_Minimum_Corner(int ax, int ay, int az, int bx, int by, int bz, int ex, int ey, int ez)
		{
			// Arrange
			Coord3 a = new Coord3(ax, ay, az);
			Coord3 b = new Coord3(bx, by, bz);

			Coord3 expected = new Coord3(ex, ey, ez);

			// Act
			Coord3 result = Coord3.Min(a, b);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(3, 100, -3, -3, 105, 3, 3, 105, 3)]
		public void Max_Returns_Miximum_Corner(int ax, int ay, int az, int bx, int by, int bz, int ex, int ey, int ez)
		{
			// Arrange
			Coord3 a = new Coord3(ax, ay, az);
			Coord3 b = new Coord3(bx, by, bz);

			Coord3 expected = new Coord3(ex, ey, ez);

			// Act
			Coord3 result = Coord3.Max(a, b);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(10, 64, 14, 10, 64, 14, true)]
		[TestCase(10, 64, 14, 10, 64, 13, false)]
		[TestCase(10, 64, 14, 9, 64, 14, false)]
		public void Operator_Equality_Compares_Equal(int x1, int y1, int z1, int x2, int y2, int z2, bool expected)
		{
			// Arrange
			Coord3 input1 = new Coord3(x1, y1, z1);
			Coord3 input2 = new Coord3(x2, y2, z2);

			// Act
			bool result = input1 == input2;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(10, 64, 14, 10, 64, 14, false)]
		[TestCase(10, 64, 14, 10, 64, 13, true)]
		[TestCase(10, 64, 14, 9, 64, 14, true)]
		public void Operator_Inequality_Compares_Not_Equal(int x1, int y1, int z1, int x2, int y2, int z2, bool expected)
		{
			// Arrange
			Coord3 input1 = new Coord3(x1, y1, z1);
			Coord3 input2 = new Coord3(x2, y2, z2);

			// Act
			bool result = input1 != input2;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(78, 64, -45, 1, 5, 1)]
		public void Operator_Addition(int x, int y, int z, int dx, int dy, int dz)
		{
			// Arrange
			Coord3 input1 = new Coord3(x, y, z);
			Coord3 input2 = new Coord3(dx, dy, dz);

			// Act
			var result = input1 + (dx, dy, dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(input1.X + input2.X));
			Assert.That(result.Y, Is.EqualTo(input1.Y + input2.Y));
			Assert.That(result.Z, Is.EqualTo(input1.Z + input2.Z));
		}

		[TestCase(78, 64, -45, 1, 5, 1)]
		public void Operator_Subtraction(int x, int y, int z, int dx, int dy, int dz)
		{
			// Arrange
			Coord3 input1 = new Coord3(x, y, z);
			Coord3 input2 = new Coord3(dx, dy, dz);

			// Act
			var result = input1 - (dx, dy, dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(input1.X - input2.X));
			Assert.That(result.Y, Is.EqualTo(input1.Y - input2.Y));
			Assert.That(result.Z, Is.EqualTo(input1.Z - input2.Z));
		}
	}
}
