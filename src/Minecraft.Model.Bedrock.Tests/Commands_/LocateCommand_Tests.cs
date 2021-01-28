using System;
using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class LocateCommand_Tests
	{
		[TestCase((StructureFeatureType)(-1))]
		[TestCase((StructureFeatureType)14)]
		public void Constructor_Throws_On_Feature_Out_Of_Range(StructureFeatureType feature)
		{
			// Arrange

			// Act
			TestDelegate test = () => new LocateCommand(feature);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(StructureFeatureType.BastionRemnant, "/locate bastion_remnant")]
		[TestCase(StructureFeatureType.EndCity, "/locate endcity")]
		[TestCase(StructureFeatureType.Temple, "/locate temple")]
		[TestCase(StructureFeatureType.Village, "/locate village")]
		public void GetCommandText_Constructs_Correct_Command(StructureFeatureType feature, string expected)
		{
			// Arrange
			LocateCommand command = new LocateCommand(feature);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
