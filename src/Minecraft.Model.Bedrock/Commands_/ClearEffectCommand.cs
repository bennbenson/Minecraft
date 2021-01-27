using System;
using System.Diagnostics;

namespace Minecraft.Model.Bedrock
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class ClearEffectCommand : Command
	{
		public ClearEffectCommand(TargetPlayer target, StatusEffect? effect = null)
			: base("effect")
		{
			if (target is null)
				throw new ArgumentNullException(nameof(target));
			if (effect < StatusEffect.Speed || effect >= StatusEffect.HeroOfTheVillage)
				throw new ArgumentOutOfRangeException(nameof(effect), "Invalid status effect");

			Target = target;
			Effect = effect;
		}


		public TargetPlayer Target { get; }

		public StatusEffect? Effect { get; }

		protected override Type EqualityContract => typeof(ClearEffectCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			if (Effect is StatusEffect effect)
				return $"/effect {Target.GetArgumentText()} {effect.GetArgumentText()} 0";

			return $"/effect {Target.GetArgumentText()} clear";
		}
	}
}
