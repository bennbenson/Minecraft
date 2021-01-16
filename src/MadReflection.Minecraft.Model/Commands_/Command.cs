﻿using System;

namespace Minecraft.Model
{
	public abstract class Command : IEquatable<Command>, ICommandText
	{
		public static readonly Command Empty = new EmptyCommand();

		protected Command(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Command name cannot be empty.", nameof(name));

			Name = name;
		}

		private Command(string name, bool _)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));

			Name = name;
		}


		public string Name { get; }

		protected virtual Type EqualityContract => typeof(Command);


		protected abstract string GetCommandTextImpl(Edition edition);


		#region Object members
		public override int GetHashCode() => 0;

		public override bool Equals(object? obj) => obj is Command other && Equals(other);

		public override string ToString() => $"/{Name}";
		#endregion


		#region IEquatable<Command> members
		public bool Equals(Command? other) => other is not null && EqualityContract == other.EqualityContract;
		#endregion


		#region ICommandText members
		public string GetCommandText(Edition edition)
		{
			if (edition < Edition.Java || edition > Edition.Bedrock)
				throw new ArgumentOutOfRangeException(nameof(edition), "Invalid MinecraftEdition value.");

			return GetCommandTextImpl(edition);
		}
		#endregion


		private sealed class EmptyCommand : Command
		{
			public EmptyCommand()
				: base("", false)
			{
			}


			protected override Type EqualityContract => typeof(EmptyCommand);

			protected override string GetCommandTextImpl(Edition edition) => "";
		}
	}
}
