using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class CommandBlock : Block
	{
		public CommandBlock(Command command)
			: this(GetCommandBlockID(CommandBlockType.Impulse), null, command, CommandBlockType.Impulse, "", false, true, false)
		{
		}

		public CommandBlock(Command command, CommandBlockType type)
			: this(GetCommandBlockID(type), null, command, type, "", false, false, false)
		{
		}

		public CommandBlock(Command command, CommandBlockType type, string hover, bool alwaysActive, bool requiresRedstone, bool executeOnFirstTick)
			: this(GetCommandBlockID(type), null, command, type, hover, alwaysActive, requiresRedstone, executeOnFirstTick)
		{
		}

		public CommandBlock(IDictionary<string, string> dataValues, Command command)
			: this(GetCommandBlockID(CommandBlockType.Impulse), CloneDataValues(dataValues), command, CommandBlockType.Impulse, "", false, false, false)
		{
		}

		public CommandBlock(IDictionary<string, string> dataValues, Command command, CommandBlockType type)
			: this(GetCommandBlockID(type), CloneDataValues(dataValues), command, type, "", false, false, false)
		{
		}

		public CommandBlock(IDictionary<string, string> dataValues, Command command, CommandBlockType type, string hover, bool alwaysActive, bool requiresRedstone, bool executeOnFirstTick)
			: this(GetCommandBlockID(type), CloneDataValues(dataValues), command, type, hover, alwaysActive, requiresRedstone, executeOnFirstTick)
		{
		}

		private CommandBlock(string id, Dictionary<string, string>? dataValues, Command command, CommandBlockType type, string hover, bool alwaysActive, bool requiresRedstone, bool executeOnFirstTick)
			: base(id, dataValues)
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


		public override Block With(IDictionary<string, string> dataValues)
		{
			return new CommandBlock(ID, CloneDataValues(dataValues), Command, Type, Hover, AlwaysActive, RequiresRedstone, ExecuteOnFirstTick);
		}

		public CommandBlock With(Command? command = null, CommandBlockType? type = null, string? hover = null, bool? alwaysActive = null, bool? requiresRedstone = null, bool? executeOnFirstTick = null)
		{
			return new CommandBlock(ID, DataValues as Dictionary<string, string>, command ?? Command, type ?? Type, hover ?? Hover, alwaysActive ?? AlwaysActive, requiresRedstone ?? RequiresRedstone, executeOnFirstTick ?? ExecuteOnFirstTick);
		}

		private static string GetCommandBlockID(CommandBlockType type)
		{
			return type switch
			{
				CommandBlockType.Repeating => "repeating_command_block",
				CommandBlockType.Chain => "chain_command_block",
				CommandBlockType.Impulse => "command_block",
				_ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid CommandBlockType value.")
			};
		}

		private static Dictionary<string, string>? CloneDataValues(IDictionary<string, string>? dataValues) => dataValues is null ? null : new Dictionary<string, string>(dataValues);


		//public override string GetArgumentText() => "";


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
