using System;
using Minecraft.Model;

namespace Minecraft.Construction
{
	/// <summary>Utilities related to volume calculation.</summary>
	public static class Volume
	{
		/// <summary>Counts the blocks in the specified volume.</summary>
		/// <param name="from">The first corner of the volume.</param>
		/// <param name="to">The opposite corner of the volume.</param>
		/// <returns>The number of blocks in the volume.</returns>
		public static int CountBlocks(Coord3 from, Coord3 to)
		{
			int dx = Math.Abs(from.X - to.X) + 1;
			int dy = Math.Abs(from.Y - to.Y) + 1;
			int dz = Math.Abs(from.Z - to.Z) + 1;
			return dx * dy * dz;
		}

		/// <summary>Determines if the "fill" command can fill the specified volume.</summary>
		/// <param name="from">The first corner of the volume.</param>
		/// <param name="to">The opposite corner of the volume.</param>
		/// <returns><c>true</c> if the volume contains 32768 blocks of fewer; <c>false</c> otherwise.</returns>
		public static bool CanFill(Coord3 from, Coord3 to)
		{
			const int maxVolume = short.MaxValue + 1;
			return CountBlocks(from, to) > maxVolume;
		}
	}
}
