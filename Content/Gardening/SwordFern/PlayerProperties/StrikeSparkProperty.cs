using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Content.Gardening.SwordFern.PlayerProperties
{
    public class StrikeSparkProperty : PlayerProperty
    {
        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (crit)
            {
                damage = (int)(damage * 5f);
            }
            player.GetModPlayer<DisarrayGlobalPlayer>().ManuallyRemovedProperties.Remove(GetLoadedData[Type] as PlayerProperty);
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (crit)
            {
                damage = (int)(damage * 2.5f);
            }
            player.GetModPlayer<DisarrayGlobalPlayer>().ManuallyRemovedProperties.Remove(GetLoadedData[Type] as PlayerProperty);
        }
    }
}