using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class GiveEffectCommand_Tests
	{
		[TestCase("@a", StatusEffect.NightVision, "/effect @a night_vision")]
		public void GetCommandText_Effect(string target, StatusEffect effect, string expected)
		{
			// Arrange
			GiveEffectCommand command = new GiveEffectCommand(target, effect);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", StatusEffect.NightVision, 180, "/effect @a night_vision 180")]
		public void GetCommandText_Effect_Seconds(string target, StatusEffect effect, int seconds, string expected)
		{
			// Arrange
			GiveEffectCommand command = new GiveEffectCommand(target, effect, seconds);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}


		[TestCase("@a", StatusEffect.NightVision, 180, 0, "/effect @a night_vision 180")]
		[TestCase("@a", StatusEffect.NightVision, 180, 3, "/effect @a night_vision 180 3")]
		public void GetCommandText_3(string target, StatusEffect effect, int seconds, int amplifier, string expected)
		{
			// Arrange
			GiveEffectCommand command = new GiveEffectCommand(target, effect, seconds, amplifier);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", StatusEffect.NightVision, 180, 3, false, "/effect @a night_vision 180 3")]
		[TestCase("@a", StatusEffect.NightVision, 180, 3, true,  "/effect @a night_vision 180 3 true")]
		public void GetCommandText_3(string target, StatusEffect effect, int seconds, int amplifier, bool hideParticles, string expected)
		{
			// Arrange
			GiveEffectCommand command = new GiveEffectCommand(target, effect, seconds, amplifier, hideParticles);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
