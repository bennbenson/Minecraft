using System;
using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class ParticleCommand_Tests
	{
		[TestCase]
		public void Constructor_Throws_On_Null_Effect()
		{
			// Arrange

			// Act
			TestDelegate test = () => new ParticleCommand(null!, null, null, null, 0, 0, null, null);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentNullException>());
		}

		[TestCase]
		public void Constructor_Throws_On_Empty_Effect()
		{
			// Arrange

			// Act
			TestDelegate test = () => new ParticleCommand("", null, null, null, 0, 0, null, null);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentException>());
		}

		[TestCase("dust", 0.0f, 120.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1, 2, ParticleMode.Normal, null, "/particle dust 0 120 0 0 0 0 1 2 normal")]
		//[TestCase()]
		public void GetCommandText_Constructs_Correct_Command(string effect, float? x, float? y, float? z, float? dx, float? dy, float? dz, int speed, int count, ParticleMode? particleMode, string viewers, string expected)
		{
			// Arrange
			PositionF? position = x is not null ? PositionF.Absolute((float)x, (float)y!, (float)z!) : null;
			PositionF? delta = dx is not null ? PositionF.Absolute((float)dx, (float)dy!, (float)dz!) : null;
			ParticleCommand command = new ParticleCommand(effect, null, position, delta, speed, count, particleMode, viewers);

			// Act
			string result = command.GetCommandText();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
