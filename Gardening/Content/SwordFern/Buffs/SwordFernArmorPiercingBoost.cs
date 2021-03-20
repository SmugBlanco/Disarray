using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Gardening.Content.SwordFern.Buffs
{
	public class SwordFernArmorPiercingBoost : BuffProperties
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sword Fern Boost - Armor Piercing");
			Description.SetDefault("Increases armor penetration by 25");
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex) => player.armorPenetration += 25;
    }
}