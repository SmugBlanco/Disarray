using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Buffs
{
    public class BattleMarked : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Battle Marked");
            Description.SetDefault("'Being hunted.'");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}