using System;
using System.ComponentModel;
using Minecraft.Model;

namespace Minecraft.Construction
{
	public class DuckBlindParameters
	{
		private Coord2 _center = default;
		private int _yBase = 1;


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
	}
}
