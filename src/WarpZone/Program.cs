using System;
using System.Linq;
using CommandLine;
using Minecraft.Construction;
using Minecraft.Model;
using Minecraft.Model.Bedrock;

namespace WarpZoneGenerator
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			WarpZoneParameters warpZoneParameters = new WarpZoneParameters();

			ParserResult<Options> result = Parser.Default.ParseArguments<Options>(args)
				.WithNotParsed(o =>
				{
				})
				.WithParsed(o =>
				{
					if (o.Center is not null and not "")
						warpZoneParameters.Center = Coord2.Parse(o.Center);
					if (o.YBase is int yBase and > 0)
						warpZoneParameters.YBase = yBase;
					if (o.Levels is int levels and > 0)
						warpZoneParameters.Levels = levels;
					if (o.Radius is int radius and >= 5)
						warpZoneParameters.Radius = radius;
					if (o.InteriorHeight is int interiorHeight and > 1)
						warpZoneParameters.InteriorHeight = interiorHeight;
					if (o.InterstitialHeight is int interstitialHeight and >= 0)
						warpZoneParameters.InterstitialHeight = interstitialHeight;
					if (o.CutSides is bool cutSides)
						warpZoneParameters.CutSides = cutSides;
					if (o.TeleportIn is bool teleportIn)
						warpZoneParameters.TeleportIn = teleportIn;
				});

			Command[] commands = Minecraft.Construction.Bedrock.WarpZone.Generate(warpZoneParameters, Block.Get(BlockID.StoneBrick)).ToArray();

			foreach (var command in commands)
			{
				Console.WriteLine(command.GetCommandText());
			}
		}
	}
}
