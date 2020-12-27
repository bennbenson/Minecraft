using System.Collections.Generic;

namespace Minecraft.Data
{
	public record BlockData(string NamespaceName, string Name, string ID, int DataValue, string DisplayName)
	{
		public const string MCNS = "minecraft";

		public List<BlockData> _blocks = new List<BlockData>()
		{
			{ "", "air", "air", 0, "Air" }
		};
	}

	internal static class BlockDataDictionaryExtensions
	{
		public static void Add(this List<BlockData> list, string namespaceName, string name, string id, int dataValue, string displayName)
		{
			list.Add(new BlockData(namespaceName, name, id, dataValue, displayName));
		}
	}
}
