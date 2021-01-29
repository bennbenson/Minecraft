using System.Collections.Generic;
using Minecraft.Model;
using Minecraft.Model.Bedrock;

namespace Minecraft.Construction.Bedrock
{
	public static class DuckBlind
	{
		private const int StairsEast = DataValue.Stairs.East;
		private const int StairsWest = DataValue.Stairs.West;
		private const int StairsNorth = DataValue.Stairs.North;
		private const int StairsSouth = DataValue.Stairs.South;
		private const int StairsInverted = DataValue.Stairs.Inverted;

		public static IEnumerable<Command> Generate()
		{
			Block chiseledStoneBrick = Block.Get(BlockID.StoneBrick, DataValue.StoneBrick.Chiseled);
			Block redConcrete = Block.Get(BlockID.Concrete, DataValue.BlockColor.Red);
			Block limeConcrete = Block.Get(BlockID.Concrete, DataValue.BlockColor.Lime);
			Block blueConcrete = Block.Get(BlockID.Concrete, DataValue.BlockColor.Blue);
			Block orangeConcrete = Block.Get(BlockID.Concrete, DataValue.BlockColor.Orange);
			Block blackConcrete = Block.Get(BlockID.Concrete, DataValue.BlockColor.Black);
			Block bottomCommandBlock = Block.Get(BlockID.CommandBlock).WithDataValue(1);
			Block topCommandBlock = Block.Get(BlockID.CommandBlock);
			Block smoothQuartzStairs = Block.Get(BlockID.Stairs.SmoothQuartz);
			Dictionary<int, Block> stairs = new Dictionary<int, Block>()
			{
				{ StairsEast,                   smoothQuartzStairs.WithDataValue(StairsEast                  ) },
				{ StairsWest,                   smoothQuartzStairs.WithDataValue(StairsWest                  ) },
				{ StairsSouth,                  smoothQuartzStairs.WithDataValue(StairsSouth                 ) },
				{ StairsNorth,                  smoothQuartzStairs.WithDataValue(StairsNorth                 ) },
				{ StairsEast  | StairsInverted, smoothQuartzStairs.WithDataValue(StairsEast  | StairsInverted) },
				{ StairsWest  | StairsInverted, smoothQuartzStairs.WithDataValue(StairsWest  | StairsInverted) },
				{ StairsSouth | StairsInverted, smoothQuartzStairs.WithDataValue(StairsSouth | StairsInverted) },
				{ StairsNorth | StairsInverted, smoothQuartzStairs.WithDataValue(StairsNorth | StairsInverted) }
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

				int upsideDownBit = platform.Layer1Offset < 0 ? StairsInverted : 0;
				int layer1Y = platform.Y + platform.Layer1Offset;

				yield return new SetBlockCommand(Position.Absolute(0, layer1Y, 0), platform.CommandBlock);

				foreach (var layer1 in new[]
				{
					new { From = Coord2.At( 0,  1), To = Coord2.At( 1,  1), Direction = StairsNorth },
					new { From = Coord2.At( 1,  0), To = Coord2.At( 1, -1), Direction = StairsWest  },
					new { From = Coord2.At( 1, -1), To = Coord2.At(-1, -1), Direction = StairsSouth },
					new { From = Coord2.At(-1,  0), To = Coord2.At(-1,  1), Direction = StairsEast  },
					new { From = Coord2.At(-1,  2), To = Coord2.At( 2,  2), Direction = StairsSouth },
					new { From = Coord2.At( 2,  1), To = Coord2.At( 2, -2), Direction = StairsEast  },
					new { From = Coord2.At( 1, -2), To = Coord2.At(-2, -2), Direction = StairsNorth },
					new { From = Coord2.At(-2, -1), To = Coord2.At(-2,  2), Direction = StairsWest  }
				})
				{
					yield return new FillCommand(layer1.From.AtY(layer1Y), layer1.To.AtY(layer1Y), stairs[layer1.Direction | upsideDownBit]);
				}

				//////////////////////////////////////////////////////////////////////////////
				// Outline

				int hereUpsideDownBit = platform.Layer1Offset < 0 ? 0 : StairsInverted;
				int awayUpsideDownBit = platform.Layer1Offset < 0 ? StairsInverted : 0;
				foreach (var outline in new[]
				{
					new { From = Coord2.At(-2,  3), To = Coord2.At( 3,  3), Direction = StairsNorth },
					new { From = Coord2.At( 3,  2), To = Coord2.At( 3, -3), Direction = StairsWest },
					new { From = Coord2.At( 2, -3), To = Coord2.At(-3, -3), Direction = StairsSouth },
					new { From = Coord2.At(-3, -2), To = Coord2.At(-3,  3), Direction = StairsEast }
				})
				{
					yield return new FillCommand(outline.From.AtY(platform.Y), outline.To.AtY(platform.Y), stairs[outline.Direction | hereUpsideDownBit]);
					yield return new FillCommand(outline.From.AtY(layer1Y), outline.To.AtY(layer1Y), stairs[outline.Direction | awayUpsideDownBit]);
				}
			}
		}
	}
}
