using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class GiveEffectCommand : Command
	{
		public GiveEffectCommand(TargetPlayer target, StatusEffect effect)
			: this(target, effect, null, 0, false)
		{
		}

		public GiveEffectCommand(TargetPlayer target, StatusEffect effect, int seconds)
			: this(target, effect, seconds, 0, false)
		{
		}

		public GiveEffectCommand(TargetPlayer target, StatusEffect effect, int seconds, int amplifier)
			: this(target, effect, (int?)seconds, amplifier, false)
		{
		}

		public GiveEffectCommand(TargetPlayer target, StatusEffect effect, int seconds, int amplifier, bool hideParticles)
			: this(target, effect, (int?)seconds, amplifier, hideParticles)
		{
		}

		private GiveEffectCommand(TargetPlayer target, StatusEffect effect, int? seconds, int amplifier, bool hideParticles)
			: base("effect")
		{
			if (target is null)
				throw new ArgumentNullException(nameof(target));
			if (effect < StatusEffect.Speed || effect > StatusEffect.HeroOfTheVillage)
				throw new ArgumentOutOfRangeException(nameof(effect), "Invalid status effect.");
			if (seconds < 1)
				throw new ArgumentOutOfRangeException(nameof(seconds), "Seconds cannot be zero or negative.");

			Target = target;
			Effect = effect;
			Seconds = seconds;
			Amplifier = amplifier;
			HideParticles = hideParticles;
		}


		public TargetPlayer Target { get; }

		public StatusEffect Effect { get; }

		public int? Seconds { get; }

		public int Amplifier { get; }

		public bool HideParticles { get; }

		protected override Type EqualityContract => typeof(GiveEffectCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			StringBuilder result = new StringBuilder($"/effect {Target.GetArgumentText()} {Effect.GetArgumentText()}");

			if (Seconds is not null || Amplifier > 0)
				result.Append($" {Seconds}");

			if (Amplifier > 0)
				result.Append($" {Amplifier}");

			if (HideParticles)
				result.Append(" true");

			return result.ToString();
		}
	}
}
