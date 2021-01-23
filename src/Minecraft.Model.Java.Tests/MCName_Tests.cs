using NUnit.Framework;

namespace Minecraft.Model.Java.Tests
{
	[TestFixture]
	public class MCName_Tests
	{
		[TestCase]
		public void Get_By_Name_and_NamespaceName()
		{
			// Arrange

			// Act
			MCName name = MCName.Get("thing", "anon");

			// Assert
			Assert.That(name.Name, Is.EqualTo("thing"));
			Assert.That(name.NamespaceName, Is.EqualTo("anon"));
		}

		[TestCase]
		public void Get_By_FullName()
		{
			// Arrange

			// Act
			MCName name = MCName.Get("anon:thing");

			// Assert
			Assert.That(name.Name, Is.EqualTo("thing"));
			Assert.That(name.NamespaceName, Is.EqualTo("anon"));
		}
	}
}
