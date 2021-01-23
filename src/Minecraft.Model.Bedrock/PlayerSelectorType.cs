using System;

namespace Minecraft.Model.Bedrock
{
	public enum PlayerSelectorType
	{
		Proximate = 'p',
		Random = 'r',
		All = 'a',
		Entity = 'e',
		Self = 's',
		PlayerAgent = 'c',
		AllAgents = 'v'
	}

	public static class PlayerSelectorTypeExtensions
	{
		public static bool IsValid(this PlayerSelectorType value)
		{
			return value switch
			{
				PlayerSelectorType.Proximate or
					PlayerSelectorType.Random or
					PlayerSelectorType.All or
					PlayerSelectorType.Entity or
					PlayerSelectorType.Self or
					PlayerSelectorType.PlayerAgent or
					PlayerSelectorType.AllAgents => true,
				_ => false,
			};
		}

		public static PlayerSelectorType AsPlayerSelectorType(this char value) => (PlayerSelectorType)value;

		public static PlayerSelectorType ToPlayerSelectorType(this char value)
		{
			PlayerSelectorType playerSelectorType = (PlayerSelectorType)value;

			return playerSelectorType.IsValid() ? playerSelectorType : throw new ArgumentOutOfRangeException(nameof(value), message: null);
		}
	}
}
