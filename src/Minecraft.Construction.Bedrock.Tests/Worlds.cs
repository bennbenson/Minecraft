using System;
using System.Linq;
using Minecraft.Model;
using Minecraft.Model.Bedrock;
using NUnit.Framework;

namespace Minecraft.Construction.Bedrock.Tests
{
	[TestFixture]
	public class Worlds
	{
		[TestCase]
		public void Ravine_Extreme()
		{
			WarpZoneParameters parameters = new()
			{
				Center = Coord2.At(0, 0),
				YBase = 1,
				Levels = 4,
				Radius = 9,
				InteriorHeight = 4,
				InterstitialHeight = 1
			};
			Block stoneBrick = Block.Get(BlockID.StoneBrick);
			Block blackStoneBrick = Block.Get(BlockID.PolishedBlackstoneBricks);

			Command[] normalCommands = WarpZone.Generate(parameters, stoneBrick).ToArray();
			Command[] netherCommands = WarpZone.Generate(parameters, blackStoneBrick).ToArray();

			string normalScript = string.Join("", normalCommands.Select(c => c.GetCommandText() + Environment.NewLine));
			string netherScript = string.Join("", netherCommands.Select(c => c.GetCommandText() + Environment.NewLine));
		}

		[TestCase]
		public void Base_House_Sandbox()
		{
			WarpZoneParameters parameters = new()
			{
				YBase = 6,
				Levels = 2,
				Radius = 13,
			};
			Block stoneBrick = Block.Get(BlockID.StoneBrick);
			Block blackStoneBrick = Block.Get(BlockID.PolishedBlackstoneBricks);
			Block endStoneBrick = Block.Get(BlockID.EndBricks);

			Command[] normalCommands = WarpZone.Generate(parameters, stoneBrick).ToArray();
			Command[] netherCommands = WarpZone.Generate(parameters, blackStoneBrick).ToArray();
			Command[] endCommands = WarpZone.Generate(parameters, endStoneBrick).ToArray();

			string normalScript = string.Join("", normalCommands.Select(c => c.GetCommandText() + Environment.NewLine));
			string netherScript = string.Join("", netherCommands.Select(c => c.GetCommandText() + Environment.NewLine));
		}

		[TestCase]
		public void Tokes()
		{
			WarpZoneParameters parameters = new()
			{
				YBase = 7,
				Levels = 2,
				Radius = 13,
			};
			Block stoneBrick = Block.Get(BlockID.StoneBrick);
			Block blackStoneBrick = Block.Get(BlockID.PolishedBlackstoneBricks);

			Command[] normalCommands = WarpZone.Generate(parameters, stoneBrick).ToArray();
			Command[] netherCommands = WarpZone.Generate(parameters, blackStoneBrick).ToArray();

			string scriptVariable = ScriptUtility.GenerateScriptVariable(normalCommands, netherCommands);
		}

		[TestCase]
		public void TempOnWin10_Seed_1554387118()
		{
			NetherPortalRoomParameters? netherPortalParameters = new NetherPortalRoomParameters()
			{
				Center = new Coord2(0, 0),
				YBase = 3,
				Direction = Direction.South,
				ExtraHigh = true,
				ExtraWide = true
			};
			Block netherBrick = Block.Get(BlockID.StainedGlass, DataValue.BlockColor.Red);
			WarpZoneParameters warpZoneParameters = new WarpZoneParameters()
			{
				Center = new Coord2(0, 0),
				YBase = 10,
				Levels = 11,
				Radius = 15
			};
			Block stoneBrick = Block.Get(BlockID.StainedGlass, DataValue.BlockColor.Gray);


			Command[] netherPortalCommands = NetherPortalRoom.Generate(netherPortalParameters, netherBrick).ToArray();
			Command[] warpZoneCommands = WarpZone.Generate(warpZoneParameters, stoneBrick).ToArray();
			Command[] duckBlindCommands = DuckBlind.Generate().ToArray();

			string scriptVariable = ScriptUtility.GenerateScriptVariable(netherPortalCommands, warpZoneCommands, duckBlindCommands);
		}
	}
}
