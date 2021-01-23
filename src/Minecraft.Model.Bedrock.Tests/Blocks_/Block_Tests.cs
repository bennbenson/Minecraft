using System;
using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
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

		[TestCase("leaves", 0)]
		public void Get_Returns_Block_On_Known_ID(string id, int dataValue)
		{
			// Arrange

			// Act
			Block result = Block.Get(id, dataValue);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[TestCase("a_block_that_doesnt_exist")]
		public void Get_Throws_On_Unknown_ID(string id)
		{
			// Arrange

			// Act
			TestDelegate test = () => Block.Get(id, 0);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}

		[TestCase("smooth_quartz_stairs", 1)]
		public void Get_Throws_On_Unknown_ID(string id, int dataValue)
		{
			// Arrange

			// Act
			TestDelegate test = () => Block.Get(id, dataValue);

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
			Block result = Block.Get(id, 0);

			// Assert
			Assert.That(result, Is.AssignableTo<CommandBlock>());
		}
	}
}
