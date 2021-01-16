using System;

namespace Minecraft.Model
{
	public interface IJavaBlock
	{
		string ID { get; }
	}

	public static class JEBlockExtensions
	{
		public static IJavaBlock AsJava(this IJavaBlock block) => block;
	}
}
