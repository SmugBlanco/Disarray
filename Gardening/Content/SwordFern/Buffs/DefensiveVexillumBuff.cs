using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Gardening.Content.SwordFern.Buffs
{
	public class DefensiveVexillumBuff : BuffProperties
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Vexillum - Defensive");
			Description.SetDefault("Decreases incoming damage by 4%");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) => player.endurance += 0.04f;
	}
}