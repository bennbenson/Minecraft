using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class CommandBlock : Block
	{
		public CommandBlock(Command command)
			: this(GetCommandBlockID(CommandBlockType.Impulse), command, CommandBlockType.Impulse, false, true)
		{
		}

		public CommandBlock(Command command, CommandBlockType type, bool alwaysActive, bool requiresRedstone)
			: this(GetCommandBlockID(type), command, type, alwaysActive, requiresRedstone)
		{
		}

		private CommandBlock(string id, Command command, CommandBlockType type, bool alwaysActive, bool requiresRedstone)
			: base(id, id, 0)
		{
			if (command is null)
				throw new ArgumentNullException(nameof(command));

			Command = command;
			Type = type;
			AlwaysActive = alwaysActive;
			RequiresRedstone = requiresRedstone;
		}


		public Command Command { get; }

		public CommandBlockType Type { get; }

		public bool AlwaysActive { get; }

		public bool RequiresRedstone { get; }

		protected override Type EqualityContract => typeof(CommandBlock);

		private string DebuggerDisplay => ToString();


		private static string GetCommandBlockID(CommandBlockType type)
		{
			return type switch
			{
				CommandBlockType.Repeating => "repeating_command_block",
				CommandBlockType.Chain => "chain_command_block",
				_ => "command_block"
			};
		}


		#region Object members
		public override int GetHashCode() => HashCode.Combine(Command, Type, AlwaysActive, RequiresRedstone);

		public override bool Equals(object? obj) => obj is CommandBlock other && Equals(other);

		public override string ToString() => $"{Command}; Type={Type}, AlwaysActive={AlwaysActive}, RequiresRedstone={RequiresRedstone}";
		#endregion


		#region IEquatable<CommandBlock> members
		public bool Equals(CommandBlock other) => other is not null && Command == other.Command && Type == other.Type && AlwaysActive == other.AlwaysActive && RequiresRedstone == other.RequiresRedstone;
		#endregion
	}
}
