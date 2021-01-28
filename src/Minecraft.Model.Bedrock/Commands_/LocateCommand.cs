using System;
using System.Diagnostics;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class LocateCommand : Command
	{
		public LocateCommand(StructureFeatureType feature)
			: base("locate")
		{
			if (feature < StructureFeatureType.BastionRemnant || feature > StructureFeatureType.Village)
				throw new ArgumentOutOfRangeException(nameof(feature), $"Invalid {nameof(StructureFeatureType)} value.");

			Feature = feature;
		}


		public StructureFeatureType Feature { get; }

		protected override Type EqualityContract => typeof(OpCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText() => $"/locate {Feature.GetArgumentText()}";
	}
}
