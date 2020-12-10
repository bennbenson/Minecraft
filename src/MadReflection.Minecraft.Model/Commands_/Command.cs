using System;

namespace Minecraft.Model
{
	public abstract class Command
	{
		protected Command(string name)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Command name cannot be empty.", nameof(name));

			Name = name;
		}


		public string Name { get; }

		public abstract string CommandText { get; }


		#region Object members
		public override string ToString() => $"/{Name}";
		#endregion
	}
}
