using System;
using System.Collections.Generic;
using System.Linq;
using Minecraft.Model;
using Minecraft.Model.Java;

namespace Minecraft.Construction.Java
{
	public static class NetherPortalRoom
	{
		public static IEnumerable<Command> Generate(NetherPortalRoomParameters parameters, Block block)
		{
			if (parameters is null)
				throw new ArgumentNullException(nameof(parameters));
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			Block obsidian = Block.Get("obsidian");

			Coord3 center = parameters.Center.AtY(parameters.YBase);
			int extraHeight = parameters.ExtraHigh ? 1 : 0;
			int extraWidth = parameters.ExtraWide ? 1 : 0;
			int extraDepth = parameters.ExtraWide ? 1 : 0;

			IEnumerable<Command> commands = GenerateInternal().ToArray();

			Coord3 moveTo = parameters.Center.AtY(parameters.YBase);
			Translation translation = parameters.Direction switch
			{
				Direction.West => Translation.Rotate90,
				Direction.North => Translation.Rotate180,
				Direction.East => Translation.Rotate270,
				_ => Translation.Identity,
			};
			commands = commands.TranslateAndMove(translation, moveTo);

			return commands;

			IEnumerable<Command> GenerateInternal()
			{
				yield return new FillCommand((-2, 0, 0), (2, 4 + extraHeight, 0), obsidian, FillMode.Replace);
				yield return new FillCommand((-2, 0, -1), (2, 4 + extraHeight, -1), block);
				yield return new FillCommand((-(2 + extraWidth), 0, 1), (2 + extraWidth, 4 + extraHeight, (8 + extraDepth)), block, FillMode.Hollow);
				yield return new FillCommand((-1, 1, -1), (1, 3 + extraHeight, 1), Block.Air, FillMode.Replace);
			}
		}
	}
}
