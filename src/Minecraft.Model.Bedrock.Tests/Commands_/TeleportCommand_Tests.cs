using NUnit.Framework;

namespace Minecraft.Model.Bedrock.Tests
{
	[TestFixture]
	public class TeleportCommand_Tests
	{
		[TestCase("player1", "/tp player1")]
		[TestCase("@r", "/tp @r")]
		public void GetCommandText_DestinationPlayer(string targetPlayer, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targetPlayer);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void GetCommandText_DestinationPosition()
		{
			// Arrange
			//TeleportCommand command = new TeleportCommand("");

			// Act

			// Assert
		}

		//[TestCase]
		//public void GetCommandText_Constructs_Correct_Command()
		//{
		//	// Arrange
		//	TeleportCommand command = new TeleportCommand("@s", (TargetPlayer)"player1", facing:(TargetPosition)(0, 100, 0));

		//	string expected = "/tp @s player1 facing 0 100 0";

		//	// Act
		//	string result = command.GetCommandText();

		//	// Assert
		//	Assert.That(result, Is.EqualTo(expected));
		//}

		//[TestCase]
		//public void GetCommandText_Constructs_Correct_Command()
		//{
		//	// Arrange
		//	TeleportCommand command = new TeleportCommand("@s", (TargetPlayer)"player1", facing:(TargetPosition)(0, 100, 0));

		//	string expected = "/tp @s player1 facing 0 100 0";

		//	// Act
		//	string result = command.GetCommandText();

		//	// Assert
		//	Assert.That(result, Is.EqualTo(expected));
		//}
	}
}
