using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class TeleportCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			TeleportCommand command = new TeleportCommand("@s", (TargetPlayer)"player1", (TargetPosition)(0, 100, 0));

			string expected = "/tp @s player1 facing 0 100 0";

			// Act
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
