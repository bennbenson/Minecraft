using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Position_Tests
	{
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


		[TestCase(0, -1, 0)]
		[TestCase(0, 256, 0)]
		public void Constructor_Absolete_Validates_Argument_Ranges(int x, int y, int z)
		{
			// Arrange

			// Act
			TestDelegate test = () => new Position(PositionValue.Absolute(x), PositionValue.Absolute(y), PositionValue.Absolute(z));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(PositionType.Absolute, PositionType.Absolute, PositionType.Local)]
		[TestCase(PositionType.Absolute, PositionType.Relative, PositionType.Local)]
		[TestCase(PositionType.Relative, PositionType.Absolute, PositionType.Local)]
		[TestCase(PositionType.Relative, PositionType.Relative, PositionType.Local)]
		[TestCase(PositionType.Absolute, PositionType.Local, PositionType.Absolute)]
		[TestCase(PositionType.Absolute, PositionType.Local, PositionType.Relative)]
		[TestCase(PositionType.Relative, PositionType.Local, PositionType.Absolute)]
		[TestCase(PositionType.Relative, PositionType.Local, PositionType.Relative)]
		[TestCase(PositionType.Local, PositionType.Absolute, PositionType.Absolute)]
		[TestCase(PositionType.Local, PositionType.Absolute, PositionType.Relative)]
		[TestCase(PositionType.Local, PositionType.Relative, PositionType.Absolute)]
		[TestCase(PositionType.Local, PositionType.Relative, PositionType.Relative)]
		public void Constructor_Disallows_Mixed_Local_and_Non_Local(PositionType typeX, PositionType typeY, PositionType typeZ)
		{
			// Arrange

			// Act
			TestDelegate test = () => new Position(new PositionValue(typeX, 0), new PositionValue(typeY, 0), new PositionValue(typeZ, 0));

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}
	}
}
