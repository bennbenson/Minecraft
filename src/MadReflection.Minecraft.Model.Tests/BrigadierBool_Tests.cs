using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Minecraft.Model.Tests
{
	[TestFixture]
	public class BrigadierBool_Tests
	{
		[TestCase(false)]
		[TestCase(true)]
		public void Constructor_Preserves_Value(bool input)
		{
			// Arrange

			// Act
			bool result = new BrigadierBool(input).Value;

			// Assert
			Assert.That(result, Is.EqualTo(input));
		}

		[TestCase(false, "false")]
		[TestCase(true, "true")]
		public void ToString_Yields_Lowercase(bool input, string expected)
		{
			// Arrange

			// Act
			string result = new BrigadierBool(input).ToString();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}


		// https://ericlippert.com/2012/04/19/null-is-not-false-part-three/
		//[TestCase] public void Operator_False() { }
		//[TestCase] public void Operator_True() { }

	}
}
