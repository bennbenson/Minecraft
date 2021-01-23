using System.Collections.Generic;
using Minecraft.Model;
using Minecraft.Model.Java;

namespace Minecraft.Construction.Java
{
	public static class DuckBlind
	{
		public static IEnumerable<Command> Generate()
		{
			Block chiseledStoneBrick = Block.Get(BlockID.StoneBricks.Chiseled);
			Block redConcrete = Block.Get(BlockID.Concrete.Red);
			Block limeConcrete = Block.Get(BlockID.Concrete.Lime);
			Block blueConcrete = Block.Get(BlockID.Concrete.Blue);
			Block orangeConcrete = Block.Get(BlockID.Concrete.Orange);
			Block blackConcrete = Block.Get(BlockID.Concrete.Black);
			Block bottomCommandBlock = Block.Get(BlockID.CommandBlock).With("facing", "up");
			Block topCommandBlock = Block.Get(BlockID.CommandBlock).With("facing", "down");
			Block smoothQuartzStairs = Block.Get(BlockID.SmoothQuartzStairs);
			Dictionary<string, Block> stairs = new Dictionary<string, Block>()
			{
				{ "east-bottom",  smoothQuartzStairs.With(("facing", "east" ), ("half", "bottom")) },
				{ "west-bottom",  smoothQuartzStairs.With(("facing", "west" ), ("half", "bottom")) },
				{ "south-bottom", smoothQuartzStairs.With(("facing", "south"), ("half", "bottom")) },
				{ "north-bottom", smoothQuartzStairs.With(("facing", "north"), ("half", "bottom")) },
				{ "east-top",     smoothQuartzStairs.With(("facing", "east" ), ("half", "top"   )) },
				{ "west-top",     smoothQuartzStairs.With(("facing", "west" ), ("half", "top"   )) },
				{ "south-top",    smoothQuartzStairs.With(("facing", "south"), ("half", "top"   )) },
				{ "north-top",    smoothQuartzStairs.With(("facing", "north"), ("half", "top"   )) }
			};

			foreach (var platform in new[]
			{
				new { Y = 99,  Layer1Offset = -1, CommandBlock = bottomCommandBlock },
				new { Y = 104, Layer1Offset =  1, CommandBlock = topCommandBlock    }
			})
			{
				//////////////////////////////////////////////////////////////////////////////
				// Layer 0

				yield return new SetBlockCommand(Coord2.At(0, 0).AtY(platform.Y), chiseledStoneBrick);

				foreach (var layer0Arrows in new[]
				{
					new { From = Coord2.At(-1, -1), To = Coord2.At( 1, -2), Concrete = redConcrete    },
					new { From = Coord2.At( 1, -1), To = Coord2.At( 2,  1), Concrete = limeConcrete   },
					new { From = Coord2.At( 1,  1), To = Coord2.At(-1,  2), Concrete = blueConcrete   },
					new { From = Coord2.At(-1,  1), To = Coord2.At(-2, -1), Concrete = orangeConcrete },
				})
				{
					yield return new FillCommand(layer0Arrows.From.AtY(platform.Y), layer0Arrows.To.AtY(platform.Y), layer0Arrows.Concrete);
				}

				foreach (var layer0Diagonal in new[]
				{
					Coord2.At(-2, -2), Coord2.At(-1, -1), Coord2.At( 1, -1), Coord2.At( 2, -2),
					Coord2.At( 2,  2), Coord2.At( 1,  1), Coord2.At(-1,  1), Coord2.At(-2,  2),
				})
				{
					yield return new SetBlockCommand(layer0Diagonal.AtY(platform.Y), blackConcrete);
				}


				//////////////////////////////////////////////////////////////////////////////
				// Layer 1

				string upsideDownBit = platform.Layer1Offset < 0 ? "top" : "bottom";
				int layer1Y = platform.Y + platform.Layer1Offset;

				yield return new SetBlockCommand(Position.Absolute(0, layer1Y, 0), platform.CommandBlock);

				foreach (var layer1 in new[]
				{
					new { From = Coord2.At( 0,  1), To = Coord2.At( 1,  1), Facing = "north" },
					new { From = Coord2.At( 1,  0), To = Coord2.At( 1, -1), Facing = "west"  },
					new { From = Coord2.At( 1, -1), To = Coord2.At(-1, -1), Facing = "south" },
					new { From = Coord2.At(-1,  0), To = Coord2.At(-1,  1), Facing = "east"  },
					new { From = Coord2.At(-1,  2), To = Coord2.At( 2,  2), Facing = "south" },
					new { From = Coord2.At( 2,  1), To = Coord2.At( 2, -2), Facing = "east"  },
					new { From = Coord2.At( 1, -2), To = Coord2.At(-2, -2), Facing = "north" },
					new { From = Coord2.At(-2, -1), To = Coord2.At(-2,  2), Facing = "west"  }
				})
				{
					yield return new FillCommand(layer1.From.AtY(layer1Y), layer1.To.AtY(layer1Y), stairs[$"{layer1.Facing}-{upsideDownBit}"]);
				}

				//////////////////////////////////////////////////////////////////////////////
				// Outline

				string hereUpsideDownBit = platform.Layer1Offset < 0 ? "bottom" : "top";
				string awayUpsideDownBit = platform.Layer1Offset < 0 ? "top" : "bottom";
				foreach (var outline in new[]
				{
					new { From = Coord2.At(-2,  3), To = Coord2.At( 3,  3), Facing = "north" },
					new { From = Coord2.At( 3,  2), To = Coord2.At( 3, -3), Facing = "west" },
					new { From = Coord2.At( 2, -3), To = Coord2.At(-3, -3), Facing = "south" },
					new { From = Coord2.At(-3, -2), To = Coord2.At(-3,  3), Facing = "east" }
				})
				{
					yield return new FillCommand(outline.From.AtY(platform.Y), outline.To.AtY(platform.Y), stairs[$"{outline.Facing}-{hereUpsideDownBit}"]);
					yield return new FillCommand(outline.From.AtY(layer1Y), outline.To.AtY(layer1Y), stairs[$"{outline.Facing}-{awayUpsideDownBit}"]);
				}
			}
		}
	}
}
