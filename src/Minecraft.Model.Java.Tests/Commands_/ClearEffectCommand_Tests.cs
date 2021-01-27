using System;
using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class ClearEffectCommand_Tests
	{
		[TestCase("@a", (StatusEffect)0)]
		[TestCase("@a", (StatusEffect)33)]
		public void Constructor_Throws_On_Invalid_StatusEffect(string target, StatusEffect effect)
		{
			// Arrange

			// Act
			TestDelegate test = () => new ClearEffectCommand(target, effect);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase("@a", "/effect clear @a")]
		public void GetCommandText_All(string target, string expected)
		{
			// Arrange
			ClearEffectCommand command = new ClearEffectCommand(target);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", StatusEffect.NightVision, "/effect clear @a night_vision")]
		public void GetCommandText_All_NightVision(string target, StatusEffect effect, string expected)
		{
			// Arrange
			ClearEffectCommand command = new ClearEffectCommand(target, effect);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
