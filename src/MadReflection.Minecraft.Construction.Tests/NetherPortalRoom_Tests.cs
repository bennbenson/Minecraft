using System.Linq;
using Minecraft.Model;
using NUnit.Framework;

namespace Minecraft.Construction.Tests
{
	[TestFixture]
	public class NetherPortalRoom_Tests
	{
		[TestCase]
		public void Defaults()
		{
			// Arrange
			NetherPortalRoomParameters parameters = new()
			{
				Center = new Coord2(0, 0),
				YBase = 3
			};
			Block netherBrick = Block.GetByBedrockID("nether_brick");

			// Act
			Command[] commands = NetherPortalRoom.Generate(parameters, netherBrick).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText(Edition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Length, Is.EqualTo(3));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -2 3 0 2 7 0 obsidian"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -2 3 1 2 7 8 nether_brick 0 hollow"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -1 4 0 1 6 1 air"));
		}

		[TestCase]
		public void Defaults_ExtraHigh_ExtraWide()
		{
			// Arrange
			NetherPortalRoomParameters parameters = new()
			{
				Center = new Coord2(0, 0),
				YBase = 3,
				ExtraHigh = true,
				ExtraWide = true
			};
			Block netherBrick = Block.GetByBedrockID("nether_brick");

			// Act
			Command[] commands = NetherPortalRoom.Generate(parameters, netherBrick).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText(Edition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Length, Is.EqualTo(3));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -2 3 0 2 8 0 obsidian"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -3 3 1 3 8 9 nether_brick 0 hollow"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -1 4 0 1 7 1 air"));
		}

		[TestCase]
		public void Defaults_Nether()
		{
			// Arrange
			NetherPortalRoomParameters parameters = new()
			{
				Center = new Coord2(0, 0),
				YBase = 3,
				Direction = Direction.North
			};
			Block netherBrick = Block.GetByBedrockID("red_nether_brick");

			// Act
			Command[] commands = NetherPortalRoom.Generate(parameters, netherBrick).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText(Edition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Length, Is.EqualTo(3));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -2 3 0 2 7 0 obsidian"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -2 3 -1 2 7 -8 red_nether_brick 0 hollow"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -1 4 0 1 6 -1 air"));
		}
	}
}
