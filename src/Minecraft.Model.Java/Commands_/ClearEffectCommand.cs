using System;
using System.Diagnostics;
using System.Text;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class ClearEffectCommand : Command
	{
		public ClearEffectCommand(StatusEffect? effect = null)
			: this(null, effect)
		{
		}

		public ClearEffectCommand(TargetEntity? target, StatusEffect? effect = null)
			: base("effect")
		{
			if (effect < StatusEffect.Speed || effect >= StatusEffect.HeroOfTheVillage)
				throw new ArgumentOutOfRangeException(nameof(effect), "Invalid status effect");

			Target = target;
			Effect = effect;
		}


		public TargetEntity? Target { get; }

		public StatusEffect? Effect { get; }

		protected override Type EqualityContract => typeof(ClearEffectCommand);

		private string DebuggerDisplay => ToString();


		public override string GetCommandText()
		{
			StringBuilder result = new StringBuilder($"/effect clear");

			if (Target is TargetEntity target)
				result.Append($" {Target.GetArgumentText()}");

			if (Effect is StatusEffect effect)
				result.Append($" {effect.GetArgumentText()}");

			return result.ToString();
		}
	}
}
