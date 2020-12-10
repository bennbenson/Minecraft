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
					throw new ArgumentOutOfRangeException(nameof(value), "YBase cannot be less than 1.");

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
					throw new ArgumentOutOfRangeException(nameof(value), "Levels cannot be less than 1.");

				_levels = value;
			}
		}

		[DefaultValue(5)]
		public int Radius
		{
			get => _radius;
			set
			{
				if (value < 5)
					throw new ArgumentOutOfRangeException(nameof(value), "Radius cannot be less than 5.");

				_radius = value;
			}
		}

		[DefaultValue(4)]
		public int LevelHeight
		{
			get => _levelHeight;
			set
			{
				if (value is < 2 or > 10)
					throw new ArgumentOutOfRangeException(nameof(value), "LevelHeight cannot be less than 2 or greater than 10.");

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
					throw new ArgumentOutOfRangeException(nameof(value), "InterstitialHeight cannot be less than 0 or greater than 10.");

				_interstitialHeight = value;
			}
		}

		[DefaultValue(false)]
		public bool CutSides { get; set; }

		[DefaultValue(false)]
		public bool TeleportIn { get; set; }
	}
}
