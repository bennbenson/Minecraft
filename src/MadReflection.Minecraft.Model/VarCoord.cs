using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public readonly struct VarCoord : IEquatable<VarCoord>
	{
		private readonly int? _x;
		private readonly int? _y;
		private readonly int? _z;


		public VarCoord(int? x, int? y, int? z)
		{
			_x = x;
			_y = y;
			_z = z;
		}


		public int? X => _x;

		public int? Y => _y;

		public int? Z => _z;


		public VarCoord WithX(int? x) => new VarCoord(x, _y, _z);

		public VarCoord WithY(int? y) => new VarCoord(_x, y, _z);

		public VarCoord WithZ(int? z) => new VarCoord(_x, _y, z);

		private string DebuggerDisplay => ToString();

		public static VarCoord Parse(string s) => InternalTryParse(s, out VarCoord result, out Exception exception, true) ? result : throw exception;

		public static bool TryParse(string s, out VarCoord result) => InternalTryParse(s, out result, out _, false);

		public static bool InternalTryParse(string s, out VarCoord result, out Exception exception, bool needException)
		{
			Match match = Regex.Match(s,@"^\((?<x>-?[0-9]+|\*),(?<y>-?[0-9]+|\*),(?<z>-?[0-9]+|\*)\)$");
			if (match.Success)
			{
				var xValue = match.Groups["x"].Value;
				int? x = xValue == "*" ? null : int.Parse(xValue);

				var yValue = match.Groups["y"].Value;
				int? y = yValue == "*" ? null : int.Parse(yValue);

				var zValue = match.Groups["z"].Value;
				int? z = zValue == "*" ? null : int.Parse(zValue);

				result = new VarCoord(x, y, z);
				exception = null;
				return true;
			}

			result = default;
			exception = needException ? new FormatException() : null;
			return false;
		}


		public static bool operator ==(VarCoord a, VarCoord b) => a.Equals(b);

		public static bool operator !=(VarCoord a, VarCoord b) => !a.Equals(b);


		#region Object members
		public override int GetHashCode() => (_x ?? 0) ^ ((_y ?? 0) << 8) ^ ((_z ?? 0) << 16);

		public override bool Equals(object obj) => obj is VarCoord other && Equals(other);

		public override string ToString() => $"({X?.ToString() ?? "*"},{Y?.ToString() ?? "*"},{Z?.ToString() ?? "*"})";
		#endregion


		#region IEquatable<VarCoord> members
		public bool Equals(VarCoord other) => _x == other._x && _y == other._y && _z == other._z;
		#endregion
	}
}
