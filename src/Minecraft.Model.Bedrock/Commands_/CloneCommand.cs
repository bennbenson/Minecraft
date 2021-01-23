using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class CloneCommand : Command
	{
		public CloneCommand(Position begin, Position end, Position destination, MaskMode? maskMode = null, CloneMode? cloneMode = null)
			: base("clone")
		{
			Begin = begin;
			End = end;
			Destination = destination;
			MaskMode = maskMode ?? MaskMode.Replace;
			CloneMode = cloneMode ?? CloneMode.Normal;
		}


		public Position Begin { get; }

		public Position End { get; }

		public Position Destination { get; }

		public MaskMode MaskMode { get; }

		public CloneMode CloneMode { get; }

		protected override Type EqualityContract => typeof(CloneCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			StringBuilder result = new StringBuilder($"/clone {Begin.GetArgumentText()} {End.GetArgumentText()} {Destination.GetArgumentText()}");

			if (MaskMode is FilteredMaskMode { Block: Block filterBlock })
			{
				result.Append($" {MaskMode.GetArgumentText()} {CloneMode.GetArgumentText()} {filterBlock.ID}");
				if (filterBlock.DataValue != 0)
					result.Append($" {filterBlock.DataValue}");
			}
			else
			{
				if (MaskMode is not ReplaceMaskMode)
					result.Append($" {MaskMode.GetArgumentText()}");

				if (CloneMode is not NormalCloneMode)
					result.Append($" {CloneMode.GetArgumentText()}");
			}

			return result.ToString();
		}
	}
}
