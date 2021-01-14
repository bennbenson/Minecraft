using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model
{
	public abstract class TargetEntity : IEquatable<TargetEntity>
	{
		protected abstract Type EqualityContract { get; }


		#region Object members
		public override int GetHashCode() => 0;

		public override bool Equals(object? obj) => obj is TargetEntity other && Equals(other);
		#endregion


		#region IEquatable<Entity> members
		public bool Equals(TargetEntity? other) => other is not null && EqualityContract == other.EqualityContract;
		#endregion
	}

	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TargetPlayer : TargetEntity, IEquatable<TargetPlayer>
	{
		public TargetPlayer(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (name is "")
				throw new ArgumentException("Name cannot be empty", nameof(name));

			Name = name;
		}


		public string Name { get; }

		protected override Type EqualityContract => typeof(TargetPlayer);

		private string DebuggerDisplay => ToString();


		#region Object members
		public override int GetHashCode() => Name.GetHashCode();

		public override bool Equals(object? obj) => obj is TargetPlayer other && Equals(other);

		public override string ToString() => Name;
		#endregion


		#region IEquatable<Player> members
		public bool Equals(TargetPlayer? other) => other is not null && EqualityContract == other.EqualityContract && Name == other.Name;
		#endregion
	}

	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TargetSelector : TargetEntity, IEquatable<TargetSelector>
	{
		public TargetSelector(char t)
		{
			if (t is not 'a' and not 'e' and not 'p' and not 'r' and not 's')
				throw new ArgumentOutOfRangeException(nameof(t), "Invalid selector character.");

			T = t;
		}


		public char T { get; }

		protected override Type EqualityContract => typeof(TargetSelector);

		private string DebuggerDisplay => $"@{T}";


		#region Object members
		public override int GetHashCode() => T.GetHashCode();

		public override bool Equals(object? obj) => obj is TargetSelector other && Equals(other);

		public override string ToString()
		{
			//if (conditions.Any())
			//    return $"@{T}[{string.Join(",", conditions.Select(x=>$"{x.Name}={x.Value}"))}]"

			return $"@{T}";
		}
		#endregion


		#region IEquatable<TargetSelector> members
		public bool Equals(TargetSelector? other) => other is not null && EqualityContract == other.EqualityContract && T == other.T;
		#endregion
	}

	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TargetLocation : TargetEntity, IEquatable<TargetLocation>
	{
		public TargetLocation(Position position)
		{
			X = position.X;
			Y = position.Y;
			Z = position.Z;
		}


		public PositionValue X { get; }

		public PositionValue Y { get; }

		public PositionValue Z { get; }

		protected override Type EqualityContract => typeof(TargetLocation);

		private string DebuggerDisplay => ToString();


		#region Object members
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);

		public override bool Equals(object? obj) => obj is TargetLocation other && Equals(other);

		public override string ToString() => $"{X} {Y} {Z}";
		#endregion


		#region IEquatable<TargetLocation> members
		public bool Equals(TargetLocation? other) => other is not null && EqualityContract == other.EqualityContract && X == other.X && Y == other.Y && Z == other.Z;
		#endregion
	}



	public abstract class VictimEntity : IEquatable<VictimEntity>
	{
		public static VictimSelector All { get; } = new VictimSelector('s');
		public static VictimSelector Self { get; } = new VictimSelector('a');


		protected abstract Type EqualityContract { get; }


		#region Object members
		public override int GetHashCode() => 0;

		public override bool Equals(object? obj) => obj is VictimEntity other && Equals(other);
		#endregion


		#region IEquatable<VictimEntity> members
		public bool Equals(VictimEntity? other) => other is not null && EqualityContract == other.EqualityContract;
		#endregion
	}

	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public sealed class VictimPlayer : VictimEntity, IEquatable<VictimPlayer>
	{
		public VictimPlayer(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (name is "")
				throw new ArgumentException("Name cannot be empty", nameof(name));

			Name = name;
		}


		public string Name { get; }

		private string DebuggerDisplay => ToString();

		protected override Type EqualityContract => typeof(VictimPlayer);


		#region Object members
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			return Name.Equals(obj);
		}

		public override string ToString()
		{
			return Name.ToString();
		}
		#endregion


		#region IEquatable<VictimPlayer> members
		public bool Equals(VictimPlayer? other) => other is not null && EqualityContract == other.EqualityContract && Name == other.Name;
		#endregion
	}

	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public sealed class VictimSelector : VictimEntity, IEquatable<VictimSelector>
	{
		public VictimSelector(char t)
		{
			if (t is not 'a' and not 'e' and not 'p' and not 'r' and not 's')
				throw new ArgumentOutOfRangeException(nameof(t), "Invalid selector character.");

			T = t;
		}


		public char T { get; }

		private string DebuggerDisplay => ToString();

		protected override Type EqualityContract => typeof(VictimSelector);


		#region Object members
		public override int GetHashCode() => T.GetHashCode();

		public override bool Equals(object? obj) => obj is VictimSelector other && Equals(other);

		public override string ToString() => $"@{T}";
		#endregion


		#region IEquatable<VictimSelector> members
		public bool Equals(VictimSelector? other)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
