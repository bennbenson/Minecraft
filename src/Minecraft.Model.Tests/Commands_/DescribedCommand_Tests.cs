using System;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class DescribedCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Returns_InnerCommand_Command_Text()
		{
			// Arrange
			string description = "This is a test command.";
			TestCommand innerCommand = new TestCommand();
			DescribedCommand command = new DescribedCommand(innerCommand, description);

			string expected = innerCommand.GetCommandText();

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
			Assert.That(command.Description, Is.EqualTo(description));
		}


		private class TestCommand : Command
		{
			public TestCommand()
				: base("test")
			{
			}


			public override string GetCommandText()
			{
				return "/test";
			}
		}
	}
}
