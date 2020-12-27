using System;
using System.Diagnostics;

namespace Minecraft.Model.Data
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	internal readonly struct BedrockIdentifier : IEquatable<BedrockIdentifier>
	{
		private readonly string _id;
		private readonly int _dv;


		public BedrockIdentifier(string id, int dv = 0)
		{
			_id = id;
			_dv = dv;
		}


		public string ID
		{
			get => _id ?? "";
			init => _id = value ?? "";
		}

		public int DV
		{
			get => _dv;
			init
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException(nameof(value), "DV cannot be negative.");

				_dv = value;
			}
		}

		private string DebuggerDisplay => ToString();


		public void Deconstruct(out string id, out int dv)
		{
			id = _id;
			dv = _dv;
		}


		public static bool operator ==(BedrockIdentifier left, BedrockIdentifier right) => left.Equals(right);
		public static bool operator !=(BedrockIdentifier left, BedrockIdentifier right) => !left.Equals(right);

		public static implicit operator BedrockIdentifier(string id) => new BedrockIdentifier(id, 0);
		public static implicit operator BedrockIdentifier((string id, int dv) value) => new BedrockIdentifier(value.id, value.dv);
		public static explicit operator (string id, int dv)(BedrockIdentifier identifier) => (identifier.ID, identifier.DV);


		#region Object members
		public override int GetHashCode() => HashCode.Combine(ID, DV);

		public override bool Equals(object? obj) => obj is BedrockIdentifier other && Equals(other);

		public override string ToString() => $"{ID} {DV}";
		#endregion


		#region IEquatable<BedrockIdentifier> members
		public bool Equals(BedrockIdentifier other) => ID == other.ID && DV == other.DV;
		#endregion
	}
}
