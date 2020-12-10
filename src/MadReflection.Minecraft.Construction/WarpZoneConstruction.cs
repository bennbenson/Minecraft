using System;
using System.Collections.Generic;
using Minecraft.Model;

namespace Minecraft.Construction
{
	public static class WarpZoneConstruction
	{
		public static IEnumerable<Command> GenerateWarpZone(WarpZoneParameters parameters, Block block)
		{
			if (parameters is null)
				throw new ArgumentNullException(nameof(parameters));
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			//if (yBase + 1 > 3 + levels * 7)
			//	throw new ArgumentOutOfRangeException(nameof(levels), "Levels cannot exceed the vertical build limit.");

			return GenerateWarpZoneInternal(parameters, block);
		}

		private static IEnumerable<Command> GenerateWarpZoneInternal(WarpZoneParameters parameters, Block block)
		{
			// original parameters:: Coord2 center, int yBase, int radius, int levels
			Coord2 center = parameters.Center;
			int y = parameters.YBase;
			int radius = parameters.Radius;
			int interstitialHeight = parameters.InterstitialHeight;

			int fullHeight = (parameters.Levels + 1) * (interstitialHeight == 0 ? 1 : interstitialHeight + 2) + parameters.Levels * parameters.LevelHeight;

			yield return new FillCommand(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y + fullHeight - 1), block);

			for (int index = 0; index < parameters.Levels; ++index)
			{
				if (interstitialHeight > 0)
				{
					yield return new FillCommand(center.Add(-radius, -radius).AtY(y), center.Add(radius, radius).AtY(y + interstitialHeight + 1), block, FillMode.Hollow);

					y += 1 + interstitialHeight;
				}

				++y;

				yield return new FillCommand(center.Add(-(radius - 3), -(radius - 3)).AtY(y), center.Add((radius - 3), (radius - 3)).AtY(y + parameters.LevelHeight - 1), Block.Air);

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
				yield return new TeleportCommand(new TeleportTarget(center.AtY(tpY)), center.AddX(1).AtY(tpY));
			}
		}


		//private static class Version1
		//{
		//	public static IEnumerable<Command> GenerateWarpZone(Coord2 center, int yBase, int radius, int levels, Block block)
		//	{
		//		if (block is null)
		//			throw new ArgumentNullException(nameof(block));
		//		if (yBase < 0)
		//			throw new ArgumentOutOfRangeException(nameof(yBase), "Base must be at least 0.");
		//		if (radius < 5)
		//			throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be at least 5.");
		//		if (levels < 1)
		//			throw new ArgumentOutOfRangeException(nameof(levels), "Levels must be at least 1.");
		//		if (yBase + 1 > 3 + levels * 7)
		//			throw new ArgumentOutOfRangeException(nameof(levels), "Levels cannot exceed the vertical build limit.");

		//		return GenerateWarpZoneInternal(center, yBase, radius, levels, block);
		//	}

		//	private static IEnumerable<Command> GenerateWarpZoneInternal(Coord2 center, int yBase, int radius, int levels, Block block)
		//	{
		//		yield return new FillCommand(center.Add(-radius, -radius).AtY(yBase), center.Add(radius, radius).AtY(yBase + 2), block, FillMode.Hollow);

		//		for (int index = 0; index < levels; ++index)
		//		{
		//			int levelBase = yBase + 3 + index * 7;

		//			yield return new FillCommand(center.Add(-radius, -radius).AtY(levelBase), center.Add(radius, radius).AtY(levelBase + 3), block);
		//			yield return new FillCommand(center.Add(-(radius - 3), -(radius - 3)).AtY(levelBase), center.Add(radius - 3, radius - 3).AtY(levelBase + 3), Block.Air);
		//			yield return new FillCommand(center.Add(-(radius - 1), -(radius - 3)).AtY(levelBase + 1), center.Add(radius - 1, radius - 3).AtY(levelBase + 1), Block.Air);
		//			yield return new FillCommand(center.Add(-(radius - 3), -(radius - 1)).AtY(levelBase + 1), center.Add(radius - 3, radius - 1).AtY(levelBase + 1), Block.Air);
		//			yield return new FillCommand(center.Add(-radius, -radius).AtY(levelBase + 4), center.Add(radius, radius).AtY(levelBase + 6), block, FillMode.Hollow);
		//		}
		//	}
		//}
	}
}
