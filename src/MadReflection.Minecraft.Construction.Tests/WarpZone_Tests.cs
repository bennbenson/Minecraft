using System;
using System.Linq;
using Minecraft.Model;
using NUnit.Framework;

namespace Minecraft.Construction.Tests
{
	[TestFixture]
	public class WarpZone_Tests
	{
		[TestCase]
		public void Simple_Radius_10_with_1_Level()
		{
			// Arrange
			WarpZoneParameters parameters = new()
			{
				Center = new Coord2(0, 0),
				YBase = 2,
				Levels = 1,
				Radius = 10,
				InteriorHeight = 4,
				InterstitialHeight = 1,
				CutSides = false
			};
			Block block = Block.GetByBedrockID("stonebrick");

			// Act
			Command[] commands = WarpZone.Generate(parameters, block).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Count, Is.EqualTo(4));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -10 2 -10 10 11 10 stonebrick"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -10 2 -10 10 4 10 stonebrick 0 hollow"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -7 5 -7 7 8 7 air"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill -10 9 -10 10 11 10 stonebrick 0 hollow"));
		}

		[TestCase]
		public void Simple_Radius_10_with_2_Levels()
		{
			// Arrange
			WarpZoneParameters parameters = new()
			{
				Center = new Coord2(0, 0),
				YBase = 2,
				Levels = 2,
				Radius = 10,
				InteriorHeight = 4,
				InterstitialHeight = 1,
				CutSides = false
			};
			Block block = Block.GetByBedrockID("stonebrick");

			// Act
			System.Collections.Generic.IEnumerable<Command> commands = WarpZone.Generate(parameters, block);
			string[] commandTexts = commands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Count, Is.EqualTo(6));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -10 2 -10 10 18 10 stonebrick"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -10 2 -10 10 4 10 stonebrick 0 hollow"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -7 5 -7 7 8 7 air"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill -10 9 -10 10 11 10 stonebrick 0 hollow"));
			Assert.That(commandTexts[4], Is.EqualTo("/fill -7 12 -7 7 15 7 air"));
			Assert.That(commandTexts[5], Is.EqualTo("/fill -10 16 -10 10 18 10 stonebrick 0 hollow"));
		}

		[TestCase]
		public void Simple_Radius_10_with_2_Levels_0_Interstitial()
		{
			// Arrange
			WarpZoneParameters parameters = new()
			{
				Center = new Coord2(0, 0),
				YBase = 2,
				Levels = 2,
				Radius = 10,
				InteriorHeight = 4,
				InterstitialHeight = 0,
				CutSides = false
			};
			Block block = Block.GetByBedrockID("stonebrick");

			// Act
			Command[] commands = WarpZone.Generate(parameters, block).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Count, Is.EqualTo(3));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -10 2 -10 10 12 10 stonebrick"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -7 3 -7 7 6 7 air"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -7 8 -7 7 11 7 air"));
		}

		[TestCase]
		public void Simple_Radius_10_with_2_Levels_2_Interstitial()
		{
			// Arrange
			WarpZoneParameters parameters = new()
			{
				Center = new Coord2(0, 0),
				YBase = 2,
				Levels = 2,
				Radius = 10,
				InteriorHeight = 4,
				InterstitialHeight = 2,
				CutSides = false
			};
			Block block = Block.GetByBedrockID("stonebrick");

			// Act
			Command[] commands = WarpZone.Generate(parameters, block).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Count, Is.EqualTo(6));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -10 2 -10 10 21 10 stonebrick"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -10 2 -10 10 5 10 stonebrick 0 hollow"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -7 6 -7 7 9 7 air"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill -10 10 -10 10 13 10 stonebrick 0 hollow"));
			Assert.That(commandTexts[4], Is.EqualTo("/fill -7 14 -7 7 17 7 air"));
			Assert.That(commandTexts[5], Is.EqualTo("/fill -10 18 -10 10 21 10 stonebrick 0 hollow"));
		}


		[TestCase]
		public void Simple_Radius_10_with_3_Levels_Pair_and_CutSides()
		{
			// Arrange
			WarpZoneParameters parameters = new()
			{
				Center = Coord2.At(0, 0),
				YBase = 1,
				Levels = 3,
				Radius = 12,
				InteriorHeight = 4,
				InterstitialHeight = 1,
				CutSides = true
			};
			Block stoneBrick = Block.GetByBedrockID("stonebrick");
			Block blackStoneBrick = Block.GetByBedrockID("polished_blackstone_bricks");

			// Act
			Command[] normalCommands = WarpZone.Generate(parameters, stoneBrick).ToArray();
			Command[] netherCommands = WarpZone.Generate(parameters, blackStoneBrick).ToArray();

			string[] normalCommandTexts = normalCommands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).ToArray();
			string[] netherCommandTexts = netherCommands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).ToArray();

			// Assert
			Assert.That(normalCommandTexts.Length, Is.EqualTo(14));
			Assert.That(normalCommandTexts[0], Is.EqualTo("/fill -12 1 -12 12 24 12 stonebrick"));
			Assert.That(normalCommandTexts[1], Is.EqualTo("/fill -12 1 -12 12 3 12 stonebrick 0 hollow"));
			Assert.That(normalCommandTexts[2], Is.EqualTo("/fill -9 4 -9 9 7 9 air"));
			Assert.That(normalCommandTexts[3], Is.EqualTo("/fill -11 5 -9 11 5 9 air"));
			Assert.That(normalCommandTexts[4], Is.EqualTo("/fill -9 5 -11 9 5 11 air"));
			Assert.That(normalCommandTexts[5], Is.EqualTo("/fill -12 8 -12 12 10 12 stonebrick 0 hollow"));
			Assert.That(normalCommandTexts[6], Is.EqualTo("/fill -9 11 -9 9 14 9 air"));
			Assert.That(normalCommandTexts[7], Is.EqualTo("/fill -11 12 -9 11 12 9 air"));
			Assert.That(normalCommandTexts[8], Is.EqualTo("/fill -9 12 -11 9 12 11 air"));
			Assert.That(normalCommandTexts[9], Is.EqualTo("/fill -12 15 -12 12 17 12 stonebrick 0 hollow"));
			Assert.That(normalCommandTexts[10], Is.EqualTo("/fill -9 18 -9 9 21 9 air"));
			Assert.That(normalCommandTexts[11], Is.EqualTo("/fill -11 19 -9 11 19 9 air"));
			Assert.That(normalCommandTexts[12], Is.EqualTo("/fill -9 19 -11 9 19 11 air"));
			Assert.That(normalCommandTexts[13], Is.EqualTo("/fill -12 22 -12 12 24 12 stonebrick 0 hollow"));

			Assert.That(netherCommandTexts.Length, Is.EqualTo(14));
			Assert.That(netherCommandTexts[0], Is.EqualTo("/fill -12 1 -12 12 24 12 polished_blackstone_bricks"));
			Assert.That(netherCommandTexts[1], Is.EqualTo("/fill -12 1 -12 12 3 12 polished_blackstone_bricks 0 hollow"));
			Assert.That(netherCommandTexts[2], Is.EqualTo("/fill -9 4 -9 9 7 9 air"));
			Assert.That(netherCommandTexts[3], Is.EqualTo("/fill -11 5 -9 11 5 9 air"));
			Assert.That(netherCommandTexts[4], Is.EqualTo("/fill -9 5 -11 9 5 11 air"));
			Assert.That(netherCommandTexts[5], Is.EqualTo("/fill -12 8 -12 12 10 12 polished_blackstone_bricks 0 hollow"));
			Assert.That(netherCommandTexts[6], Is.EqualTo("/fill -9 11 -9 9 14 9 air"));
			Assert.That(netherCommandTexts[7], Is.EqualTo("/fill -11 12 -9 11 12 9 air"));
			Assert.That(netherCommandTexts[8], Is.EqualTo("/fill -9 12 -11 9 12 11 air"));
			Assert.That(netherCommandTexts[9], Is.EqualTo("/fill -12 15 -12 12 17 12 polished_blackstone_bricks 0 hollow"));
			Assert.That(netherCommandTexts[10], Is.EqualTo("/fill -9 18 -9 9 21 9 air"));
			Assert.That(netherCommandTexts[11], Is.EqualTo("/fill -11 19 -9 11 19 9 air"));
			Assert.That(netherCommandTexts[12], Is.EqualTo("/fill -9 19 -11 9 19 11 air"));
			Assert.That(netherCommandTexts[13], Is.EqualTo("/fill -12 22 -12 12 24 12 polished_blackstone_bricks 0 hollow"));

			// Copy/Paste
			string output = string.Join(Environment.NewLine, normalCommands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).Concat(new[] { "" }).Concat(netherCommands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock))));
		}

		[TestCase]
		public void Multiple_Initial_Fill_Commands()
		{
			// Arrange
			WarpZoneParameters parameters = new WarpZoneParameters()
			{
				Center = new Coord2(0, 0),
				Levels = 5,
				Radius = 31,
				YBase = 10
			};
			Block block = Block.GetByBedrockID("stonebrick");

			// Act
			Command[] commands = WarpZone.Generate(parameters, block).ToArray();
			string[] commandTexts = commands.Select(c => c.GetCommandText(MinecraftEdition.Bedrock)).ToArray();

			// Assert
			Assert.That(commandTexts.Length, Is.EqualTo(16));
			Assert.That(commandTexts[0], Is.EqualTo("/fill -31 10 -31 31 17 31 stonebrick"));
			Assert.That(commandTexts[1], Is.EqualTo("/fill -31 18 -31 31 25 31 stonebrick"));
			Assert.That(commandTexts[2], Is.EqualTo("/fill -31 26 -31 31 33 31 stonebrick"));
			Assert.That(commandTexts[3], Is.EqualTo("/fill -31 34 -31 31 41 31 stonebrick"));
			Assert.That(commandTexts[4], Is.EqualTo("/fill -31 42 -31 31 47 31 stonebrick"));
			// No need to check the rest.
		}
	}
}
