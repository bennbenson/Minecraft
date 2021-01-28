using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Model.Java
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
	public class ParticleCommand : Command
	{
		public ParticleCommand(string effectName, string[]? parameters, PositionF? position, PositionF? delta, float speed, int count, ParticleMode? particleMode, TargetEntity? viewers)
			: base("particle")
		{
			if (effectName is null)
				throw new ArgumentNullException(nameof(effectName));
			if (effectName.Length == 0)
				throw new ArgumentException("Effect name cannot be empty.", nameof(effectName));

			EffectName = effectName;
			Parameters = parameters is not null ? ImmutableArray.Create(parameters) : null;
			Position = position;
			Delta = delta;
			Speed = speed;
			Count = count;
			ParticleMode = particleMode;
			Viewers = viewers;
		}


		public string EffectName { get; }

		public ImmutableArray<string>? Parameters { get; }

		public PositionF? Position { get; }

		public PositionF? Delta { get; }

		public float Speed { get; }

		public int Count { get; }

		public ParticleMode? ParticleMode { get; }

		public TargetEntity? Viewers { get; }


		protected override Type EqualityContract => typeof(ParticleCommand);

		private string DebuggerDisplay => ToString();

		public override string GetCommandText()
		{
			StringBuilder result = new StringBuilder($"/particle {EffectName.GetArgumentText()}");

			// Without access to the Java version, I can't verify that this is creating
			// the proper output.  For now, the syntax information suggests that this is
			// correct.  However, I've seen it be wrong before.

			if (Parameters is not null)
			{
				foreach (string parameter in Parameters)
				{
					result.Append(' ');
					result.Append(parameter.GetArgumentText());
				}
			}

			if (Position is PositionF position)
			{
				result.Append(' ');
				result.Append(position.GetArgumentText());
			}

			if (Delta is PositionF delta)
			{
				result.Append(' ');
				result.Append(delta.GetArgumentText());
			}

			result.Append(' ');
			result.Append(Speed);

			result.Append(' ');
			result.Append(Count);

			if (ParticleMode is Java.ParticleMode particleMode)
			{
				result.Append(' ');
				result.Append(particleMode.GetArgumentText());
			}


			return result.ToString();
		}
	}
}
