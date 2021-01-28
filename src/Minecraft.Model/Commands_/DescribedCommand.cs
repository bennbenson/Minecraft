using System;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class DescribedCommand : Command, IWrappedCommand
	{
		public DescribedCommand(Command innerCommand, string description)
			: base(innerCommand?.Name ?? "described")
		{
			if (innerCommand is null)
				throw new ArgumentNullException(nameof(innerCommand));

			InnerCommand = innerCommand;
			Description = description ?? "";
		}


		public string Description { get; }

		protected override Type EqualityContract => InnerCommand.GetType();

		private string DebuggerDisplay => InnerCommand.ToString();


		public override string GetCommandText() => InnerCommand.GetCommandText();


		#region Object members
		public override int GetHashCode() => InnerCommand.GetHashCode();

		public override bool Equals(object? obj) => (obj is DescribedCommand other && Equals(other)) || (obj is Command command && InnerCommand.Equals(command));

		public override string ToString() => InnerCommand.ToString();
		#endregion


		#region IEquatable<DescribedCommand> members
		public bool Equals(DescribedCommand other) => other is not null && InnerCommand.Equals(other.InnerCommand);
		#endregion


		#region IWrappedCommand members
		public Command InnerCommand { get; }
		#endregion
	}
}
