using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Gardening.Content.SwordFern.Buffs
{
	public class OffensiveVexillumBuff : BuffProperties
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Vexillum - Offensive");
			Description.SetDefault("Increases outgoing damage by 4%");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) => player.allDamage += 0.04f;
	}
}