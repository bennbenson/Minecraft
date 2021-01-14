using System;
using System.Linq;
using Minecraft.Model;
using NUnit.Framework;

namespace Minecraft.Construction.Tests
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
				InterstitialHeight = 1,
				CutSides = false
			};
			Block stoneBrick = Block.GetByBedrockID("stonebrick");
			Block blackStoneBrick = Block.GetByBedrockID("polished_blackstone_bricks");

			Command[] normalCommands = WarpZone.Generate(parameters, stoneBrick).ToArray();
			Command[] netherCommands = WarpZone.Generate(parameters, blackStoneBrick).ToArray();

			string normalScript = string.Join("", normalCommands.Select(c => c.GetCommandText(Edition.Bedrock) + Environment.NewLine));
			string netherScript = string.Join("", netherCommands.Select(c => c.GetCommandText(Edition.Bedrock) + Environment.NewLine));
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
			Block stoneBrick = Block.GetByBedrockID("stonebrick");
			Block blackStoneBrick = Block.GetByBedrockID("polished_blackstone_bricks");
			Block endStoneBrick = Block.GetByBedrockID("end_bricks");

			Command[] normalCommands = WarpZone.Generate(parameters, stoneBrick).ToArray();
			Command[] netherCommands = WarpZone.Generate(parameters, blackStoneBrick).ToArray();
			Command[] endCommands = WarpZone.Generate(parameters, endStoneBrick).ToArray();

			string normalScript = string.Join("", normalCommands.Select(c => c.GetCommandText(Edition.Bedrock) + Environment.NewLine));
			string netherScript = string.Join("", netherCommands.Select(c => c.GetCommandText(Edition.Bedrock) + Environment.NewLine));
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
			Block stoneBrick = Block.GetByBedrockID("stonebrick");
			Block blackStoneBrick = Block.GetByBedrockID("polished_blackstone_bricks");

			Command[] normalCommands = WarpZone.Generate(parameters, stoneBrick).ToArray();
			Command[] netherCommands = WarpZone.Generate(parameters, blackStoneBrick).ToArray();

			string normalScript = string.Join("", normalCommands.Select(c => c.GetCommandText(Edition.Bedrock) + Environment.NewLine));
			string netherScript = string.Join("", netherCommands.Select(c => c.GetCommandText(Edition.Bedrock) + Environment.NewLine));
		}
	}
}
