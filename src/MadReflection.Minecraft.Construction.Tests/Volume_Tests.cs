using Minecraft.Model;
using NUnit.Framework;

namespace Minecraft.Construction.Tests
{
	[TestFixture]
	public class Volume_Tests
	{
		[TestCase(0, 10, 0, 0, 10, 0, 1)]
		[TestCase(0, 10, 0, 0, 11, 0, 2)]
		[TestCase(10, 10, 10, 20, 10, 20, 121)]
		[TestCase(-15, 10, 5, -1, 10, -2, 120)]
		public void Volume_Test_1(int x1, int y1, int z1, int x2, int y2, int z2, int expected)
		{
			// Arrange
			Coord3 coord1 = new Coord3(x1, y1, z1);
			Coord3 coord2 = new Coord3(x2, y2, z2);

			// Act
			int result = Volume.CountBlocks(coord1, coord2);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
