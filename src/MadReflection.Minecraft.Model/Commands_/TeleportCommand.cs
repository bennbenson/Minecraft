using System;

namespace Minecraft.Model
{
	public class TeleportCommand : Command
	{
		private readonly string _whom;


		public TeleportCommand(TeleportTarget target)
			: this("@s", target, null)
		{
		}

		public TeleportCommand(TeleportTarget target, Coord3 facing)
			: this("@s", target, facing)
		{
		}

		public TeleportCommand(string whom, TeleportTarget target)
			: this(whom, target, null)
		{
		}

		public TeleportCommand(string whom, TeleportTarget target, Coord3? facing)
			: base("tp")
		{
			if (whom is null)
				throw new ArgumentNullException(nameof(whom));
			if (target is null)
				throw new ArgumentNullException(nameof(target));

			_whom = whom;
			Target = target;
			Facing = facing;
		}


		public string Whom => _whom;

		public TeleportTarget Target { get; }

		public Coord3? Facing { get; }

		public override string CommandText
		{
			get
			{
				string result = $"/{Name} ";

				if (_whom is not null)
					result += " " + _whom;

				result += " " + Target.ArgumentText;

				if (Facing is Coord3 facing)
					result += " " + facing.ArgumentText;

				return result;
			}
		}
	}
}
