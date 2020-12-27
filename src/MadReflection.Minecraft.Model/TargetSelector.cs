using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	//[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	//public class TargetSelector : IEquatable<TargetSelector>
	//{
	//	private readonly string? _player;
	//	private readonly Position? _position;


	//	public TargetSelector(string player)
	//	{
	//		_player = player;
	//		_position = null;
	//	}

	//	public TargetSelector(Position position)
	//	{
	//		_player = null;
	//		_position = position;
	//	}


	//	public bool IsPlayer => _player is not null;

	//	public bool IsPosition => _position is not null;

	//	private string DebuggerDisplay => ToString();

	//	public string ArgumentText => _player is not null ? _player.ToString() : $"{_position?.ArgumentText}";


	//	#region Object members
	//	public override int GetHashCode() => _player is not null ? _player.GetHashCode() : _position.GetHashCode();

	//	public override bool Equals(object? obj) => obj is TargetSelector other && Equals(other);

	//	public override string ToString() => _player is not null ? _player.ToString() : _position?.ToString() ?? "";
	//	#endregion


	//	#region IEquatable<TeleportTarget> members
	//	public bool Equals(TargetSelector? other)
	//	{
	//		if (other is null)
	//			return false;

	//		if (_player is not null && other._player is not null)
	//			return _player.Equals(other._player);

	//		if (_position is not null && other._position is not null)
	//			return _position.Equals(other._position);

	//		return false;
	//	}
	//	#endregion
	//}
}
