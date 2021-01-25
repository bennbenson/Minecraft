using System;
using System.Linq;
using Minecraft.Model;
using Minecraft.Model.Java;
using NUnit.Framework;

namespace Minecraft.Construction.Java.Tests
{
	[TestFixture]
	public class WarpZone_Tests
	{
		[TestCase]
		public void Defaults()
		{
			// Arrange
			var parameters = new WarpZoneParameters();
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 10 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_100_100_Center()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Center = new Coord2(100, 100)
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill 92 1 92 108 10 108 stone_bricks",
				"/fill 92 1 92 108 3 108 stone_bricks hollow",
				"/fill 95 4 95 105 7 105 air",
				"/fill 92 8 92 108 10 108 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_2_YBase()
		{
			// Arrange
			WarpZoneParameters parameters = new()
			{
				YBase = 5
			};
			Block block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 5 -8 8 14 8 stone_bricks",
				"/fill -8 5 -8 8 7 8 stone_bricks hollow",
				"/fill -5 8 -5 5 11 5 air",
				"/fill -8 12 -8 8 14 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_2_Levels()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Levels = 2
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 17 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow",
				"/fill -5 11 -5 5 14 5 air",
				"/fill -8 15 -8 8 17 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_10_Radius()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Radius = 10
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -10 1 -10 10 10 10 stone_bricks",
				"/fill -10 1 -10 10 3 10 stone_bricks hollow",
				"/fill -7 4 -7 7 7 7 air",
				"/fill -10 8 -10 10 10 10 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_10_Radius_2_Levels()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Levels = 2,
				Radius = 10
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -10 1 -10 10 17 10 stone_bricks",
				"/fill -10 1 -10 10 3 10 stone_bricks hollow",
				"/fill -7 4 -7 7 7 7 air",
				"/fill -10 8 -10 10 10 10 stone_bricks hollow",
				"/fill -7 11 -7 7 14 7 air",
				"/fill -10 15 -10 10 17 10 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		// InteriorHeight
		[TestCase]
		public void Defaults_with_2_InteriorHeight()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				InteriorHeight = 2
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 8 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 5 5 air",
				"/fill -8 6 -8 8 8 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_2_Levels_m1_InterstitialHeight()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Levels = 2,
				InterstitialHeight = -1
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 11 8 stone_bricks",
				"/fill -5 2 -5 5 5 5 air",
				"/fill -5 7 -5 5 10 5 air"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_2_Levels_0_InterstitialHeight()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Levels = 2,
				InterstitialHeight = 0
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 14 8 stone_bricks",
				"/fill -5 3 -5 5 6 5 air",
				"/fill -5 9 -5 5 12 5 air"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_2_Levels_1_InterstitialHeight()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Levels = 2,
				InterstitialHeight = 1
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 17 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow",
				"/fill -5 11 -5 5 14 5 air",
				"/fill -8 15 -8 8 17 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_Walls_CutSidesOnly()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Walls = new CutSidesOnly()
			};
			var stoneBrick = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 10 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -4 5 -7 4 5 7 air",
				"/fill -7 5 -4 7 5 4 air",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, stoneBrick);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
		[TestCase]

		public void Defaults_with_Walls_CutSidesOnly_0_Buffer()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Walls = new CutSidesOnly()
				{
					Buffer = 0
				}
			};
			var stoneBrick = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 10 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -5 5 -7 5 5 7 air",
				"/fill -7 5 -5 7 5 5 air",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, stoneBrick);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
		[TestCase]

		public void Defaults_with_Walls_CutSidesOnly_2_Buffer()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Walls = new CutSidesOnly()
				{
					Buffer = 2
				}
			};
			var stoneBrick = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 10 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -3 5 -7 3 5 7 air",
				"/fill -7 5 -3 7 5 3 air",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, stoneBrick);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_Walls_PlaceCommandBlocks()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Walls = new PlaceCommandBlocks()
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 10 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -4 5 -6 4 5 6 air",
				"/fill -6 5 -4 6 5 4 air",
				"/fill -4 5 -7 4 5 -7 command_block[facing=south]",
				"/fill -4 5 7 4 5 7 command_block[facing=north]",
				"/fill -7 5 4 -7 5 -4 command_block[facing=east]",
				"/fill 7 5 4 7 5 -4 command_block[facing=west]",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_Walls_PlaceCommandBlocks_0_Buffer()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Walls = new PlaceCommandBlocks()
				{
					Buffer = 0
				}
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 10 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -5 5 -6 5 5 6 air",
				"/fill -6 5 -5 6 5 5 air",
				"/fill -5 5 -7 5 5 -7 command_block[facing=south]",
				"/fill -5 5 7 5 5 7 command_block[facing=north]",
				"/fill -7 5 5 -7 5 -5 command_block[facing=east]",
				"/fill 7 5 5 7 5 -5 command_block[facing=west]",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_Walls_PlaceCommandBlocks_2_Buffer()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Walls = new PlaceCommandBlocks()
				{
					Buffer = 2
				}
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -8 1 -8 8 10 8 stone_bricks",
				"/fill -8 1 -8 8 3 8 stone_bricks hollow",
				"/fill -5 4 -5 5 7 5 air",
				"/fill -3 5 -6 3 5 6 air",
				"/fill -6 5 -3 6 5 3 air",
				"/fill -3 5 -7 3 5 -7 command_block[facing=south]",
				"/fill -3 5 7 3 5 7 command_block[facing=north]",
				"/fill -7 5 3 -7 5 -3 command_block[facing=east]",
				"/fill 7 5 3 7 5 -3 command_block[facing=west]",
				"/fill -8 8 -8 8 10 8 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(1, 1)]
		[TestCase(5, 2)]
		//[TestCase()]
		public void Defaults_with_TeleportIn(int yBase, int interstitialHeight)
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				YBase = yBase,
				InterstitialHeight = interstitialHeight,
				TeleportIn = true
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = $"/say You can teleport to 0 {yBase + interstitialHeight + 3} 0.";

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().Last();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}


		[TestCase]
		public void Multiple_Initial_Fill_Commands()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Levels = 5,
				Radius = 31,
				YBase = 10
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -31 10 -31 31 17 31 stone_bricks",
				"/fill -31 18 -31 31 25 31 stone_bricks",
				"/fill -31 26 -31 31 33 31 stone_bricks",
				"/fill -31 34 -31 31 41 31 stone_bricks",
				"/fill -31 42 -31 31 47 31 stone_bricks",
				"/fill -31 10 -31 31 12 31 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var all = commands.ProjectCommandText().ToArray();
			// This test only needs to check that the first 5 are initial fill commands and
			// that the next command is for the first interstitial space.
			var result = all.Take(expected.Length).ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}


		[TestCase]
		public void Defaults_with_2_YBase_10_Radius_2_Levels_2_InterstitialHeight()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				YBase = 2,
				Levels = 2,
				Radius = 10,
				InteriorHeight = 4,
				InterstitialHeight = 2
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -10 2 -10 10 21 10 stone_bricks",
				"/fill -10 2 -10 10 5 10 stone_bricks hollow",
				"/fill -7 6 -7 7 9 7 air",
				"/fill -10 10 -10 10 13 10 stone_bricks hollow",
				"/fill -7 14 -7 7 17 7 air",
				"/fill -10 18 -10 10 21 10 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase]
		public void Defaults_with_12_Radius_3_Levels_CutSides()
		{
			// Arrange
			var parameters = new WarpZoneParameters()
			{
				Levels = 3,
				Radius = 12,
				Walls = new CutSidesOnly()
			};
			var block = Block.Get(BlockID.StoneBricks.Normal);

			var expected = new string[]
			{
				"/fill -12 1 -12 12 24 12 stone_bricks",
				"/fill -12 1 -12 12 3 12 stone_bricks hollow",
				"/fill -9 4 -9 9 7 9 air",
				"/fill -8 5 -11 8 5 11 air",
				"/fill -11 5 -8 11 5 8 air",
				"/fill -12 8 -12 12 10 12 stone_bricks hollow",
				"/fill -9 11 -9 9 14 9 air",
				"/fill -8 12 -11 8 12 11 air",
				"/fill -11 12 -8 11 12 8 air",
				"/fill -12 15 -12 12 17 12 stone_bricks hollow",
				"/fill -9 18 -9 9 21 9 air",
				"/fill -8 19 -11 8 19 11 air",
				"/fill -11 19 -8 11 19 8 air",
				"/fill -12 22 -12 12 24 12 stone_bricks hollow"
			};

			// Act
			var commands = WarpZone.Generate(parameters, block);
			var result = commands.ProjectCommandText().ToArray();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
