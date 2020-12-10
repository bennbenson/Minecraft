using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minecraft.Model
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class Block : IEquatable<Block>
	{
		private static readonly object _cacheSyncRoot = new object();
		private static readonly Dictionary<(string, int), Block> _cache = new();
		#region Pre-defined block
		public static readonly Block Default = new Block("", false);
		public static readonly Block Air = Get("air");
		#endregion

		private readonly string _id;
		private readonly int _data;


		private protected Block(string id, int data)
		{
			if (id is null)
				throw new ArgumentNullException(nameof(id));
			if (id == "")
				throw new ArgumentException("ID cannot be blank.", nameof(id));
			if (data < 0)
				throw new ArgumentOutOfRangeException(nameof(data), "Data cannot be negative.");

			_id = id;
			_data = data;
		}

		private Block(string id, bool _)
		{
			_id = id;
			_data = 0;
		}


		public string ID => _id ?? "air";

		public int Data { get => _data; init => _data = value; }

		public bool IsUnspecified => _id == "";


		public static void Validate(ref string id, ref int data)
		{
		}

		public string ToStringMinimal => Data > 0 ? ToString() : $"{ID}";

		private string DebuggerDisplay => ToString();

		public static Block Get(string id, int data = 0)
		{
			if (!_cache.TryGetValue((id, data), out Block block))
			{
				lock (_cacheSyncRoot)
				{
					if (!_cache.TryGetValue((id, data), out block))
					{
						block = new Block(id, data);

						_cache.Add((id, data), block);
					}
				}
			}

			return block;
		}



		#region Object members
		public override int GetHashCode() => HashCode.Combine(ID, Data);

		public override bool Equals(object obj) => obj is Block other && Equals(other);

		public override string ToString() => $"{ID} {Data}";
		#endregion


		#region IEquatable<Block> members
		public bool Equals(Block other) => other is not null && ID == other.ID && Data == other.Data;
		#endregion
	}
}
