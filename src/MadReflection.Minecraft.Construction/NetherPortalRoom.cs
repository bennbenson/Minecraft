using System;
using System.Collections.Generic;
using Minecraft.Model;

namespace Minecraft.Construction
{
	public static class NetherPortalRoom
	{
		public static IEnumerable<Command> Generate(NetherPortalRoomParameters parameters, Block block)
		{
			if (parameters is null)
				throw new ArgumentNullException(nameof(parameters));
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			if (parameters.Direction == Direction.West || parameters.Direction == Direction.East)
				return GenerateNetherPortalRoomX(parameters, block);

			return GenerateNetherPortalRoomZ(parameters, block);
		}

		public static IEnumerable<Command> GenerateNetherPortalRoomX(NetherPortalRoomParameters parameters, Block block)
		{
			Block obsidian = Block.GetByBedrockID("obsidian");

			Coord3 center = parameters.Center.AtY(parameters.YBase);
			int xTranslation = parameters.Direction == Direction.West ? -1 : 1;
			int extraHeight = parameters.ExtraHigh ? 1 : 0;
			int extraWidth = parameters.ExtraWide ? 1 : 0;
			int extraDepth = parameters.ExtraWide ? 1 : 0;

			yield return new FillCommand(center.Add(0, 0, -2), center.Add(0, 4 + extraHeight, 2), obsidian, FillMode.Replace);
			yield return new FillCommand(center.Add(1 * xTranslation, 0, -(2 + extraWidth)), center.Add((8 + extraDepth) * xTranslation, 4 + extraHeight, 2 + extraWidth), block, FillMode.Hollow);
			yield return new FillCommand(center.Add(0, 1, -1), center.Add(1 * xTranslation, 3 + extraHeight, 1), Block.Air, FillMode.Replace);
		}

		public static IEnumerable<Command> GenerateNetherPortalRoomZ(NetherPortalRoomParameters parameters, Block block)
		{
			Block obsidian = Block.GetByBedrockID("obsidian");

			Coord3 center = parameters.Center.AtY(parameters.YBase);
			int zTranslation = parameters.Direction == Direction.North ? -1 : 1;
			int extraHeight = parameters.ExtraHigh ? 1 : 0;
			int extraWidth = parameters.ExtraWide ? 1 : 0;
			int extraDepth = parameters.ExtraWide ? 1 : 0;

			yield return new FillCommand(center.Add(-2, 0, 0), center.Add(2, 4 + extraHeight, 0), obsidian, FillMode.Replace);
			yield return new FillCommand(center.Add(-(2 + extraWidth), 0, 1 * zTranslation), center.Add(2 + extraWidth, 4 + extraHeight, (8 + extraDepth) * zTranslation), block, FillMode.Hollow);
			yield return new FillCommand(center.Add(-1, 1, 0), center.Add(1, 3 + extraHeight, 1 * zTranslation), Block.Air, FillMode.Replace);
		}
	}
}
