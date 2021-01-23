using System;

namespace Minecraft.Model
{
	public class DescribedCommand : Command
	{
		public DescribedCommand(Command innerCommand, string description)
			: base(innerCommand?.Name ?? "described")
		{
			if (innerCommand is null)
				throw new ArgumentNullException(nameof(innerCommand));

			InnerCommand = innerCommand;
			Description = description ?? "";
		}


		public Command InnerCommand { get; }

		public string Description { get; }

		protected override Type EqualityContract => InnerCommand.GetType();

		public string DebuggerDisplay => InnerCommand.ToString();


		public override string GetCommandText() => InnerCommand.GetCommandText();


		#region Object members
		public override int GetHashCode() => InnerCommand.GetHashCode();

		public override bool Equals(object? obj) => (obj is DescribedCommand other && Equals(other)) || (obj is Command command && InnerCommand.Equals(command));

		public override string ToString() => InnerCommand.ToString();
		#endregion


		#region IEquatable<DescribedCommand> members
		public bool Equals(DescribedCommand other) => other is not null && InnerCommand.Equals(other.InnerCommand);
		#endregion
	}
}
