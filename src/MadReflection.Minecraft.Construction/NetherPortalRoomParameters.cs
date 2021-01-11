using System;
using System.ComponentModel;
using Minecraft.Model;

namespace Minecraft.Construction
{
	public class NetherPortalRoomParameters
	{
		private Coord2 _center = default;
		private int _yBase = 1;
		private Direction _direction = Direction.South;


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

		[DefaultValue(Direction.South)]
		public Direction Direction
		{
			get => _direction;
			set
			{
				if (value < 0 || value > Direction.East)
					throw new ArgumentOutOfRangeException(nameof(value), "Direction is not a valid Direction value.");

				_direction = value;
			}
		}

		[DefaultValue(false)]
		public bool ExtraHigh { get; set; }

		[DefaultValue(false)]
		public bool ExtraWide { get; set; }
	}
}
