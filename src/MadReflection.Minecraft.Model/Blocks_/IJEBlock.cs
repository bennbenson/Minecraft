using System;

namespace Minecraft.Model
{
	public interface IJEBlock
	{
		string ID { get; }
	}

	public static class JEBlockExtensions
	{
		public static TResult GetJavaProperty<TResult>(this IJEBlock block, Func<IJEBlock, TResult> func)
		{
			if (func is null)
				throw new ArgumentNullException(nameof(func));

			return func(block);
		}
	}
}
