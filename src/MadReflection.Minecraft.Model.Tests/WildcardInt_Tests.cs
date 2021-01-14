using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class WildcardInt_Tests
	{
		[TestCase]
		public void Default_Is_Zero()
		{
			// Arrange

			// Act
			WildcardInt result = default;

			// Assert
			Assert.That(result.Value, Is.EqualTo(0));
			Assert.That(result.IsWildcard, Is.False);
		}
	}
}
