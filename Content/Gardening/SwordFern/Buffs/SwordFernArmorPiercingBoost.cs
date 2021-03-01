using Disarray.Core.Data;
using Terraria;

namespace Disarray.Content.Gardening.SwordFern.Buffs
{
	public class SwordFernArmorPiercingBoost : DisarrayBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sword Fern Boost - Armor Piercing");
			Description.SetDefault("Increases armor penetration by 25");
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.armorPenetration += 25;
        }
    }
}