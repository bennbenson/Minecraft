using System.Linq;
using Minecraft.Model;
using NUnit.Framework;

namespace Minecraft.Construction.Bedrock.Tests
{
	[TestFixture]
	public class DuckBlind_Tests
	{
		[TestCase]
		public void Generate_1()
		{
			// Arrange

			// Act
			Command[] commands = DuckBlind.Generate().ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText()).ToArray();

			// Assert
			Assert.That(commandTexts.Count, Is.EqualTo(60));
			Assert.That(commandTexts[0], Is.EqualTo("/setblock 0 99 0 stonebrick 3"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -1 99 -1 1 99 -2 concrete 14"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill 1 99 -1 2 99 1 concrete 5"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill 1 99 1 -1 99 2 concrete 11"));
			Assert.That(commandTexts[4], Is.EqualTo("/fill -1 99 1 -2 99 -1 concrete 1"));
			Assert.That(commandTexts[5], Is.EqualTo("/setblock -2 99 -2 concrete 15"));
			Assert.That(commandTexts[6], Is.EqualTo("/setblock -1 99 -1 concrete 15"));
			Assert.That(commandTexts[7], Is.EqualTo("/setblock 1 99 -1 concrete 15"));
			Assert.That(commandTexts[8], Is.EqualTo("/setblock 2 99 -2 concrete 15"));
			Assert.That(commandTexts[9], Is.EqualTo("/setblock 2 99 2 concrete 15"));
			Assert.That(commandTexts[10], Is.EqualTo("/setblock 1 99 1 concrete 15"));
			Assert.That(commandTexts[11], Is.EqualTo("/setblock -1 99 1 concrete 15"));
			Assert.That(commandTexts[12], Is.EqualTo("/setblock -2 99 2 concrete 15"));
			Assert.That(commandTexts[13], Is.EqualTo("/setblock 0 98 0 command_block 1"));
			Assert.That(commandTexts[14], Is.EqualTo("/fill 0 98 1 1 98 1 smooth_quartz_stairs 7"));
			Assert.That(commandTexts[15], Is.EqualTo("/fill 1 98 0 1 98 -1 smooth_quartz_stairs 5"));
			Assert.That(commandTexts[16], Is.EqualTo("/fill 1 98 -1 -1 98 -1 smooth_quartz_stairs 6"));
			Assert.That(commandTexts[17], Is.EqualTo("/fill -1 98 0 -1 98 1 smooth_quartz_stairs 4"));
			Assert.That(commandTexts[18], Is.EqualTo("/fill -1 98 2 2 98 2 smooth_quartz_stairs 6"));
			Assert.That(commandTexts[19], Is.EqualTo("/fill 2 98 1 2 98 -2 smooth_quartz_stairs 4"));
			Assert.That(commandTexts[20], Is.EqualTo("/fill 1 98 -2 -2 98 -2 smooth_quartz_stairs 7"));
			Assert.That(commandTexts[21], Is.EqualTo("/fill -2 98 -1 -2 98 2 smooth_quartz_stairs 5"));
			Assert.That(commandTexts[22], Is.EqualTo("/fill -2 99 3 3 99 3 smooth_quartz_stairs 3"));
			Assert.That(commandTexts[23], Is.EqualTo("/fill -2 98 3 3 98 3 smooth_quartz_stairs 7"));
			Assert.That(commandTexts[24], Is.EqualTo("/fill 3 99 2 3 99 -3 smooth_quartz_stairs 1"));
			Assert.That(commandTexts[25], Is.EqualTo("/fill 3 98 2 3 98 -3 smooth_quartz_stairs 5"));
			Assert.That(commandTexts[26], Is.EqualTo("/fill 2 99 -3 -3 99 -3 smooth_quartz_stairs 2"));
			Assert.That(commandTexts[27], Is.EqualTo("/fill 2 98 -3 -3 98 -3 smooth_quartz_stairs 6"));
			Assert.That(commandTexts[28], Is.EqualTo("/fill -3 99 -2 -3 99 3 smooth_quartz_stairs"));
			Assert.That(commandTexts[29], Is.EqualTo("/fill -3 98 -2 -3 98 3 smooth_quartz_stairs 4"));
			Assert.That(commandTexts[30], Is.EqualTo("/setblock 0 104 0 stonebrick 3"));
			Assert.That(commandTexts[31], Is.EqualTo("/fill -1 104 -1 1 104 -2 concrete 14"));
			Assert.That(commandTexts[32], Is.EqualTo("/fill 1 104 -1 2 104 1 concrete 5"));
			Assert.That(commandTexts[33], Is.EqualTo("/fill 1 104 1 -1 104 2 concrete 11"));
			Assert.That(commandTexts[34], Is.EqualTo("/fill -1 104 1 -2 104 -1 concrete 1"));
			Assert.That(commandTexts[35], Is.EqualTo("/setblock -2 104 -2 concrete 15"));
			Assert.That(commandTexts[36], Is.EqualTo("/setblock -1 104 -1 concrete 15"));
			Assert.That(commandTexts[37], Is.EqualTo("/setblock 1 104 -1 concrete 15"));
			Assert.That(commandTexts[38], Is.EqualTo("/setblock 2 104 -2 concrete 15"));
			Assert.That(commandTexts[39], Is.EqualTo("/setblock 2 104 2 concrete 15"));
			Assert.That(commandTexts[40], Is.EqualTo("/setblock 1 104 1 concrete 15"));
			Assert.That(commandTexts[41], Is.EqualTo("/setblock -1 104 1 concrete 15"));
			Assert.That(commandTexts[42], Is.EqualTo("/setblock -2 104 2 concrete 15"));
			Assert.That(commandTexts[43], Is.EqualTo("/setblock 0 105 0 command_block"));
			Assert.That(commandTexts[44], Is.EqualTo("/fill 0 105 1 1 105 1 smooth_quartz_stairs 3"));
			Assert.That(commandTexts[45], Is.EqualTo("/fill 1 105 0 1 105 -1 smooth_quartz_stairs 1"));
			Assert.That(commandTexts[46], Is.EqualTo("/fill 1 105 -1 -1 105 -1 smooth_quartz_stairs 2"));
			Assert.That(commandTexts[47], Is.EqualTo("/fill -1 105 0 -1 105 1 smooth_quartz_stairs"));
			Assert.That(commandTexts[48], Is.EqualTo("/fill -1 105 2 2 105 2 smooth_quartz_stairs 2"));
			Assert.That(commandTexts[49], Is.EqualTo("/fill 2 105 1 2 105 -2 smooth_quartz_stairs"));
			Assert.That(commandTexts[50], Is.EqualTo("/fill 1 105 -2 -2 105 -2 smooth_quartz_stairs 3"));
			Assert.That(commandTexts[51], Is.EqualTo("/fill -2 105 -1 -2 105 2 smooth_quartz_stairs 1"));
			Assert.That(commandTexts[52], Is.EqualTo("/fill -2 104 3 3 104 3 smooth_quartz_stairs 7"));
			Assert.That(commandTexts[53], Is.EqualTo("/fill -2 105 3 3 105 3 smooth_quartz_stairs 3"));
			Assert.That(commandTexts[54], Is.EqualTo("/fill 3 104 2 3 104 -3 smooth_quartz_stairs 5"));
			Assert.That(commandTexts[55], Is.EqualTo("/fill 3 105 2 3 105 -3 smooth_quartz_stairs 1"));
			Assert.That(commandTexts[56], Is.EqualTo("/fill 2 104 -3 -3 104 -3 smooth_quartz_stairs 6"));
			Assert.That(commandTexts[57], Is.EqualTo("/fill 2 105 -3 -3 105 -3 smooth_quartz_stairs 2"));
			Assert.That(commandTexts[58], Is.EqualTo("/fill -3 104 -2 -3 104 3 smooth_quartz_stairs 4"));
			Assert.That(commandTexts[59], Is.EqualTo("/fill -3 105 -2 -3 105 3 smooth_quartz_stairs"));
		}
	}
}
