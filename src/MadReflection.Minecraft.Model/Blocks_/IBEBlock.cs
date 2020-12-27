using System;

namespace Minecraft.Model
{
	public interface IBEBlock
	{
		string ID { get; }
		int DV { get; }
	}

	public static class BEBlockExtensions
	{
		public static TResult GetBedrockProperty<TResult>(this IBEBlock block, Func<IBEBlock, TResult> func)
		{
			if (func is null)
				throw new ArgumentNullException(nameof(func));

			return func(block);
		}
	}
}
