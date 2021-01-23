using System;
using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class Target_Tests
	{
	}
	[TestFixture]
	public class TargetPlayer_Tests
	{
		[TestCase("@a", PlayerSelectorType.All)]
		public void Parse_Succeeds_On_Selector(string input, PlayerSelectorType type)
		{
			// Arrange

			// Act
			var result = TargetPlayer.Parse(input);

			// Assert
			Assert.That(result, Is.TypeOf<TargetPlayerSelector>());
			Assert.That(((TargetPlayerSelector)result).Type, Is.EqualTo(PlayerSelectorType.All));
		}

		[TestCase("playerx")]
		public void Parse_Succeeds_On_Name(string input)
		{
			// Arrange

			// Act
			var result = TargetPlayer.Parse(input);

			// Assert
			Assert.That(result, Is.TypeOf<TargetPlayerName>());
			Assert.That(((TargetPlayerName)result).Name, Is.EqualTo(input));
		}

		[TestCase("asdf-qwer")]
		public void Parse_Throws_On_Invalid_Inputs(string input)
		{
			// Arrange

			// Act
			TestDelegate test = () => TargetPlayer.Parse(input);

			// Assert
			Assert.That(test, Throws.TypeOf<FormatException>());
		}

		[TestCase("@a", true)]
		[TestCase("playerx", true)]
		[TestCase("asdf-qwer", false)]
		public void TryParse_Succeeds(string input, bool expected)
		{
			// Arrange

			// Act
			bool result = TargetPlayer.TryParse(input, out TargetPlayer? output);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
	[TestFixture]
	public class TargetPlayerName_Tests
	{
	}
	[TestFixture]
	public class TargetPlayerSelector_Tests
	{
	}
	[TestFixture]
	public class TargetPosition_Tests
	{
	}
}
