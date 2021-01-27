using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class SayCommand_Tests
	{
		[TestCase("Hello, World!", "/say Hello, World!")]
		public void GetCommandText_Returns_Correct_Command(string message, string expected)
		{
			// Arrange
			SayCommand command = new SayCommand(message);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
