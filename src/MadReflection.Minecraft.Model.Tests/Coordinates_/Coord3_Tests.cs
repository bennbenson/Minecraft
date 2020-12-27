using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Coord3_Tests
	{
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
	}
}
