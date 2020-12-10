using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class Coord3_Tests
	{
		[TestCase]
		public void Constructor_Throws_On_Negative_Y()
		{
			// Arrange

			// Act
			TestDelegate test = () => new Coord3(0, -1, 0);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}
	}
}
