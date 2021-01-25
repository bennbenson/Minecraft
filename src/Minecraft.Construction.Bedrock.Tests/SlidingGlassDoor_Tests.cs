using System.Collections.Generic;
using System.Linq;
using Minecraft.Model;
using NUnit.Framework;

/*
Inputs: (X:604, Z:125), (X:601, Z:125), 68...

	1: Impulse:    /fill 603 68 125 602 70 125 air
	2: Impulse:    /fill 604 68 125 601 70 125 stained_glass_pane 7 replace stained_glass_pane
	3: Chain:      /fill 604 68 125 601 70 125 stained_glass_pane 0 replace stained_glass_pane 7
	4: Chain:      /fill 603 68 125 602 70 125 stained_glass_pane

Inputs: (X:601, Z:125), (X:604, Z:125), 68...

	1: Impulse:    /fill 602 68 125 603 70 125 air
	2: Impulse:    /fill 601 68 125 604 70 125 stained_glass_pane 7 replace stained_glass_pane
	3: Chain:      /fill 601 68 125 604 70 125 stained_glass_pane 0 replace stained_glass_pane 7
	4: Chain:      /fill 602 68 125 603 70 125 stained_glass_pane

Inputs: (X:599, Z:115), (X:599, Z:118), 68...

	1: Impulse:    /fill 599 68 117 599 70 116 air
	2: Impulse:    /fill 599 68 118 599 70 115 stained_glass_pane 7 replace stained_glass_pane
	3: Chain:      /fill 599 68 118 599 70 115 stained_glass_pane 0 replace stained_glass_pane 7
	4: Chain:      /fill 599 68 117 599 70 116 stained_glass_pane
*/

namespace Minecraft.Construction.Bedrock.Tests
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
				"/fill 604 68 125 601 70 125 stained_glass_pane 7 replace stained_glass_pane",
				"/fill 604 68 125 601 70 125 stained_glass_pane 0 replace stained_glass_pane 7",
				"/fill 603 68 125 602 70 125 stained_glass_pane"
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
				"/fill 604 68 125 601 70 125 stained_glass_pane 7 replace stained_glass_pane",
				"/fill 604 68 125 601 70 125 stained_glass_pane 0 replace stained_glass_pane 7",
				"/fill 603 68 125 602 70 125 stained_glass_pane"
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
				"/fill 599 68 115 599 70 118 stained_glass_pane 7 replace stained_glass_pane",
				"/fill 599 68 115 599 70 118 stained_glass_pane 0 replace stained_glass_pane 7",
				"/fill 599 68 116 599 70 117 stained_glass_pane"
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
				"/fill 599 68 118 599 70 115 stained_glass_pane 7 replace stained_glass_pane",
				"/fill 599 68 118 599 70 115 stained_glass_pane 0 replace stained_glass_pane 7",
				"/fill 599 68 117 599 70 116 stained_glass_pane"
			};

			// Act
			var blocks = SlidingGlassDoor.Calculate4Wide3High(left, right, y);
			var result = blocks.Select(r => r.Command).ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
