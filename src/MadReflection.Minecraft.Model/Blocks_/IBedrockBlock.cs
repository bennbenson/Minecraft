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
		public static TResult GetBedrockProperty<TResult>(this IBedrockBlock block, Func<IBedrockBlock, TResult> func)
		{
			if (func is null)
				throw new ArgumentNullException(nameof(func));

			return func(block);
		}

		public static Block WithBlockData(this IBedrockBlock block, int dataValue)
		{
			return Block.GetByBedrockID(block.ID, dataValue);
		}
	}
}
