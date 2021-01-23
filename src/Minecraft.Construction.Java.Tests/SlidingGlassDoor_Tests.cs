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
			Coord2 left = new Coord2(604, 125);
			Coord2 right = new Coord2(601, 125);
			int y = 68;

			// Act
			var result = SlidingGlassDoor.Calculate4Wide3High(left, right, y).ToList();

			// Assert
			List<string> commands = result.Select(r => r.Command.GetCommandText()).ToList();

			Assert.That(commands.Count, Is.EqualTo(4));
			Assert.That(commands[0], Is.EqualTo("/fill 603 68 125 602 70 125 air"));
			Assert.That(commands[1], Is.EqualTo("/fill 604 68 125 601 70 125 gray_stained_glass_pane replace white_stained_glass_pane"));
			Assert.That(commands[2], Is.EqualTo("/fill 604 68 125 601 70 125 white_stained_glass_pane replace gray_stained_glass_pane"));
			Assert.That(commands[3], Is.EqualTo("/fill 603 68 125 602 70 125 white_stained_glass_pane"));
		}

		[TestCase]
		public void MyTestMethod2()
		{
			// Arrange
			Coord2 left = new Coord2(601, 125);
			Coord2 right = new Coord2(604, 125);
			int y = 68;

			// Act
			var result = SlidingGlassDoor.Calculate4Wide3High(left, right, y).ToList();

			// Assert
			List<string> commands = result.Select(r => r.Command.GetCommandText()).ToList();

			Assert.That(commands.Count, Is.EqualTo(4));
			Assert.That(commands[0], Is.EqualTo("/fill 603 68 125 602 70 125 air"));
			Assert.That(commands[1], Is.EqualTo("/fill 604 68 125 601 70 125 gray_stained_glass_pane replace white_stained_glass_pane"));
			Assert.That(commands[2], Is.EqualTo("/fill 604 68 125 601 70 125 white_stained_glass_pane replace gray_stained_glass_pane"));
			Assert.That(commands[3], Is.EqualTo("/fill 603 68 125 602 70 125 white_stained_glass_pane"));
		}

		[TestCase]
		public void MyTestMethod3()
		{
			// Arrange
			Coord2 left = new Coord2(599, 115);
			Coord2 right = new Coord2(599, 118);
			int y = 68;

			// Act
			var result = SlidingGlassDoor.Calculate4Wide3High(left, right, y).ToList();

			// Assert
			List<string> commands = result.Select(r => r.Command.GetCommandText()).ToList();

			Assert.That(commands.Count, Is.EqualTo(4));
			Assert.That(commands[0], Is.EqualTo("/fill 599 68 116 599 70 117 air"));
			Assert.That(commands[1], Is.EqualTo("/fill 599 68 115 599 70 118 gray_stained_glass_pane replace white_stained_glass_pane"));
			Assert.That(commands[2], Is.EqualTo("/fill 599 68 115 599 70 118 white_stained_glass_pane replace gray_stained_glass_pane"));
			Assert.That(commands[3], Is.EqualTo("/fill 599 68 116 599 70 117 white_stained_glass_pane"));
		}

		[TestCase]
		public void MyTestMethod4()
		{
			// Arrange
			Coord2 left = new Coord2(599, 118);
			Coord2 right = new Coord2(599, 115);
			int y = 68;

			// Act
			var result = SlidingGlassDoor.Calculate4Wide3High(left, right, y).ToList();

			// Assert
			List<string> commands = result.Select(r => r.Command.GetCommandText()).ToList();

			Assert.That(commands.Count, Is.EqualTo(4));
			Assert.That(commands[0], Is.EqualTo("/fill 599 68 117 599 70 116 air"));
			Assert.That(commands[1], Is.EqualTo("/fill 599 68 118 599 70 115 gray_stained_glass_pane replace white_stained_glass_pane"));
			Assert.That(commands[2], Is.EqualTo("/fill 599 68 118 599 70 115 white_stained_glass_pane replace gray_stained_glass_pane"));
			Assert.That(commands[3], Is.EqualTo("/fill 599 68 117 599 70 116 white_stained_glass_pane"));
		}
	}
}
