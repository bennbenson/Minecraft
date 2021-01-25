using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Minecraft.Construction.Tests
{
	[TestFixture]
	public class WarpZoneParameters_Tests
	{
		[TestCase(-1)]
		[TestCase(253)]
		public void YBase_Throws_On_Out_Of_Range_Values(int input)
		{
			// Arrange
			WarpZoneParameters parameters = new WarpZoneParameters();

			// Act
			TestDelegate test = () => { parameters.YBase = input; };

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(-1)]
		[TestCase(0)]
		public void Levels_Throws_On_Out_Of_Range_Values(int input)
		{
			// Arrange
			WarpZoneParameters parameters = new WarpZoneParameters();

			// Act
			TestDelegate test = () => { parameters.Levels = input; };

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(-1)]
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public void Radius_Throws_On_Out_Of_Range_Values(int input)
		{
			// Arrange
			WarpZoneParameters parameters = new WarpZoneParameters();

			// Act
			TestDelegate test = () => { parameters.Radius = input; };

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(1)]
		[TestCase(11)]
		public void InteriorHeight_Throws_On_Out_Of_Range_Values(int input)
		{
			// Arrange
			WarpZoneParameters parameters = new WarpZoneParameters();

			// Act
			TestDelegate test = () => { parameters.InteriorHeight = input; };

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[TestCase(-2)]
		[TestCase(11)]
		public void InterstitialHeight_Throws_On_Out_Of_Range_Values(int input)
		{
			// Arrange
			WarpZoneParameters parameters = new WarpZoneParameters();

			// Act
			TestDelegate test = () => { parameters.InterstitialHeight = input; };

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentOutOfRangeException>());
		}
	}
}
