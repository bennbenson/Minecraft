using Minecraft.Model.Data;
using NUnit.Framework;

namespace Minecraft.Model.Tests.Data
{
	[TestFixture]
	public class BlockData_Tests
	{
		[TestCase]
		public void Find_By_JavaID()
		{
			// Arrange

			// Act
			BlockData? bd = BlockData.Find("oak_fence", null);

			// Assert
			Assert.That(bd, Is.Not.Null);
		}

		[TestCase]
		public void Find_By_BedrockID()
		{
			// Arrange

			// Act

			// Assert
		}
	}
}
