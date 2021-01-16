using System;

namespace Minecraft.Model
{
	public interface IBedrockBlock
	{
		string ID { get; }
		int DataValue { get; }
	}

	public static class BEBlockExtensions
	{
		public static IBedrockBlock AsBedrock(this IBedrockBlock block) => block;

		public static Block WithBlockData(this IBedrockBlock block, int dataValue)
		{
			return Block.GetByBedrockID(block.ID, dataValue);
		}
	}
}
