using Disarray.Core.Data;
using Disarray.Core.Globals;
using System.Linq;
using Terraria;

namespace Disarray.Content.Forge.PlayerProperties
{
    public class KnockbackIncrement : PropertiesPlayer
    {
        public static void ImplementKB(Player player, float kB)
        {
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is KnockbackIncrement);
            if (property is KnockbackIncrement knockBackIncrement)
            {
                knockBackIncrement.knockBack += kB;
            }
            else
            {
                player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new KnockbackIncrement(kB));
            }
        }

        public float knockBack;

        public KnockbackIncrement(float kB)
        {
            knockBack += kB;
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