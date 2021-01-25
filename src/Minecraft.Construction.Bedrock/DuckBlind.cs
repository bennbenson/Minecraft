using System.Collections.Generic;
using Minecraft.Model;
using Minecraft.Model.Bedrock;

namespace Minecraft.Construction.Bedrock
{
	public static class DuckBlind
	{
		private const int InvertedStairs = 4;

		public static IEnumerable<Command> Generate()
		{
			Block chiseledStoneBrick = Block.Get(BlockID.StoneBrick, StoneBrick.Chiseled);
			Block redConcrete = Block.Get(BlockID.Concrete, BlockColor.Red);
			Block limeConcrete = Block.Get(BlockID.Concrete, BlockColor.Lime);
			Block blueConcrete = Block.Get(BlockID.Concrete, BlockColor.Blue);
			Block orangeConcrete = Block.Get(BlockID.Concrete, BlockColor.Orange);
			Block blackConcrete = Block.Get(BlockID.Concrete, BlockColor.Black);
			Block bottomCommandBlock = Block.Get(BlockID.CommandBlock).WithDataValue(1);
			Block topCommandBlock = Block.Get(BlockID.CommandBlock);
			Block smoothQuartzStairs = Block.Get(BlockID.SmoothQuartzStairs);
			Dictionary<int, Block> stairs = new Dictionary<int, Block>()
			{
				{ Cardinal.East,                   smoothQuartzStairs.WithDataValue(Cardinal.East                  ) },
				{ Cardinal.West,                   smoothQuartzStairs.WithDataValue(Cardinal.West                  ) },
				{ Cardinal.South,                  smoothQuartzStairs.WithDataValue(Cardinal.South                 ) },
				{ Cardinal.North,                  smoothQuartzStairs.WithDataValue(Cardinal.North                 ) },
				{ Cardinal.East  | InvertedStairs, smoothQuartzStairs.WithDataValue(Cardinal.East  | InvertedStairs) },
				{ Cardinal.West  | InvertedStairs, smoothQuartzStairs.WithDataValue(Cardinal.West  | InvertedStairs) },
				{ Cardinal.South | InvertedStairs, smoothQuartzStairs.WithDataValue(Cardinal.South | InvertedStairs) },
				{ Cardinal.North | InvertedStairs, smoothQuartzStairs.WithDataValue(Cardinal.North | InvertedStairs) }
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

				int upsideDownBit = platform.Layer1Offset < 0 ? InvertedStairs : 0;
				int layer1Y = platform.Y + platform.Layer1Offset;

				yield return new SetBlockCommand(Position.Absolute(0, layer1Y, 0), platform.CommandBlock);

				foreach (var layer1 in new[]
				{
					new { From = Coord2.At( 0,  1), To = Coord2.At( 1,  1), Direction = Cardinal.North },
					new { From = Coord2.At( 1,  0), To = Coord2.At( 1, -1), Direction = Cardinal.West  },
					new { From = Coord2.At( 1, -1), To = Coord2.At(-1, -1), Direction = Cardinal.South },
					new { From = Coord2.At(-1,  0), To = Coord2.At(-1,  1), Direction = Cardinal.East  },
					new { From = Coord2.At(-1,  2), To = Coord2.At( 2,  2), Direction = Cardinal.South },
					new { From = Coord2.At( 2,  1), To = Coord2.At( 2, -2), Direction = Cardinal.East  },
					new { From = Coord2.At( 1, -2), To = Coord2.At(-2, -2), Direction = Cardinal.North },
					new { From = Coord2.At(-2, -1), To = Coord2.At(-2,  2), Direction = Cardinal.West  }
				})
				{
					yield return new FillCommand(layer1.From.AtY(layer1Y), layer1.To.AtY(layer1Y), stairs[layer1.Direction | upsideDownBit]);
				}

				//////////////////////////////////////////////////////////////////////////////
				// Outline

				int hereUpsideDownBit = platform.Layer1Offset < 0 ? 0 : InvertedStairs;
				int awayUpsideDownBit = platform.Layer1Offset < 0 ? InvertedStairs : 0;
				foreach (var outline in new[]
				{
					new { From = Coord2.At(-2,  3), To = Coord2.At( 3,  3), Direction = Cardinal.North },
					new { From = Coord2.At( 3,  2), To = Coord2.At( 3, -3), Direction = Cardinal.West },
					new { From = Coord2.At( 2, -3), To = Coord2.At(-3, -3), Direction = Cardinal.South },
					new { From = Coord2.At(-3, -2), To = Coord2.At(-3,  3), Direction = Cardinal.East }
				})
				{
					yield return new FillCommand(outline.From.AtY(platform.Y), outline.To.AtY(platform.Y), stairs[outline.Direction | hereUpsideDownBit]);
					yield return new FillCommand(outline.From.AtY(layer1Y), outline.To.AtY(layer1Y), stairs[outline.Direction | awayUpsideDownBit]);
				}
			}
		}
	}
}
