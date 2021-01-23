using CommandLine;

namespace WarpZoneGenerator
{
	internal class Options
	{
		[Option('c', "center", Required = false)]
		public string? Center { get; set; }

		[Option('y', "ybase", Required = false)]
		public int? YBase { get; set; }

		[Option('l', "levels", Required = false)]
		public int? Levels { get; set; }

		[Option('r', "radius", Required = false)]
		public int? Radius { get; set; }

		[Option('h', "interior", Required = false)]
		public int? InteriorHeight { get; set; }

		[Option('i', "interstitial", Required = false)]
		public int? InterstitialHeight { get; set; }

		[Option('x', "cutsides", Required = false)]
		public bool? CutSides { get; set; }

		[Option('t', "teleportin", Required = false)]
		public bool? TeleportIn { get; set; }
	}
}
