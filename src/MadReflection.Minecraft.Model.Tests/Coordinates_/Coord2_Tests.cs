using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Coord2_Tests
	{
		[TestCase]
		public void Default_Is_Origin()
		{
			// Arrange

			// Act
			Coord2 result = default;

			// Assert
			Assert.That(result.X, Is.EqualTo(0));
			Assert.That(result.Z, Is.EqualTo(0));
		}

		[TestCase(10, -13)]
		public void Constructor_Sets_Properties(int x, int z)
		{
			// Arrange

			// Act
			Coord2 result = new Coord2(x, z);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 21, 11)]
		public void WithX_Modifies_X(int x, int z, int newX)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord2 result = input.WithX(newX);

			// Assert
			Assert.That(result.X, Is.EqualTo(newX));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 21, 11)]
		public void WithZ_Modifies_Z(int x, int z, int dz)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord2 result = input.WithZ(dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Z, Is.EqualTo(dz));
		}

		[TestCase(10, 21, 11)]
		public void AtY_Creates_Coord3(int x, int z, int y)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord3 result = input.AtY(y);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Y, Is.EqualTo(y));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 21, -3, 22)]
		public void Add_Modifies_Properties(int x, int z, int dx, int dz)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord2 result = input.Add(dx, dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(x + dx));
			Assert.That(result.Z, Is.EqualTo(z + dz));
		}

		[TestCase(10, 21, -3)]
		public void AddX_Modifies_X(int x, int z, int dx)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord2 result = input.AddX(dx);

			// Assert
			Assert.That(result.X, Is.EqualTo(x + dx));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 21, 22)]
		public void AddZ_Modifies_Z(int x, int z, int dz)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord2 result = input.AddZ(dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Z, Is.EqualTo(z + dz));
		}

		[TestCase("(3, 9)", 3, 9)]
		[TestCase("(3,9)", 3, 9)]
		public void Parse_Succeeds_On_Valid_Input(string input, int expectedX, int expectedZ)
		{
			// Arrange
			Coord2 expected = new Coord2(expectedX, expectedZ);

			// Act
			Coord2 result = Coord2.Parse(input);

			// Assert
			Assert.That(result.X, Is.EqualTo(expectedX));
			Assert.That(result.Z, Is.EqualTo(expectedZ));
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(FormatException))]
		[TestCase("3,9", typeof(FormatException))]
		public void Parse_Throws_On_Invalid_Input(string input, Type expectedException)
		{
			// Arrange

			// Act
			TestDelegate test = () => Coord2.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf(expectedException));
		}

		[TestCase("(10,11)", true, 10, 11)]
		public void TryParse_Succeeds(string input, bool expectedResult, int expectedX, int expectedZ)
		{
			// Arrange

			// Act
			bool result = Coord2.TryParse(input, out Coord2 output);

			// Assert
			Assert.That(result, Is.EqualTo(expectedResult));
			Assert.That(output.X, Is.EqualTo(expectedX));
			Assert.That(output.Z, Is.EqualTo(expectedZ));
		}

		[TestCase(-11, 16)]
		public void At_Returns_Coord2(int x, int z)
		{
			// Arrange

			// Act
			Coord2 result = Coord2.At(x, z);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 14, 10, 14, true)]
		[TestCase(10, 14, 10, 13, false)]
		[TestCase(10, 14, 9, 14, false)]
		public void Operator_Equality_Compares_Equal(int x1, int z1, int x2, int z2, bool expected)
		{
			// Arrange
			Coord2 input1 = new Coord2(x1, z1);
			Coord2 input2 = new Coord2(x2, z2);

			// Act
			bool result = input1 == input2;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(10, 14, 10, 14, false)]
		[TestCase(10, 14, 10, 13, true)]
		[TestCase(10, 14, 9, 14, true)]
		public void Operator_Inequality_Compares_Not_Equal(int x1, int z1, int x2, int z2, bool expected)
		{
			// Arrange
			Coord2 input1 = new Coord2(x1, z1);
			Coord2 input2 = new Coord2(x2, z2);

			// Act
			bool result = input1 != input2;

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(78, -45, 1, 1)]
		public void Operator_Addition(int x, int z, int dx, int dz)
		{
			// Arrange
			Coord2 input1 = new Coord2(x, z);
			Coord2 input2 = new Coord2(dx, dz);

			// Act
			var result = input1 + (dx, dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(input1.X + input2.X));
			Assert.That(result.Z, Is.EqualTo(input1.Z + input2.Z));
		}

		[TestCase(78, -45, 1, 1)]
		public void Operator_Subtraction(int x, int z, int dx, int dz)
		{
			// Arrange
			Coord2 input1 = new Coord2(x, z);
			Coord2 input2 = new Coord2(dx, dz);

			// Act
			var result = input1 - (dx, dz);

			// Assert
			Assert.That(result.X, Is.EqualTo(input1.X - input2.X));
			Assert.That(result.Z, Is.EqualTo(input1.Z - input2.Z));
		}

		[TestCase(10, 64, 21)]
		public void Explicit_Cast_From_Coord3(int x, int y, int z)
		{
			// Arrange
			Coord3 input = new Coord3(x, y, z);

			// Act
			Coord2 result = (Coord2)input;

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Z, Is.EqualTo(z));
		}
	}
}
