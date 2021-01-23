using System;
using System.Diagnostics;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class CommandBlock : Block
	{
		public CommandBlock(Command command)
			: this(0, command)
		{
		}

		public CommandBlock(int dataValue, Command command)
			: this(GetCommandBlockID(CommandBlockType.Impulse), dataValue, command, CommandBlockType.Impulse, "", false, true, false)
		{
		}

		public CommandBlock(int dataValue, Command command, CommandBlockType type)
			: this(GetCommandBlockID(type), dataValue, command, type, "", false, false, false)
		{
		}

		public CommandBlock(int dataValue, Command command, CommandBlockType type, string hover, bool alwaysActive, bool requiresRedstone, bool executeOnFirstTick)
			: this(GetCommandBlockID(type), dataValue, command, type, hover, alwaysActive, requiresRedstone, executeOnFirstTick)
		{
		}

		private CommandBlock(string id, int dataValue, Command command, CommandBlockType type, string hover, bool alwaysActive, bool requiresRedstone, bool executeOnFirstTick)
			: base(id, dataValue)
		{
			if (command is null)
				throw new ArgumentNullException(nameof(command));

			Command = command;
			Type = type;
			Hover = hover ?? "";
			AlwaysActive = alwaysActive;
			RequiresRedstone = requiresRedstone;
			ExecuteOnFirstTick = executeOnFirstTick;
		}


		public Command Command { get; }

		public CommandBlockType Type { get; }

		public string Hover { get; }

		public bool AlwaysActive { get; }

		public bool RequiresRedstone { get; }

		public bool ExecuteOnFirstTick { get; }

		protected override Type EqualityContract => typeof(CommandBlock);

		private string DebuggerDisplay => ToString();


		public CommandBlock With(int? dataValue = null, Command? command = null, CommandBlockType? type = null, string? hover = null, bool? alwaysActive = null, bool? requiresRedstone = null, bool? executeOnFirstTick = null)
		{
			return new CommandBlock(dataValue ?? DataValue, command ?? Command, type ?? Type, hover ?? Hover, alwaysActive ?? AlwaysActive, requiresRedstone ?? RequiresRedstone, executeOnFirstTick ?? ExecuteOnFirstTick);
		}

		private static string GetCommandBlockID(CommandBlockType type)
		{
			return type switch
			{
				CommandBlockType.Repeating => "repeating_command_block",
				CommandBlockType.Chain => "chain_command_block",
				_ => "command_block"
			};
		}

		public override string GetArgumentText() => "";


		#region Object members
		public override int GetHashCode() => HashCode.Combine(Command, Type, AlwaysActive, RequiresRedstone);

		public override bool Equals(object? obj) => obj is CommandBlock other && Equals(other);

		public override string ToString() => $"{Command}; Type={Type}, AlwaysActive={AlwaysActive}, RequiresRedstone={RequiresRedstone}; Hover={Hover}";
		#endregion


		#region IEquatable<CommandBlock> members
		public bool Equals(CommandBlock other) => other is not null && Command == other.Command && Type == other.Type && AlwaysActive == other.AlwaysActive && RequiresRedstone == other.RequiresRedstone;
		#endregion
	}
}
