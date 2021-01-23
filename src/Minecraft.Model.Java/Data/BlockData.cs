using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Minecraft.Model.Java.Data
{
	internal class BlockData
	{
		private static readonly Lazy<List<BlockData>> _data = new Lazy<List<BlockData>>(LoadBlockData);

		[JsonPropertyName("id")]
		public string ID { get; init; } = null!;

		[JsonPropertyName("dn")]
		public string DisplayName { get; init; } = null!;


		private static List<BlockData> LoadBlockData()
		{
			Type type = typeof(BlockData);
			Assembly assembly = type.Assembly;

			string json = GetBlocksJson(type, assembly);

			return JsonSerializer.Deserialize<BlockData[]>(json)!.ToList();
		}

		private static string GetBlocksJson(Type type, Assembly assembly)
		{
			using (Stream stream = assembly.GetManifestResourceStream($"{type.Namespace}.blocks.json")!)
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		public static BlockData? Find(string id)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));

			return _data.Value.Where(bd => bd.ID == id).SingleOrDefault();
		}
	}
}
