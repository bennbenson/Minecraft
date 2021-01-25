using System;
using System.ComponentModel;
using Minecraft.Model;

namespace Minecraft.Construction
{
	/// <summary>Represents the parameters that determine the size and shape of the warp zone structure.</summary>
	public sealed class WarpZoneParameters
	{
		private Coord2 _center = default;
		private int _yBase = 1;
		private int _radius = 8;
		private int _levels = 1;
		private int _levelHeight = 4;
		private int _interstitialHeight = 1;


		/// <summary>The center point of the warp zone.</summary>
		/// <remarks>Default = 0,0</remarks>
		public Coord2 Center
		{
			get => _center;
			set => _center = value;
		}

		/// <summary>The base Y level of the structure.</summary>
		[DefaultValue(1)]
		public int YBase
		{
			get => _yBase;
			set
			{
				if (value < 1)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(YBase)} cannot be less than 1.");
				if (value > 252)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(YBase)} cannot be greater than 252.");

				_yBase = value;
			}
		}

		/// <summary>The number of levels (floors) in the structure.</summary>
		[DefaultValue(1)]
		public int Levels
		{
			get => _levels;
			set
			{
				if (value < 1)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Levels)} cannot be less than 1.");
				if (value > 85)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Levels)} cannot be greater than 85.");

				_levels = value;
			}
		}

		/// <summary>Gets or sets the number of blocks from the center point that the </summary>
		[DefaultValue(8)]
		public int Radius
		{
			get => _radius;
			set
			{
				// With a radius of 90, each layer must be filled individually because each
				// layer would be just shy of the fill volume limit, at 32761 blocks.
				// At 45, it has a chance of being able to execute the interstitial and
				// interior fill commands, which have at least 3 layers each.
				// This is an arbitrary but considerably reasonable limit.  One should go
				// vertical for more room rather than use such a large radius.

				if (value < 5 || value > 45)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Radius)} cannot be less than 5 or greater than 45.");

				_radius = value;
			}
		}

		/// <summary>The height of the interior space.</summary>
		[DefaultValue(4)]
		public int InteriorHeight
		{
			get => _levelHeight;
			set
			{
				if (value is < 2 or > 10)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(InteriorHeight)} cannot be less than 2 or greater than 10.");

				_levelHeight = value;
			}
		}

		/// <summary>The height of the space between levels, which includes floor/ceiling space.</summary>
		/// <remarks>The interstitial height includes a floor layer and a ceiling layer.  The total interstitial space is <code><see cref="InterstitialHeight"/> + 2</code> blocks high.</remarks>
		[DefaultValue(1)]
		public int InterstitialHeight
		{
			get => _interstitialHeight;
			set
			{
				if (value is < -1 or > 10)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(InterstitialHeight)} cannot be less than 0 or greater than 10.");

				_interstitialHeight = value;
			}
		}

		/// <summary>Gets or sets and object that indicates what to do to the walls of each floor.</summary>
		[DefaultValue(null)]
		public WarpZoneWall? Walls { get; set; }

		/// <summary>Gets or sets a value indicating whether to echo a location to teleport in.</summary>
		[DefaultValue(false)]
		public bool TeleportIn { get; set; }
	}

	public abstract class WarpZoneWall
	{
		private int _buffer = 1;


		private protected WarpZoneWall()
		{
		}


		/// <summary>Gets or sets the number of blocks from the corners untouched when cutting into the walls and placing command blocks.</summary>
		[DefaultValue(1)]
		public int Buffer
		{
			get => _buffer;
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException($"{nameof(Buffer)} cannot be less than 0.");

				_buffer = value;
			}
		}
	}

	public sealed class CutSidesOnly : WarpZoneWall
	{
	}

	public sealed class PlaceCommandBlocks : WarpZoneWall
	{
	}
}
