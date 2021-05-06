using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Content.Buffs
{
	public class CruiseMissileCooldown : BuffProperties
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cruise Missile Cooldown");
            Description.SetDefault("'Sorry President Obama, you're going to have to wait a bit.'");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}