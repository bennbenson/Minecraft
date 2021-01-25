using System;
using System.Collections.Generic;
using Minecraft.Model;
using Minecraft.Model.Bedrock;

namespace Minecraft.Construction.Bedrock
{
	public static class WarpZone
	{
		public static IEnumerable<Command> Generate(WarpZoneParameters parameters, Block block)
		{
			if (parameters is null)
				throw new ArgumentNullException(nameof(parameters));
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			Coord2 center = parameters.Center;
			int y = parameters.YBase;
			int radius = parameters.Radius;
			int interiorHeight = parameters.InteriorHeight;
			int interstitialHeight = parameters.InterstitialHeight;

			int fullHeight = (parameters.Levels + 1) * (interstitialHeight + 2) + parameters.Levels * interiorHeight;

			int xzSize = Volume.CountBlocks(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y));
			int planeCount = (short.MaxValue + 1) / xzSize;

			int yLow = y;
			int yHighest = y + fullHeight - 1;
			int yHigh = Math.Min(y + planeCount - 1, yHighest);

			if (yHighest > 255)
				throw new ArgumentException("The resulting structure exceeds the maximum build height.", nameof(parameters));

			if ((parameters.Radius * 2 + 1) * (parameters.InterstitialHeight + 2) > 32768)
				throw new ArgumentException("The resulting structure cannot be built because the interstitial space is too large.", nameof(parameters));

			if (((parameters.Radius - 3) * 2 + 1) * parameters.InterstitialHeight > 32768)
				throw new ArgumentException("The resulting structure cannot be built because the interior space is too large.", nameof(parameters));

			if (parameters.Walls is not null && parameters.Radius < parameters.Walls.Buffer + 3)
				throw new ArgumentException("Side buffer exceeds the entire inner radius", nameof(parameters));

			CutSidesOnly? cutSidesOnly = parameters.Walls as CutSidesOnly;
			PlaceCommandBlocks? placeCommandBlocks = parameters.Walls as PlaceCommandBlocks;

			CommandBlock? southFacingCommandBlock = null;
			CommandBlock? northFacingCommandBlock = null;
			CommandBlock? westFacingCommandBlock = null;
			CommandBlock? eastFacingCommandBlock = null;
			if (placeCommandBlocks is not null)
			{
				southFacingCommandBlock = new CommandBlock(Orientation.South, Command.Empty);
				northFacingCommandBlock = new CommandBlock(Orientation.North, Command.Empty);
				westFacingCommandBlock = new CommandBlock(Orientation.West, Command.Empty);
				eastFacingCommandBlock = new CommandBlock(Orientation.East, Command.Empty);
			}

			return Generate();

			IEnumerable<Command> Generate()
			{
				while (yLow < yHighest)
				{
					yield return new FillCommand(center.Add(-radius, -radius).AtY(yLow), center.Add(radius, radius).AtY(yHigh), block);

					yLow = yHigh + 1;
					yHigh = Math.Min(yLow + planeCount - 1, yHighest);
				}

				for (int index = 0; index < parameters.Levels; ++index)
				{
					if (interstitialHeight > 0)
						yield return new FillCommand(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y + interstitialHeight + 1), block, FillMode.Hollow);

					y += interstitialHeight + 2;

					yield return new FillCommand(center.Add(-(radius - 3), -(radius - 3)).AtY(y), center.Add((radius - 3), (radius - 3)).AtY(y + interiorHeight - 1), Block.Air);

					if (cutSidesOnly is not null)
					{
						yield return new FillCommand(center.Add(-(radius - (3 + cutSidesOnly.Buffer)), -(radius - 1)).AtY(y + 1), center.Add(radius - (3 + cutSidesOnly.Buffer), radius - 1).AtY(y + 1), Block.Air);
						yield return new FillCommand(center.Add(-(radius - 1), -(radius - (3 + cutSidesOnly.Buffer))).AtY(y + 1), center.Add(radius - 1, radius - (3 + cutSidesOnly.Buffer)).AtY(y + 1), Block.Air);
					}
					else if (placeCommandBlocks is not null)
					{
						yield return new FillCommand(center.Add(-(radius - (3 + placeCommandBlocks.Buffer)), -(radius - 2)).AtY(y + 1), center.Add(radius - (3 + placeCommandBlocks.Buffer), radius - 2).AtY(y + 1), Block.Air);
						yield return new FillCommand(center.Add(-(radius - 2), -(radius - (3 + placeCommandBlocks.Buffer))).AtY(y + 1), center.Add(radius - 2, radius - (3 + placeCommandBlocks.Buffer)).AtY(y + 1), Block.Air);
						yield return new FillCommand(center.Add(-(radius - (3 + placeCommandBlocks.Buffer)), -(radius - 1)).AtY(y + 1), center.Add(radius - (3 + placeCommandBlocks.Buffer), -(radius - 1)).AtY(y + 1), southFacingCommandBlock!);
						yield return new FillCommand(center.Add(-(radius - (3 + placeCommandBlocks.Buffer)), radius - 1).AtY(y + 1), center.Add(radius - (3 + placeCommandBlocks.Buffer), radius - 1).AtY(y + 1), northFacingCommandBlock!);
						yield return new FillCommand(center.Add(-(radius - 1), radius - (3 + placeCommandBlocks.Buffer)).AtY(y + 1), center.Add(-(radius - 1), -(radius - (3 + placeCommandBlocks.Buffer))).AtY(y + 1), eastFacingCommandBlock!);
						yield return new FillCommand(center.Add(radius - 1, radius - (3 + placeCommandBlocks.Buffer)).AtY(y + 1), center.Add(radius - 1, -(radius - (3 + placeCommandBlocks.Buffer))).AtY(y + 1), westFacingCommandBlock!);
					}

					y += interiorHeight;
				}

				if (interstitialHeight > 0)
					yield return new FillCommand(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y + interstitialHeight + 1), block, FillMode.Hollow);

				if (parameters.TeleportIn)
				{
					int tpY = parameters.YBase + interstitialHeight + 3;

					yield return new SayCommand($"You can teleport to {center.AtY(tpY).GetArgumentText()}.");
				}
			}
		}
	}
}
