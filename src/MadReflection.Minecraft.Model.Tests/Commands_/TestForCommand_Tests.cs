﻿using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class TestForCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			TestForCommand command = new TestForCommand(TargetPlayerSelector.Entity.WithRadius(4));

			string expected = "/testfor @e[r=4]";

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
