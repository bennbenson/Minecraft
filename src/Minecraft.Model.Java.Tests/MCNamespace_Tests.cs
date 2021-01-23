using System;
using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class MCNamespace_Tests
	{
		[TestCase(null)]
		public void Get_Throws_On_Null(string input)
		{
			// Arrange

			// Act
			TestDelegate test = () => MCNamespace.Get(input);

			// Assert
			Assert.That(test, Throws.TypeOf<ArgumentNullException>());
		}

		[TestCase("")]
		[TestCase("minecraft")]
		public void MyTestMethod1(string input)
		{
			// Arrange

			// Act
			MCNamespace ns = MCNamespace.Get(input);

			// Assert
			Assert.That(ns.NamespaceName, Is.EqualTo(input));
		}

		[TestCase("minecraft")]
		public void Get_Returns_Same_Instance_For_Same_Name(string input)
		{
			// Arrange

			// Act
			MCNamespace ns1 = MCNamespace.Get(input);
			MCNamespace ns2 = MCNamespace.Get(input);

			// Assert
			Assert.That(ns1.NamespaceName, Is.EqualTo(ns2.NamespaceName));
			Assert.That(ns1, Is.EqualTo(ns2));
		}


		[TestCase]
		public void Operator_Addition()
		{
			// Arrange

			// Act
			MCNamespace ns = MCNamespace.Get("anon");
			MCName name = ns + "thing";
			string result = name.ToString();

			// Assert
			Assert.That(result, Is.EqualTo("anon:thing"));
		}

		[TestCase]
		public void Implicit_Cast_From_String()
		{
			// Arrange

			// Act
			MCNamespace ns = "anon";

			// Assert
		}
	}
}
