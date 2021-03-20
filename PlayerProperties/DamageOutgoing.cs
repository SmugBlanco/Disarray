using Disarray.Core.Properties;
using Terraria;

namespace Disarray.PlayerProperties
{
    public class DamageOutgoing : PlayerProperty
    {
        public float DamageIncrease;

        public int DamageIncreaseFlat;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is DamageOutgoing property)
            {
                DamageIncrease += property.DamageIncrease;
                DamageIncreaseFlat += property.DamageIncreaseFlat;
            }
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit) => damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);

        public override void ModifyHitPvp(Player player, Item item, Player target, ref int damage, ref bool crit) =>  damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);

        public override void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit) => damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);
    }
}