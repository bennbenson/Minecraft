using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class FillCommand : Command, IEquatable<FillCommand>
	{
		public FillCommand(Position from, Position to, Block block)
			: base("fill")
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));

			From = from;
			To = to;
			Block = block;
			FillMode = FillMode.Replace;
		}

		public FillCommand(Position from, Position to, Block block, FillMode fillMode)
			: base("fill")
		{
			if (block is null)
				throw new ArgumentNullException(nameof(block));
			if (fillMode is null)
				throw new ArgumentNullException(nameof(fillMode));

			From = from;
			To = to;
			Block = block;
			FillMode = fillMode;
		}


		public Position From { get; }

		public Position To { get; }

		public Block Block { get; }

		public FillMode FillMode { get; init; }

		protected override Type EqualityContract => typeof(FillCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			StringBuilder result = new();
			result.Insert(0, FillMode.GetArgumentText());

			bool hasFillMode = result.Length > 0;

			if (hasFillMode)
				result.Insert(0, " ");

			result.Insert(0, Block.GetArgumentText());

			result.Insert(0, $"/fill {From.GetArgumentText()} {To.GetArgumentText()} ");
			return result.ToString();
		}


		#region Object members
		public override int GetHashCode() => HashCode.Combine(From, To, Block, FillMode);

		public override bool Equals(object? obj) => obj is FillCommand other && Equals(other);

		public override string ToString() => GetCommandText();
		#endregion


		#region IEquatable<FillCommand> members
		public bool Equals(FillCommand? other) => other is not null && EqualityContract == other.EqualityContract && From == other.From && To == other.To && Block == other.Block && FillMode == other.FillMode;
		#endregion
	}
}
