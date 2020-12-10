using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TeleportTarget : IEquatable<TeleportTarget>
	{
		private readonly string _player;
		private readonly Coord3? _coord;


		public TeleportTarget(string player)
		{
			_player = player;
			_coord = null;
		}

		public TeleportTarget(Coord3 coord)
		{
			_player = null;
			_coord = coord;
		}


		public bool IsPlayer => _player is not null;

		public bool IsCoord => _coord is not null;


		private string DebuggerDisplay => ToString();

		public string ArgumentText => _player is not null ? _player.ToString() : $"{_coord?.ArgumentText}";


		#region Object members
		public override int GetHashCode() => _player is not null ? _player.GetHashCode() : _coord.GetHashCode();

		public override bool Equals(object obj) => obj is TeleportTarget other && Equals(other);

		public override string ToString() => _player is not null ? _player.ToString() : _coord?.ToString();
		#endregion


		#region IEquatable<TeleportTarget> members
		public bool Equals(TeleportTarget other)
		{
			if (_player is not null && other._player is not null)
				return _player.Equals(other._player);

			if (_coord is not null && other._coord is not null)
				return _coord.Equals(other._coord);

			return false;
		}
		#endregion
	}
}
