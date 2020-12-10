using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class CommandBlock : Block
	{
		public CommandBlock(Command command)
			: this(command, CommandBlockType.Impulse, false, true)
		{
		}

		public CommandBlock(Command command, CommandBlockType type, bool alwaysActive, bool requiresRedstone)
			: base("command_block", 0)
		{
			if (command is null)
				throw new ArgumentNullException(nameof(command));

			Command = command;
			Type = type;
			AlwaysActive = alwaysActive;
			RequiresRedstone = requiresRedstone;
		}


		public Command Command { get; }

		public CommandBlockType Type { get; init; }

		public bool AlwaysActive { get; init; }

		public bool RequiresRedstone { get; init; }


		private string DebuggerDisplay => ToString();


		#region Object members
		//public override int GetHashCode() => base.GetHashCode();

		//public override bool Equals(object obj) => base.Equals(obj);

		public override string ToString() => $"{Command}; Type={Type}, AlwaysActive={AlwaysActive}, RequiresRedstone={RequiresRedstone}";
		#endregion
	}
}
