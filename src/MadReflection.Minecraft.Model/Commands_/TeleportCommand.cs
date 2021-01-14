using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TeleportCommand : Command
	{
		public TeleportCommand(TargetEntity target)
			: this(VictimEntity.Self, target, null)
		{
		}

		public TeleportCommand(TargetEntity target, TargetEntity? facing)
			: this(VictimEntity.Self, target, facing)
		{
		}

		public TeleportCommand(VictimEntity victim, TargetEntity target)
			: this(victim, target, null)
		{
		}

		public TeleportCommand(VictimEntity victim, TargetEntity target, TargetEntity? facing)
			: base("tp")
		{
			if (victim is null)
				throw new ArgumentNullException(nameof(victim));
			if (target is null)
				throw new ArgumentNullException(nameof(target));

			Victim = victim;
			Target = target;
			Facing = facing;
		}


		public VictimEntity Victim { get; }

		public TargetEntity Target { get; }

		public TargetEntity? Facing { get; }

		protected override Type EqualityContract => typeof(TeleportCommand);

		private string DebuggerDisplay => ToString();


		protected override string GetCommandTextImpl(Edition edition)
		{
			string result = $"/{Name} ";

			if (Victim is not null)
				result += " " + Victim.ToString();

			result += " " + Target.ToString();

			if (Facing is TargetEntity facing)
				result += " " + facing.ToString();

			return result;
		}
	}
}
