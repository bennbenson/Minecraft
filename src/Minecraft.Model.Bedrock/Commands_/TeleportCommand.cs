using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TeleportCommand : Command
	{
		public TeleportCommand(TargetPosition destination, bool checkForBlocks = false)
			: this(null, destination, null, null, null, checkForBlocks)
		{
		}

		public TeleportCommand(TargetPosition destination, RelativeFloat? yRotation, RelativeFloat? xRotation = null, bool checkForBlocks = false)
			: this(null, destination, yRotation, xRotation, null, checkForBlocks)
		{
		}

		public TeleportCommand(TargetPosition destination, TargetPosition? facing, bool checkForBlocks = false)
			: this(null, destination, null, null, facing, checkForBlocks)
		{
		}

		public TeleportCommand(TargetPlayer? victim, TargetPosition destination, bool checkForBlocks = false)
			: this(victim, destination, null, null, null, checkForBlocks)
		{
		}

		public TeleportCommand(TargetPlayer? victim, TargetPosition destination, RelativeFloat? yRotation, RelativeFloat? xRotation = null, bool checkForBlocks = false)
			: this(victim, destination, yRotation, xRotation, null, checkForBlocks)
		{
		}

		public TeleportCommand(TargetPlayer? victim, TargetPosition destination, TargetPosition? facing, bool checkForBlocks = false)
			: this(victim, destination, null, null, facing, checkForBlocks)
		{
		}

		public TeleportCommand(TargetPlayer destination, bool checkForBlocks = false)
			: this(null, destination, null, null, null, checkForBlocks)
		{
		}

		public TeleportCommand(TargetPlayer? victim, TargetPlayer destination, bool checkForBlocks = false)
			: this(victim, destination, null, null, null, checkForBlocks)
		{
		}



		private TeleportCommand(TargetPlayer? victim, Target destination, RelativeFloat? yRotation, RelativeFloat? xRotation, Target? facing, bool checkForBlocks)
			: base("tp")
		{
			if (destination is null)
				throw new ArgumentNullException(nameof(destination));
			if (xRotation is not null && yRotation is null)
				yRotation = 0;
			if (destination is TargetPlayerSelector tps && tps.Type is PlayerSelectorType.All or PlayerSelectorType.Entity)
				throw new ArgumentOutOfRangeException(nameof(destination), "Invalid destination player selector.");

			Victim = victim;
			Destination = destination;
			YRotation = yRotation;
			XRotation = xRotation;
			Facing = facing;
			CheckForBlocks = checkForBlocks;
		}


		public TargetPlayer? Victim { get; }

		public Target Destination { get; }

		public Target? Facing { get; }

		public RelativeFloat? YRotation { get; }

		public RelativeFloat? XRotation { get; }

		public bool CheckForBlocks { get; }

		protected override Type EqualityContract => typeof(TeleportCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			StringBuilder result = new("/tp");

			if (Victim is not null)
				result.Append($" {Victim.GetArgumentText()}");

			result.Append($" {Destination.GetArgumentText()}");

			if (Facing is not null)
			{
				result.Append($" facing {Facing.GetArgumentText()}");
			}
			else if (YRotation is not null || XRotation is not null)
			{
				RelativeFloat yRotation = YRotation ?? default;
				result.Append($" {yRotation.GetArgumentText()}");

				if (XRotation is RelativeFloat xRotation)
					result.Append($" {xRotation.GetArgumentText()}");
			}

			if (CheckForBlocks)
				result.Append(" true");

			return result.ToString();
		}
	}
}
