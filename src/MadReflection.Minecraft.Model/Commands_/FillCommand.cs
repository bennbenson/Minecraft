using System;

namespace Minecraft.Model
{
	public class FillCommand : Command
	{
		public FillCommand(Coord3 coord1, Coord3 coord2, Block block)
			: base("fill")
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			Coord1 = coord1;
			Coord2 = coord2;
			Block = block;
			FillMode = FillMode.Replace;
		}

		public FillCommand(Coord3 coord1, Coord3 coord2, Block block, FillMode fillMode)
			: base("fill")
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));
			if (fillMode is null)
				throw new ArgumentNullException(nameof(fillMode));

			Coord1 = coord1;
			Coord2 = coord2;
			Block = block;
			FillMode = fillMode;
		}


		public Coord3 Coord1 { get; }

		public Coord3 Coord2 { get; }

		public Block Block { get; }

		public FillMode FillMode { get; init; }

		public override string CommandText
		{
			get
			{
				string result = $"/{Name} {Coord1.ArgumentText} {Coord2.ArgumentText} {Block.ID}";

				if (Block.Data > 0 || FillMode is not ReplaceFillMode rfm || !rfm.Block.IsUnspecified)
				{
					result += $" {Block.Data}";

					if (FillMode is ReplaceFillMode replaceFillMode)
					{
						if (!replaceFillMode.Block.IsUnspecified)
							result += " " + replaceFillMode.ArgumentText;
					}
					else
					{
						result += " " + FillMode.ArgumentText;
					}
				}

				return result;
			}
		}


		public override string ToString()
		{
			// This should be revisited.
			return "/fill";
		}
	}
}
