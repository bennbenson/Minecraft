using System.Linq;
using NUnit.Framework;

namespace Minecraft.Construction.Java.Tests
{
	[TestFixture]
	public class DuckBlind_Tests
	{
		[TestCase]
		public void Defaults()
		{
			// Arrange
			var expected = new string[]
			{
				"/setblock 0 99 0 chiseled_stone_bricks",
				"/fill -1 99 -1 1 99 -2 red_concrete",
				"/fill 1 99 -1 2 99 1 lime_concrete",
				"/fill 1 99 1 -1 99 2 blue_concrete",
				"/fill -1 99 1 -2 99 -1 orange_concrete",
				"/setblock -2 99 -2 black_concrete",
				"/setblock -1 99 -1 black_concrete",
				"/setblock 1 99 -1 black_concrete",
				"/setblock 2 99 -2 black_concrete",
				"/setblock 2 99 2 black_concrete",
				"/setblock 1 99 1 black_concrete",
				"/setblock -1 99 1 black_concrete",
				"/setblock -2 99 2 black_concrete",
				"/setblock 0 98 0 command_block[facing=up]",
				"/fill 0 98 1 1 98 1 smooth_quartz_stairs[facing=north,half=top]",
				"/fill 1 98 0 1 98 -1 smooth_quartz_stairs[facing=west,half=top]",
				"/fill 1 98 -1 -1 98 -1 smooth_quartz_stairs[facing=south,half=top]",
				"/fill -1 98 0 -1 98 1 smooth_quartz_stairs[facing=east,half=top]",
				"/fill -1 98 2 2 98 2 smooth_quartz_stairs[facing=south,half=top]",
				"/fill 2 98 1 2 98 -2 smooth_quartz_stairs[facing=east,half=top]",
				"/fill 1 98 -2 -2 98 -2 smooth_quartz_stairs[facing=north,half=top]",
				"/fill -2 98 -1 -2 98 2 smooth_quartz_stairs[facing=west,half=top]",
				"/fill -2 99 3 3 99 3 smooth_quartz_stairs[facing=north,half=bottom]",
				"/fill -2 98 3 3 98 3 smooth_quartz_stairs[facing=north,half=top]",
				"/fill 3 99 2 3 99 -3 smooth_quartz_stairs[facing=west,half=bottom]",
				"/fill 3 98 2 3 98 -3 smooth_quartz_stairs[facing=west,half=top]",
				"/fill 2 99 -3 -3 99 -3 smooth_quartz_stairs[facing=south,half=bottom]",
				"/fill 2 98 -3 -3 98 -3 smooth_quartz_stairs[facing=south,half=top]",
				"/fill -3 99 -2 -3 99 3 smooth_quartz_stairs[facing=east,half=bottom]",
				"/fill -3 98 -2 -3 98 3 smooth_quartz_stairs[facing=east,half=top]",
				"/setblock 0 104 0 chiseled_stone_bricks",
				"/fill -1 104 -1 1 104 -2 red_concrete",
				"/fill 1 104 -1 2 104 1 lime_concrete",
				"/fill 1 104 1 -1 104 2 blue_concrete",
				"/fill -1 104 1 -2 104 -1 orange_concrete",
				"/setblock -2 104 -2 black_concrete",
				"/setblock -1 104 -1 black_concrete",
				"/setblock 1 104 -1 black_concrete",
				"/setblock 2 104 -2 black_concrete",
				"/setblock 2 104 2 black_concrete",
				"/setblock 1 104 1 black_concrete",
				"/setblock -1 104 1 black_concrete",
				"/setblock -2 104 2 black_concrete",
				"/setblock 0 105 0 command_block[facing=down]",
				"/fill 0 105 1 1 105 1 smooth_quartz_stairs[facing=north,half=bottom]",
				"/fill 1 105 0 1 105 -1 smooth_quartz_stairs[facing=west,half=bottom]",
				"/fill 1 105 -1 -1 105 -1 smooth_quartz_stairs[facing=south,half=bottom]",
				"/fill -1 105 0 -1 105 1 smooth_quartz_stairs[facing=east,half=bottom]",
				"/fill -1 105 2 2 105 2 smooth_quartz_stairs[facing=south,half=bottom]",
				"/fill 2 105 1 2 105 -2 smooth_quartz_stairs[facing=east,half=bottom]",
				"/fill 1 105 -2 -2 105 -2 smooth_quartz_stairs[facing=north,half=bottom]",
				"/fill -2 105 -1 -2 105 2 smooth_quartz_stairs[facing=west,half=bottom]",
				"/fill -2 104 3 3 104 3 smooth_quartz_stairs[facing=north,half=top]",
				"/fill -2 105 3 3 105 3 smooth_quartz_stairs[facing=north,half=bottom]",
				"/fill 3 104 2 3 104 -3 smooth_quartz_stairs[facing=west,half=top]",
				"/fill 3 105 2 3 105 -3 smooth_quartz_stairs[facing=west,half=bottom]",
				"/fill 2 104 -3 -3 104 -3 smooth_quartz_stairs[facing=south,half=top]",
				"/fill 2 105 -3 -3 105 -3 smooth_quartz_stairs[facing=south,half=bottom]",
				"/fill -3 104 -2 -3 104 3 smooth_quartz_stairs[facing=east,half=top]",
				"/fill -3 105 -2 -3 105 3 smooth_quartz_stairs[facing=east,half=bottom]"
			};

			// Act
			var commands = DuckBlind.Generate();
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
