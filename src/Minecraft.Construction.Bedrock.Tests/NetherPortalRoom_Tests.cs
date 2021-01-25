using System.Linq;
using Minecraft.Model;
using Minecraft.Model.Bedrock;
using NUnit.Framework;

namespace Minecraft.Construction.Bedrock.Tests
{
	[TestFixture]
	public class NetherPortalRoom_Tests
	{
		[TestCase]
		public void Defaults()
		{
			// Arrange
			var parameters = new NetherPortalRoomParameters();
			var block = Block.Get(BlockID.NetherBrick);

			var expected = new string[]
			{
				"/fill -2 1 0 2 5 0 obsidian",
				"/fill -2 1 -1 2 5 -1 nether_brick",
				"/fill -2 1 1 2 5 8 nether_brick 0 hollow",
				"/fill -1 2 -1 1 4 1 air"
			};

			// Act
			var commands = NetherPortalRoom.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_3_YBase()
		{
			// Arrange
			var parameters = new NetherPortalRoomParameters()
			{
				YBase = 3
			};
			var block = Block.Get(BlockID.NetherBrick);

			var expected = new string[]
			{
				"/fill -2 3 0 2 7 0 obsidian",
				"/fill -2 3 -1 2 7 -1 nether_brick",
				"/fill -2 3 1 2 7 8 nether_brick 0 hollow",
				"/fill -1 4 -1 1 6 1 air"
			};

			// Act
			var commands = NetherPortalRoom.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_ExtraHigh_ExtraWide()
		{
			// Arrange
			var parameters = new NetherPortalRoomParameters()
			{
				ExtraHigh = true,
				ExtraWide = true
			};
			var block = Block.Get(BlockID.NetherBrick);

			var expected = new string[]
			{
				"/fill -2 1 0 2 6 0 obsidian",
				"/fill -2 1 -1 2 6 -1 nether_brick",
				"/fill -3 1 1 3 6 9 nether_brick 0 hollow",
				"/fill -1 2 -1 1 5 1 air"
			};

			// Act
			var commands = NetherPortalRoom.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_North_Direction()
		{
			// Arrange
			var parameters = new NetherPortalRoomParameters()
			{
				Direction = Direction.North
			};
			var block = Block.Get(BlockID.NetherBrick);

			var expected = new string[]
			{
				"/fill 2 1 0 -2 5 0 obsidian",
				"/fill 2 1 1 -2 5 1 nether_brick",
				"/fill 2 1 -1 -2 5 -8 nether_brick 0 hollow",
				"/fill 1 2 1 -1 4 -1 air"
			};

			// Act
			var commands = NetherPortalRoom.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
