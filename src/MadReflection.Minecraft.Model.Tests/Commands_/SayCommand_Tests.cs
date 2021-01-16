using NUnit.Framework;

namespace Minecraft.Model.Tests
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
			string result = command.GetCommandText(Edition.Bedrock);

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
