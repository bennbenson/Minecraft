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

			return GenerateInternal(parameters, block);
		}

		private static IEnumerable<Command> GenerateInternal(WarpZoneParameters parameters, Block block)
		{
			Coord2 center = parameters.Center;
			int y = parameters.YBase;
			int radius = parameters.Radius;
			int interstitialHeight = parameters.InterstitialHeight;

			int fullHeight = (parameters.Levels + 1) * (interstitialHeight == 0 ? 1 : interstitialHeight + 2) + parameters.Levels * parameters.InteriorHeight;

			int xzSize = Volume.CountBlocks(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y));
			int planeCount = (short.MaxValue + 1) / xzSize;

			int yLow = y;
			int yHighest = y + fullHeight - 1;
			int yHigh = Math.Min(y + planeCount - 1, yHighest);

			while (yLow < yHighest)
			{
				yield return new FillCommand(center.Add(-radius, -radius).AtY(yLow), center.Add(radius, radius).AtY(yHigh), block);

				yLow = yHigh + 1;
				yHigh = Math.Min(yLow + planeCount - 1, yHighest);
			}

			for (int index = 0; index < parameters.Levels; ++index)
			{
				if (interstitialHeight > 0)
				{
					yield return new FillCommand(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y + interstitialHeight + 1), block, FillMode.Hollow);

					y += 1 + interstitialHeight;
				}

				++y;

				yield return new FillCommand(center.Add(-(radius - 3), -(radius - 3)).AtY(y), center.Add((radius - 3), (radius - 3)).AtY(y + parameters.InteriorHeight - 1), Block.Air);

				if (parameters.CutSides)
				{
					yield return new FillCommand(center.Add(-(radius - 1), -(radius - 3)).AtY(y + 1), center.Add(radius - 1, radius - 3).AtY(y + 1), Block.Air);
					yield return new FillCommand(center.Add(-(radius - 3), -(radius - 1)).AtY(y + 1), center.Add(radius - 3, radius - 1).AtY(y + 1), Block.Air);
				}

				y += 4;
			}

			if (interstitialHeight > 0)
				yield return new FillCommand(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y + interstitialHeight + 1), block, FillMode.Hollow);

			if (parameters.TeleportIn)
			{
				int tpY = parameters.YBase + (interstitialHeight == 0 ? 0 : interstitialHeight + 1);
				yield return new TeleportCommand(new TargetPosition(center.AtY(tpY)));
			}
		}
	}
}
