using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Buffs
{
	public class Hemorrhage : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hemorrhage");
            Description.SetDefault("'Bleeding profusely...'");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}