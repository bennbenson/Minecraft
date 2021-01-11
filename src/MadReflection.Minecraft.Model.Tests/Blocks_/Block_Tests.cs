using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
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
			Assert.That(((IJavaBlock)result).ID, Is.EqualTo(expected));
			Assert.That(((IBedrockBlock)result).ID, Is.EqualTo(expected));
		}

		[TestCase]
		public void Default_Is_Air()
		{
			// Arrange
			string expected = "air";

			// Act
			Block result = Block.Default;

			// Assert
			Assert.That(((IJavaBlock)result).ID, Is.EqualTo(expected));
			Assert.That(((IBedrockBlock)result).ID, Is.EqualTo(expected));
		}

		[TestCase("oak_fence")]
		public void GetByJavaID_Returns_Block_On_Known_ID(string input)
		{
			// Arrange

			// Act
			Block result = Block.GetByJavaID(input);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[TestCase("a_block_youve_never_heard_of")]
		public void GetByJavaID_Throws_On_Unknown_ID(string id)
		{
			// Arrange

			// Act
			TestDelegate test = () => Block.GetByJavaID(id);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}

		[TestCase("leaves", 0)]
		public void GetByBedrockID_Returns_Block_On_Known_ID(string id, int dataValue)
		{
			// Arrange

			// Act
			Block result = Block.GetByBedrockID(id, dataValue);

			// Assert
			Assert.That(result, Is.Not.Null);
		}

		[TestCase("command_block")]
		[TestCase("chain_command_block")]
		[TestCase("repeating_command_block")]
		public void GetByJavaID_Command_Block_Returns_Subclass(string id)
		{
			// Arrange

			// Act
			Block result = Block.GetByJavaID(id);

			// Assert
			Assert.That(result, Is.AssignableTo<CommandBlock>());
		}

		[TestCase("command_block")]
		[TestCase("chain_command_block")]
		[TestCase("repeating_command_block")]
		public void GetByBedrockID_Command_Block_Returns_Subclass(string id)
		{
			// Arrange

			// Act
			Block result = Block.GetByBedrockID(id, 0);

			// Assert
			Assert.That(result, Is.AssignableTo<CommandBlock>());
		}
	}
}
