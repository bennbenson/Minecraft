using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class TeleportCommand_Tests
	{
		[TestCase("player1", "/tp player1")]
		public void GetCommandText_Destination(string targetPlayer, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targetPlayer);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 100, 0, "/tp 0 100 0")]
		public void GetCommandText_Location(int x, int y, int z, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand((x, y, z));

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@s", "player1", "/tp @s player1")]
		public void GetCommandText_Targets_Destination(string targets, string destination, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targets, destination);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", 0, 100, 0, "/tp @a 0 100 0")]
		public void GetCommandText_Targets_Location(string targets, int x, int y, int z, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targets, (x, y, z));

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", 0, 100, 0, 80, 10, "/tp @a 0 100 0 80 10")]
		public void GetCommandText_Targets_Location_Rotation(string targets, int x, int y, int z, double ry, double rx, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targets, (x, y, z), new Rotation(ry, rx));

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", 0, 100, 0, -1, 100, -1, "/tp @a 0 100 0 facing -1 100 -1")]
		public void GetCommandText_Targets_Location_FacingLocation(string targets, int x, int y, int z, int fx, int fy, int fz, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targets, (x, y, z), (fx, fy, fz));

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", 0, 100, 0, "player1", "/tp @a 0 100 0 facing entity player1")]
		public void GetCommandText_Targets_Location_FacingEntity(string targets, int x, int y, int z, string facingPlayer, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targets, (x, y, z), facingPlayer);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase("@a", 0, 100, 0, "player1", FacingAnchor.Feet, "/tp @a 0 100 0 facing entity player1 feet")]
		public void GetCommandText_Targets_Location_FacingEntity_FacingAnchor(string targets, int x, int y, int z, string facingPlayer, FacingAnchor facingAnchor, string expected)
		{
			// Arrange
			TeleportCommand command = new TeleportCommand(targets, (x, y, z), facingPlayer, facingAnchor);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
