using System;
using System.Collections.Generic;
using Minecraft.Model;
using Minecraft.Model.Bedrock;

namespace Minecraft.Construction.Bedrock
{
	public static class SlidingGlassDoor
	{
		public static readonly Block OuterGlass = Block.Get(BlockID.StainedGlassPane, BlockColor.Gray);
		public static readonly Block InnerGlass = Block.Get(BlockID.StainedGlassPane, BlockColor.White);
		public static readonly Block Air = Block.Air;


		public static IEnumerable<CommandBlock> Generate((int start, int end) x, int z, int y, int height)
		{
			//yield return new CommandBlock(new TestForCommand(), CommandBlockType.Repeat, true, false);

			throw new NotImplementedException();
		}

		public static IEnumerable<CommandBlock> Generate(int x, (int start, int end) z, int y, int height)
		{
			throw new NotImplementedException();
		}






		public static IEnumerable<CommandBlock> Calculate4Wide3High(Coord2 left, Coord2 right, int y)
		{
			if (y < 0)
				throw new ArgumentOutOfRangeException(nameof(y), "Y must be non-negative.");

			if (left.X == right.X)
			{
				if (Math.Abs(left.X - right.X) != 3)
					return Calculate4Wide3HighByZ(left.X, y, (left.Z, right.Z));

				throw new ArgumentException("X difference must be 3 (for a 4-wide door).");
			}

			if (left.Z == right.Z)
			{
				if (Math.Abs(left.Z - right.Z) != 3)
					return Calculate4Wide3HighByX((left.X, right.X), y, left.Z);

				throw new ArgumentException("Z difference must be 3 (for a 4-wide door).");
			}

			throw new ArgumentException("X or Z must be the same.  Diagonal inputs are not supported.");
		}

		private static IEnumerable<CommandBlock> Calculate4Wide3HighByX((int left, int right) x, int y, int z/*, int height = 3*/)
		{
			int lowY = y;
			int highY = y + 2;

			(int left, int right) outerX;
			(int left, int right) innerX;
			if (x.left > x.right)
			{
				outerX = (x.left, x.right);
				innerX = (x.left - 1, x.right + 1);
			}
			else
			{
				outerX = (x.right, x.left);
				innerX = (x.right - 1, x.left + 1);
			}

			yield return new CommandBlock(new FillCommand(new Coord3(innerX.left, lowY, z), new Coord3(innerX.right, highY, z), Block.Air, FillMode.Replace));
			yield return new CommandBlock(new FillCommand(new Coord3(outerX.left, lowY, z), new Coord3(outerX.right, highY, z), OuterGlass, FillMode.Replace.With(InnerGlass)));
			yield return new CommandBlock(new FillCommand(new Coord3(outerX.left, lowY, z), new Coord3(outerX.right, highY, z), InnerGlass, FillMode.Replace.With(OuterGlass)));
			yield return new CommandBlock(new FillCommand(new Coord3(innerX.left, lowY, z), new Coord3(innerX.right, highY, z), InnerGlass, FillMode.Replace));
		}

		private static IEnumerable<CommandBlock> Calculate4Wide3HighByZ(int x, int y, (int left, int right) z/*, int height = 3*/)
		{
			int lowY = y;
			int highY = y + 2;

			(int left, int right) outerZ;
			(int left, int right) innerZ;
			if (z.left > z.right)
			{
				outerZ = (z.left, z.right);
				innerZ = (z.left - 1, z.right + 1);
			}
			else
			{
				outerZ = (z.left, z.right);
				innerZ = (z.left + 1, z.right - 1);
			}

			yield return new CommandBlock(new FillCommand(new Coord3(x, lowY, innerZ.left), new Coord3(x, highY, innerZ.right), Block.Air, FillMode.Replace));
			yield return new CommandBlock(new FillCommand(new Coord3(x, lowY, outerZ.left), new Coord3(x, highY, outerZ.right), OuterGlass, FillMode.Replace.With(InnerGlass)));
			yield return new CommandBlock(new FillCommand(new Coord3(x, lowY, outerZ.left), new Coord3(x, highY, outerZ.right), InnerGlass, FillMode.Replace.With(OuterGlass)));
			yield return new CommandBlock(new FillCommand(new Coord3(x, lowY, innerZ.left), new Coord3(x, highY, innerZ.right), InnerGlass, FillMode.Replace));
		}
	}
}
