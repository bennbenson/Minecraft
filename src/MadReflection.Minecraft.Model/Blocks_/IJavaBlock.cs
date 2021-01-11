using System;

namespace Minecraft.Model
{
	public interface IJavaBlock
	{
		string ID { get; }
	}

	public static class JEBlockExtensions
	{
		public static TResult GetJavaProperty<TResult>(this IJavaBlock block, Func<IJavaBlock, TResult> func)
		{
			if (func is null)
				throw new ArgumentNullException(nameof(func));

			return func(block);
		}
	}
}
