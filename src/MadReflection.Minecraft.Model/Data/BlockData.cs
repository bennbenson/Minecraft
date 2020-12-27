﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Minecraft.Model.Data
{
	internal class BlockData
	{
		private static readonly Lazy<List<BlockData>> _data = new Lazy<List<BlockData>>(LoadBlockData);

		[JsonPropertyName("je-id")]
		public string JE_ID { get; init; } = null!;
		[JsonPropertyName("be-id")]
		public string BE_ID { get; init; } = null!;
		[JsonPropertyName("be-dv")]
		public int BE_DV { get; init; } = 0;
		[JsonPropertyName("dn")]
		public string DN { get; init; } = null!;


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

		public static BlockData? Find(string? jeID, BedrockIdentifier? beIdentifier)
		{
			IEnumerable<BlockData> query = _data.Value;

			if (jeID is not null)
				query = query.Where(bd => bd.JE_ID == jeID);

			if (beIdentifier is BedrockIdentifier be)
				query = query.Where(bd => bd.BE_ID == be.ID && bd.BE_DV == be.DV);

			return query.SingleOrDefault();
		}
	}
}