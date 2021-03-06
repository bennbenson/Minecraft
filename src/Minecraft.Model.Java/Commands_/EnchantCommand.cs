﻿using System;
using System.Diagnostics;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class EnchantCommand : Command
	{
		public EnchantCommand(TargetEntity target, Enchantment enchantment, int level)
			: base("enchant")
		{
			if (target is null)
				throw new ArgumentNullException(nameof(target));
			if (enchantment < Enchantment.SweepingEdge || enchantment > Enchantment.SoulSpeed)
				throw new ArgumentOutOfRangeException(nameof(enchantment), "Invalid Enchantment value.");
			if (level < 0)
				throw new ArgumentOutOfRangeException(nameof(level), "Level cannot be negative.");

			Target = target;
			Enchantment = enchantment;
			Level = level;
		}


		public TargetEntity Target { get; }

		public Enchantment Enchantment { get; }

		public int Level { get; }


		protected override Type EqualityContract => typeof(EnchantCommand);

		private string DebuggerDisplay() => ToString();


		public override string GetCommandText() => $"/enchant {Target.GetArgumentText()} {Enchantment.GetArgumentText()} {Level}";
	}
}
