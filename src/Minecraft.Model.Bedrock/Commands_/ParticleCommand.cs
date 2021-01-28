using System;
using System.Diagnostics;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class ParticleCommand : Command
	{
		public ParticleCommand(string effect, Position position)
			: base("particle")
		{
			if (effect is null)
				throw new ArgumentNullException(nameof(effect));
			if (effect.Length == 0)
				throw new ArgumentException("Effect cannot be empty.", nameof(effect));

			Effect = effect;
			Position = position;
		}


		public string Effect { get; }

		public Position Position { get; }

		protected override Type EqualityContract => typeof(ParticleCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			return $"/particle {Effect.GetArgumentText()} {Position.GetArgumentText()}";
		}
	}
}
