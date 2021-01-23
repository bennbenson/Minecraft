using System;
using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class Block_Tests
	{
		[TestCase]
		public void Unspecified_Has_Empty_ID()
		{
			// Arrange
			string expected = "";

			// Act
			Block result = Block.Unspecified;

			// Assert
			Assert.That(result.IsUnspecified);
			Assert.That(result.ID, Is.EqualTo(expected));
		}

		[TestCase]
		public void Default_Is_Air()
		{
			// Arrange
			string expected = "air";

			// Act
			Block result = Block.Default;

			// Assert
			Assert.That(result.ID, Is.EqualTo(expected));
		}

		[TestCase("oak_fence")]
		public void Get_Returns_Block_On_Known_ID(string input)
		{
			// Arrange

			// Act
			Block result = Block.Get(input);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[TestCase("a_block_that_doesnt_exist")]
		public void Get_Throws_On_Unknown_ID(string id)
		{
			// Arrange

			// Act
			TestDelegate test = () => Block.Get(id);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}

		[TestCase("command_block")]
		[TestCase("chain_command_block")]
		[TestCase("repeating_command_block")]
		public void Get_Command_Block_Returns_Subclass(string id)
		{
			// Arrange

			// Act
			Block result = Block.Get(id);

			// Assert
			Assert.That(result, Is.AssignableTo<CommandBlock>());
		}
	}
}
