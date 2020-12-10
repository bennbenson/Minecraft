using Minecraft.Data;
using NUnit.Framework;

namespace MadReflection.Minecraft.Data.Tests
{
	// https://minecraft.gamepedia.com/Seed_(level_generation)

	[TestFixture]
	public class Seed_Tests
	{
		[TestCase("creashaks organzine")]
		[TestCase("pollinating sandboxes")]
		//[TestCase("little jungle")]
		//[TestCase("small skeleton")]
		//[TestCase("drumwood boulder head")]
		[TestCase("ddnqavbj")]
		[TestCase("166lr735ka3q6")]
		public void GetSeedFromString_Zero_Seed(string input)
		{
			// Arrange

			// Act
			int result = Seeds.GetSeedFromString(input);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[TestCase]
		public void Seed_Text_Is_Trimmed()
		{
			// Arrange
			string input = " Seed ";

			// Act
			int seed1 = Seeds.GetSeedFromString(input);
			int seed2 = Seeds.GetSeedFromString(input.Trim());

			// Assert
			Assert.That(seed2, Is.EqualTo(seed1));
		}
	}
}
