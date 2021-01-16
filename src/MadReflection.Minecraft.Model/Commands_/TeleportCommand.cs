using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TeleportCommand : Command
	{
		public TeleportCommand(Target destination)
			: this(null, destination, null, null, null, default, false)
		{
		}

		public TeleportCommand(TargetPlayer? victim, Target destination)
			: this(victim, destination, null, null, null, default, false)
		{
			Victim = victim;
			Destination = destination;
		}

		public TeleportCommand(TargetPlayer? victim, Target destination, RelativeFloat? yRotation = null, RelativeFloat? xRotation = null)
			: this(victim, destination, yRotation, xRotation, null, default, false)
		{
		}

		public TeleportCommand(TargetPlayer? victim, Target destination, RelativeFloat? yRotation = null, RelativeFloat? xRotation = null, bool checkForBlocks = false)
			: this(victim, destination, yRotation, xRotation, null, default, false)
		{
		}

		public TeleportCommand(TargetPlayer? victim, Target destination, Target? facing, bool checkForBlocks = false)
			: this(victim, destination, null, null, facing, default, false)
		{
		}

		public TeleportCommand(TargetPlayer? victim, Target destination, Target? facing, FacingAnchor anchor)
			: this(victim, destination, null, null, facing, anchor, false)
		{
		}

		private TeleportCommand(TargetPlayer? victim, Target destination, RelativeFloat? yRotation, RelativeFloat? xRotation, Target? facing, FacingAnchor anchor, bool checkForBlocks)
			: base("tp")
		{
			Victim = victim;
			Destination = destination;
			Facing = facing;
			YRotation = yRotation;
			XRotation = xRotation;
			Anchor = anchor;
			CheckForBlocks = checkForBlocks;
		}


		public TargetPlayer? Victim { get; }

		public Target Destination { get; }

		public Target? Facing { get; }

		public RelativeFloat? YRotation { get; }

		public RelativeFloat? XRotation { get; }

		public FacingAnchor? Anchor { get; }

		public bool CheckForBlocks { get; }

		protected override Type EqualityContract => typeof(TeleportCommand);

		private string DebuggerDisplay => ToString();


		protected override string GetCommandTextImpl(Edition edition)
		{
			string result = $"/tp";

			if (Victim is not null)
				result += " " + Victim.GetArgumentText(edition);

			result += " " + Destination.GetArgumentText(edition);

			if (Facing is Target facing)
				result += " facing " + facing.GetArgumentText(edition);

			return result;
		}
	}
}
