using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class SayCommand_Tests
	{
		[TestCase]
		public void GetCommandText_Constructs_Correct_Command()
		{
			// Arrange
			SayCommand command = new SayCommand("Hello World!");

			string expected = "/say Hello World!";

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
