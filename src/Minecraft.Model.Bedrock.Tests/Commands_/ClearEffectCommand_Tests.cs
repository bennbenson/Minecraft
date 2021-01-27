using System;
using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class ClearEffectCommand_Tests
	{
		[TestCase]
		public void Constructor_Throws_On_Null_Target()
		{
			// Arrange

			// Act
			TestDelegate test = () => new ClearEffectCommand(null!, null);

			// Assert
			Assert.That(test, Throws.ArgumentNullException);
		}

		[TestCase("@a", (StatusEffect)0)]
		[TestCase("@a", (StatusEffect)30)]
		public void Constructor_Throws_On_Invalid_StatusEffect(string target, StatusEffect effect)
		{
			// Arrange

			// Act
			TestDelegate test = () => new ClearEffectCommand(target, effect);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase("@a", "/effect @a clear")]
		public void GetCommandText_All(string target, string expected)
		{
			// Arrange
			ClearEffectCommand command = new ClearEffectCommand(target);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", StatusEffect.NightVision, "/effect @a night_vision 0")]
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
