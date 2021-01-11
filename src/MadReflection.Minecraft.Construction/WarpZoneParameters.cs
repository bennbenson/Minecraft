using System;
using System.ComponentModel;
using Minecraft.Model;

namespace Minecraft.Construction
{
	public class WarpZoneParameters
	{
		private Coord2 _center = default;
		private int _yBase = 1;
		private int _radius = 5;
		private int _levels = 1;
		private int _levelHeight = 4;
		private int _interstitialHeight = 1;


		/// <summary>
		/// The center point of the warp zone.  Default = 0,0
		/// </summary>
		public Coord2 Center
		{
			get => _center;
			set => _center = value;
		}

		[DefaultValue(1)]
		public int YBase
		{
			get => _yBase;
			set
			{
				if (_yBase < 0)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(YBase)} cannot be less than 1.");

				_yBase = value;
			}
		}

		[DefaultValue(1)]
		public int Levels
		{
			get => _levels;
			set
			{
				if (value < 1)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Levels)} cannot be less than 1.");

				_levels = value;
			}
		}

		[DefaultValue(5)]
		public int Radius
		{
			get => _radius;
			set
			{
				// With a radius of 90, each layer must be filled individually because each
				// layer would be just shy of the limit at 32761 blocks.
				// At 45, it has a chance of being able to execute the interstitial and
				// interior fill commands, which have at least 3 layers each.
				// This is an arbitrary but considerably reasonable limit.  One should go
				// vertical for more room rather than use such a large radius.

				if (value < 5 || value > 45)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Radius)} cannot be less than 5 or greater than 45.");

				_radius = value;
			}
		}

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

		[DefaultValue(1)]
		public int InterstitialHeight
		{
			get => _interstitialHeight;
			set
			{
				if (value is < 0 or > 10)
					throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(InterstitialHeight)} cannot be less than 0 or greater than 10.");

				_interstitialHeight = value;
			}
		}

		[DefaultValue(false)]
		public bool CutSides { get; set; }

		[DefaultValue(false)]
		public bool TeleportIn { get; set; }
	}
}
