using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class TeleportCommand : Command
	{
		public TeleportCommand(TargetEntity destination)
			: this(null, destination, null, null, null)
		{
		}

		public TeleportCommand(TargetPosition location)
			: this(null, location, null, null, null)
		{
		}

		public TeleportCommand(TargetPlayer? targets, TargetEntity destination)
			: this(targets, destination, null, null, null)
		{
		}

		public TeleportCommand(TargetPlayer? targets, TargetPosition location, Rotation? rotation = null)
			: this(targets, location, rotation, null, null)
		{
		}

		public TeleportCommand(TargetPlayer? targets, TargetPosition location, TargetPosition facingLocation)
			: this(targets, location, null, facingLocation, null)
		{
		}

		public TeleportCommand(TargetPlayer? targets, TargetPosition location, TargetEntity facingEntity, FacingAnchor? anchor = null)
			: this(targets, location, null, facingEntity, anchor)
		{
		}

		private TeleportCommand(TargetPlayer? targets, Target destination, Rotation? rotation, Target? facing, FacingAnchor? anchor)
			: base("tp")
		{
			if (anchor < FacingAnchor.Eyes || anchor > FacingAnchor.Feet)
				throw new ArgumentOutOfRangeException(nameof(anchor), "Invalid FacingAnchor value.");

			Targets = targets;
			Destination = destination;
			Rotation = rotation;
			Facing = facing;
			Anchor = anchor;
		}


		public TargetPlayer? Targets { get; }

		public Target Destination { get; }

		public Target? Facing { get; }

		public Rotation? Rotation { get; }

		public FacingAnchor? Anchor { get; }

		protected override Type EqualityContract => typeof(TeleportCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			StringBuilder result = new("/tp");

			if (Targets is not null)
				result.Append($" {Targets.GetArgumentText()}");

			result.Append($" {Destination.GetArgumentText()}");

			if (Rotation is Rotation rotation)
			{
				result.Append($" {rotation.Y.GetArgumentText()} {rotation.X.GetArgumentText()}");
			}
			else if (Facing is Target facing)
			{
				result.Append(" facing");
				if (Facing is TargetEntity)
					result.Append(" entity");
				result.Append($" {facing.GetArgumentText()}");

				if (Anchor is FacingAnchor facingAnchor)
					result.Append($" {facingAnchor.GetArgumentText()}");
			}

			return result.ToString();
		}
	}
}
