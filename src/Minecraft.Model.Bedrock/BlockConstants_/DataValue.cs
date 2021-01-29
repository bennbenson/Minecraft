namespace Minecraft.Model.Bedrock
{
	public static class DataValue
	{
		public static class Stones
		{
			public const int Stone = 0;
			public const int Granite = 1;
			public const int PolishedGranite = 2;
			public const int Diorite = 3;
			public const int PolishedDiorite = 4;
			public const int Andesite = 5;
			public const int PolishedAndesite = 6;
		}

		public static class StoneBrick
		{
			public const int Normal = 0;
			public const int Mossy = 1;
			public const int Cracked = 2;
			public const int Chiseled = 3;
			public const int Smooth = 4;
		}

		public static class Planks
		{
			public const int Oak = 0;
			public const int Spruce = 1;
			public const int Birch = 2;
			public const int Jungle = 3;
			public const int Acacia = 4;
			public const int DarkOak = 5;
		}

		public static class Log
		{
			public const int Oak = 0;
			public const int Spruce = 1;
			public const int Birch = 2;
			public const int Jungle = 3;

			public const int UpDown = 0;
			public const int EastWest = 5;
			public const int NorthSouth = 6;
			internal const int OnlyBark = 7;
		}

		public static class Log2
		{
			public const int Acacia = 4;
			public const int DarkOak = 5;

			public const int UpDown = 0;
			public const int EastWest = 5;
			public const int NorthSouth = 6;
			internal const int OnlyBark = 7;
		}

		public static class StrippedLog
		{
			public const int UpDown = 0;
			public const int EastWest = 1;
			public const int NorthSouth = 2;
		}

		public static class Wood
		{
			public const int Oak = 0;
			public const int Spruce = 1;
			public const int Birch = 2;
			public const int Jungle = 3;
			public const int Acacia = 4;
			public const int DarkOak = 5;
			public const int Stripped = 8;
		}

		public static class Leaves
		{
			public const int Oak = 0;
			public const int Spruce = 1;
			public const int Birch = 2;
			public const int Jungle = 3;
			public const int CheckForDecay = 4;
			public const int Persistent = 8;
			public const int PersistentCheckForDecay = 12;
		}

		public static class Leaves2
		{
			public const int Acacia = 0;
			public const int DarkOak = 1;
			public const int CheckForDecay = 4;
			public const int Persistent = 8;
			public const int PersistentCheckForDecay = 12;
		}

		public static class Fence
		{
			public const int Oak = 0;
			public const int Spruce = 1;
			public const int Birch = 2;
			public const int Jungle = 3;
			public const int Acacia = 4;
			public const int Dark_Oak = 5;
		}

		public static class FenceGate
		{
			public const int South = 0;
			public const int West = 1;
			public const int North = 2;
			public const int East = 3;
			public const int Open = 4;
			public const int Lowered = 8;
		}

		public static class Prismarine
		{
			public const int Normal = 0;
			public const int Dark = 1;
			public const int Bricks = 2;
		}

		public static class Quartz
		{
			public const int Block = 0;
			public const int ChiseledVertical = 1;
			public const int PillarVertical = 2;
			public const int Smooth = 3;

			public const int Vertical = 0;
			public const int EastWest = 4;
			public const int NorthSouth = 8;
		}

		public static class Trapdoor
		{
			public const int South = 0;
			public const int North = 1;
			public const int East = 2;
			public const int West = 3;

			public const int Top = 4;

			public const int Open = 8;
		}

		public static class Stairs
		{
			public const int East = 0;
			public const int West = 1;
			public const int South = 2;
			public const int North = 3;

			public const int Inverted = 4;
		}

		public static class BlockColor
		{
			public const int White = 0;
			public const int Orange = 1;
			public const int Magenta = 2;
			public const int LightBlue = 3;
			public const int Yellow = 4;
			public const int Lime = 5;
			public const int Pink = 6;
			public const int Gray = 7;
			public const int LightGray = 8;
			public const int Cyan = 9;
			public const int Purple = 10;
			public const int Blue = 11;
			public const int Brown = 12;
			public const int Green = 13;
			public const int Red = 14;
			public const int Black = 15;
		}


		public static class CommandBlock
		{
			public const int Down = 0;
			public const int Up = 1;
			public const int North = 2;
			public const int South = 3;
			public const int West = 4;
			public const int East = 5;
		}
	}
}
