using System.Collections.Generic;

namespace TomatoTool
{
	public class BattleBackgroundList
	{
		public BattleBackground this[int i]
		{
			get
			{
				return battleBackground[i];
			}

			set
			{
				battleBackground[i] = value;
			}
		}
		public int GetLength(int dimension)
		{
			return battleBackground.GetLength(dimension);
		}

		private BattleBackground[] battleBackground;
		public BattleBackground[] BattleBackground
		{
			get
			{
				return battleBackground;
			}
		}

		public const int BATTLE_BACKGROUND_MAX_LENGTH = 0x100;

		public BattleBackgroundList(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure);
		}

		public void load(TomatoAdventure tomatoAdventure)
		{
			battleBackground = new BattleBackground[0xFF];
		}

		public BattleBackground add(TomatoAdventure tomatoAdventure, Map map)
		{
			if (battleBackground[map.BattleBackgroundNumber] == null)
			{
				//battleBackground[map.BattleBackgroundNumber] = new BattleBackground(tomatoAdventure, map);
			}
			
			return battleBackground[map.BattleBackgroundNumber];
		}
	}
}
