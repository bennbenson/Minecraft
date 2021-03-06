﻿using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class ClearCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			Block block = Block.Get(BlockID.GoldBlock);
			ClearCommand command = new ClearCommand("@a", block);

			string expected = "/clear @a gold_block";

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
