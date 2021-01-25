using System.Linq;
using NUnit.Framework;

namespace Minecraft.Construction.Bedrock.Tests
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
				"/setblock 0 99 0 stonebrick 3",
				"/fill -1 99 -1 1 99 -2 concrete 14",
				"/fill 1 99 -1 2 99 1 concrete 5",
				"/fill 1 99 1 -1 99 2 concrete 11",
				"/fill -1 99 1 -2 99 -1 concrete 1",
				"/setblock -2 99 -2 concrete 15",
				"/setblock -1 99 -1 concrete 15",
				"/setblock 1 99 -1 concrete 15",
				"/setblock 2 99 -2 concrete 15",
				"/setblock 2 99 2 concrete 15",
				"/setblock 1 99 1 concrete 15",
				"/setblock -1 99 1 concrete 15",
				"/setblock -2 99 2 concrete 15",
				"/setblock 0 98 0 command_block 1",
				"/fill 0 98 1 1 98 1 smooth_quartz_stairs 7",
				"/fill 1 98 0 1 98 -1 smooth_quartz_stairs 5",
				"/fill 1 98 -1 -1 98 -1 smooth_quartz_stairs 6",
				"/fill -1 98 0 -1 98 1 smooth_quartz_stairs 4",
				"/fill -1 98 2 2 98 2 smooth_quartz_stairs 6",
				"/fill 2 98 1 2 98 -2 smooth_quartz_stairs 4",
				"/fill 1 98 -2 -2 98 -2 smooth_quartz_stairs 7",
				"/fill -2 98 -1 -2 98 2 smooth_quartz_stairs 5",
				"/fill -2 99 3 3 99 3 smooth_quartz_stairs 3",
				"/fill -2 98 3 3 98 3 smooth_quartz_stairs 7",
				"/fill 3 99 2 3 99 -3 smooth_quartz_stairs 1",
				"/fill 3 98 2 3 98 -3 smooth_quartz_stairs 5",
				"/fill 2 99 -3 -3 99 -3 smooth_quartz_stairs 2",
				"/fill 2 98 -3 -3 98 -3 smooth_quartz_stairs 6",
				"/fill -3 99 -2 -3 99 3 smooth_quartz_stairs",
				"/fill -3 98 -2 -3 98 3 smooth_quartz_stairs 4",
				"/setblock 0 104 0 stonebrick 3",
				"/fill -1 104 -1 1 104 -2 concrete 14",
				"/fill 1 104 -1 2 104 1 concrete 5",
				"/fill 1 104 1 -1 104 2 concrete 11",
				"/fill -1 104 1 -2 104 -1 concrete 1",
				"/setblock -2 104 -2 concrete 15",
				"/setblock -1 104 -1 concrete 15",
				"/setblock 1 104 -1 concrete 15",
				"/setblock 2 104 -2 concrete 15",
				"/setblock 2 104 2 concrete 15",
				"/setblock 1 104 1 concrete 15",
				"/setblock -1 104 1 concrete 15",
				"/setblock -2 104 2 concrete 15",
				"/setblock 0 105 0 command_block",
				"/fill 0 105 1 1 105 1 smooth_quartz_stairs 3",
				"/fill 1 105 0 1 105 -1 smooth_quartz_stairs 1",
				"/fill 1 105 -1 -1 105 -1 smooth_quartz_stairs 2",
				"/fill -1 105 0 -1 105 1 smooth_quartz_stairs",
				"/fill -1 105 2 2 105 2 smooth_quartz_stairs 2",
				"/fill 2 105 1 2 105 -2 smooth_quartz_stairs",
				"/fill 1 105 -2 -2 105 -2 smooth_quartz_stairs 3",
				"/fill -2 105 -1 -2 105 2 smooth_quartz_stairs 1",
				"/fill -2 104 3 3 104 3 smooth_quartz_stairs 7",
				"/fill -2 105 3 3 105 3 smooth_quartz_stairs 3",
				"/fill 3 104 2 3 104 -3 smooth_quartz_stairs 5",
				"/fill 3 105 2 3 105 -3 smooth_quartz_stairs 1",
				"/fill 2 104 -3 -3 104 -3 smooth_quartz_stairs 6",
				"/fill 2 105 -3 -3 105 -3 smooth_quartz_stairs 2",
				"/fill -3 104 -2 -3 104 3 smooth_quartz_stairs 4",
				"/fill -3 105 -2 -3 105 3 smooth_quartz_stairs"
			};

			// Act
			var commands = DuckBlind.Generate();
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
