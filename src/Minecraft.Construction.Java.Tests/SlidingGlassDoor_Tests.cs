using System.Collections.Generic;
using System.Linq;
using Minecraft.Model;
using NUnit.Framework;

namespace Minecraft.Construction.Java.Tests
{
	[TestFixture]
	public class SlidingGlassDoor_Tests
	{
		[TestCase]
		public void MyTestMethod1()
		{
			// Arrange
			var left = new Coord2(604, 125);
			var right = new Coord2(601, 125);
			var y = 68;

			var expected = new string[]
			{
				"/fill 603 68 125 602 70 125 air",
				"/fill 604 68 125 601 70 125 gray_stained_glass_pane replace white_stained_glass_pane",
				"/fill 604 68 125 601 70 125 white_stained_glass_pane replace gray_stained_glass_pane",
				"/fill 603 68 125 602 70 125 white_stained_glass_pane"
			};

			// Act
			var blocks = SlidingGlassDoor.Calculate4Wide3High(left, right, y);
			var result = blocks.Select(r => r.Command).ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void MyTestMethod2()
		{
			// Arrange
			var left = new Coord2(601, 125);
			var right = new Coord2(604, 125);
			var y = 68;

			var expected = new string[]
			{
				"/fill 603 68 125 602 70 125 air",
				"/fill 604 68 125 601 70 125 gray_stained_glass_pane replace white_stained_glass_pane",
				"/fill 604 68 125 601 70 125 white_stained_glass_pane replace gray_stained_glass_pane",
				"/fill 603 68 125 602 70 125 white_stained_glass_pane"
			};

			// Act
			var blocks = SlidingGlassDoor.Calculate4Wide3High(left, right, y);
			var result = blocks.Select(r => r.Command).ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void MyTestMethod3()
		{
			// Arrange
			var left = new Coord2(599, 115);
			var right = new Coord2(599, 118);
			var y = 68;

			var expected = new string[]
			{
				"/fill 599 68 116 599 70 117 air",
				"/fill 599 68 115 599 70 118 gray_stained_glass_pane replace white_stained_glass_pane",
				"/fill 599 68 115 599 70 118 white_stained_glass_pane replace gray_stained_glass_pane",
				"/fill 599 68 116 599 70 117 white_stained_glass_pane"
			};

			// Act
			var blocks = SlidingGlassDoor.Calculate4Wide3High(left, right, y);
			var result = blocks.Select(r => r.Command).ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void MyTestMethod4()
		{
			// Arrange
			var left = new Coord2(599, 118);
			var right = new Coord2(599, 115);
			var y = 68;

			var expected = new string[]
			{
				"/fill 599 68 117 599 70 116 air",
				"/fill 599 68 118 599 70 115 gray_stained_glass_pane replace white_stained_glass_pane",
				"/fill 599 68 118 599 70 115 white_stained_glass_pane replace gray_stained_glass_pane",
				"/fill 599 68 117 599 70 116 white_stained_glass_pane"
			};

			// Act
			var blocks = SlidingGlassDoor.Calculate4Wide3High(left, right, y);
			var result = blocks.Select(r => r.Command).ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
