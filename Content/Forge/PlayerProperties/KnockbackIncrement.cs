using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class KnockbackIncrement : PlayerProperty
    {
        public float knockBack;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is KnockbackIncrement property)
            {
                knockBack += property.knockBack;
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            knockback += knockBack;
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            knockback += knockBack;
        }
    }
}