using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class CloneCommand : Command
	{
		public CloneCommand(Position begin, Position end, Position destination, MaskMode maskMode, CloneMode cloneMode)
			: base("clone")
		{
			if (maskMode is null)
				throw new ArgumentNullException(nameof(maskMode));
			if (cloneMode is null)
				throw new ArgumentNullException(nameof(cloneMode));

			Begin = begin;
			End = end;
			Destination = destination;
			MaskMode = maskMode;
			CloneMode = cloneMode;
		}


		public Position Begin { get; }

		public Position End { get; }

		public Position Destination { get; }

		public MaskMode MaskMode { get; }

		public CloneMode CloneMode { get; }

		protected override Type EqualityContract => typeof(CloneCommand);

		private string DebuggerDisplay => ToString();


		protected override string GetCommandTextImpl(Edition edition)
		{
			StringBuilder result = new StringBuilder();

			if (edition == Edition.Bedrock && MaskMode is FilteredMaskMode fmm)
			{
				IBedrockBlock block = fmm.Block.AsBedrock();
				if (block.DataValue > 0)
					result.Insert(0, $" {block.DataValue}");
				result.Insert(0, block.ID);
				result.Insert(0, ' ');
			}

			result.Insert(0, CloneMode.GetArgumentText(edition));
			result.Insert(0, ' ');

			if (edition == Edition.Java)
			{
				if (MaskMode is FilteredMaskMode jfmm)
				{
					IJavaBlock block = jfmm.Block;
					// TODO: Insert filter data.
					result.Insert(0, block.ID);
					result.Insert(0, ' ');
				}
			}

			result.Insert(0, MaskMode.GetArgumentText(edition));
			result.Insert(0, ' ');

			result.Insert(0, Destination.GetArgumentText(edition));
			result.Insert(0, ' ');

			result.Insert(0, End.GetArgumentText(edition));
			result.Insert(0, ' ');

			result.Insert(0, Begin.GetArgumentText(edition));

			result.Insert(0, "/clone ");

			return result.ToString();
		}
	}
}
