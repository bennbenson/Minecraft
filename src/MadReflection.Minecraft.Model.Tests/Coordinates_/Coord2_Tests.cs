using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Coord2_Tests
	{
		[TestCase(10, 21, 11)]
		public void WithX_Modifies_X(int x, int z, int x2)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord2 result = input.WithX(x2);

			// Assert
			Assert.That(result.X, Is.EqualTo(x2));
			Assert.That(result.Z, Is.EqualTo(z));
		}

		[TestCase(10, 21, 11)]
		public void WithZ_Modifies_Z(int x, int z, int z2)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord2 result = input.WithZ(z2);

			// Assert
			Assert.That(result.X, Is.EqualTo(x));
			Assert.That(result.Z, Is.EqualTo(z2));
		}

		[TestCase(10, 21, 11)]
		public void AtY_Creates_Coord3(int x, int z, int y)
		{
			// Arrange
			Coord2 input = new Coord2(x, z);

			// Act
			Coord3 result = input.AtY(y);

			// Assert
			Assert.That(result.Y, Is.EqualTo(y));
		}

		// Add
		// AddX
		// AddZ

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



	}
}
