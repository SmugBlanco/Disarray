using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Content.Gardening.Forest.SwordFern.PlayerProperties
{
    public class TankSparkProperty : PlayerProperty
    {
        public override void ModifyHitByNPC(Player player, NPC npc, ref int damage, ref bool crit)
        {
            damage = (int)(damage * 0.75f);
            player.GetModPlayer<DisarrayGlobalPlayer>().ManuallyRemovedProperties.Remove(LoadedProperties[Type]);
        }

        public override void ModifyHitByProjectile(Player player, Projectile proj, ref int damage, ref bool crit)
        {
            damage = (int)(damage * 0.75f);
            player.GetModPlayer<DisarrayGlobalPlayer>().ManuallyRemovedProperties.Remove(LoadedProperties[Type]);
        }
    }
}