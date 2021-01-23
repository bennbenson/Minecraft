using System.Linq;
using Minecraft.Model;
using Minecraft.Model.Java;
using NUnit.Framework;

namespace Minecraft.Construction.Java.Tests
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
			Block netherBrick = Block.Get(BlockID.NetherBricks);

			// Act
			Command[] commands = NetherPortalRoom.Generate(parameters, netherBrick).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText()).ToArray();

			// Assert
			Assert.That(commandTexts.Length, Is.EqualTo(4));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -2 3 0 2 7 0 obsidian"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -2 3 -1 2 7 -1 nether_bricks"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -2 3 1 2 7 8 nether_bricks hollow"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill -1 4 -1 1 6 1 air"));
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
			Block netherBrick = Block.Get(BlockID.NetherBricks);

			// Act
			Command[] commands = NetherPortalRoom.Generate(parameters, netherBrick).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText()).ToArray();

			// Assert
			Assert.That(commandTexts.Length, Is.EqualTo(4));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -2 3 0 2 8 0 obsidian"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -2 3 -1 2 8 -1 nether_bricks"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -3 3 1 3 8 9 nether_bricks hollow"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill -1 4 -1 1 7 1 air"));
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
			Block netherBrick = Block.Get(BlockID.RedNetherBricks);

			// Act
			Command[] commands = NetherPortalRoom.Generate(parameters, netherBrick).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText()).ToArray();

			// Assert
			Assert.That(commandTexts.Length, Is.EqualTo(4));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -2 3 0 2 7 0 obsidian"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -2 3 1 2 7 1 red_nether_bricks"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -2 3 -1 2 7 -8 red_nether_bricks hollow"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill -1 4 1 1 6 -1 air"));
		}
	}
}
