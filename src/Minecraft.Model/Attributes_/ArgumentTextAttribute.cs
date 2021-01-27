using System;

namespace Minecraft.Model
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ArgumentTextAttribute : Attribute
	{
		internal ArgumentTextAttribute(string argumentText)
		{
			if (argumentText is null)
				throw new ArgumentNullException(nameof(argumentText));

			ArgumentText = argumentText;
		}


		public string ArgumentText { get; }
	}
}
