using System;
using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class EnchantCommand_Tests
	{
		[TestCase(null, Enchantment.Efficiency, 0)]
		public void Constructor_Throws_On_Null_Target(string target, Enchantment enchantment, int level)
		{
			// Arrange

			// Act
			TestDelegate test = () => new EnchantCommand(target, enchantment, level);

			// Assert
			Assert.That(test, Throws.ArgumentNullException);
		}

		[TestCase("@a", (Enchantment)(-3), 0)]
		public void Constructor_Throws_On_Enchantment_Out_Of_Range(string target, Enchantment enchantment, int level)
		{
			// Arrange

			// Act
			TestDelegate test = () => new EnchantCommand(target, enchantment, level);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase("@a", Enchantment.FireProtection, -1)]
		public void Constructor_Throws_On_Level_Out_Of_Range(string target, Enchantment enchantment, int level)
		{
			// Arrange

			// Act
			TestDelegate test = () => new EnchantCommand(target, enchantment, level);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase("@a", Enchantment.FireAspect, 1, "/enchant @a fire_aspect 1")]
		public void GetCommandText_1(string target, Enchantment enchantment, int level, string expected)
		{
			// Arrange
			EnchantCommand command = new EnchantCommand(target, enchantment, level);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
