using Disarray.Core.Properties;
using System;
using Terraria;

namespace Disarray.PlayerProperties
{
    public class BasicStats : PlayerProperty
    {
        public float DamageIncrease;

        public int DamageIncreaseFlat;

        public float DamageIncreaseChance;

        public float KnockbackIncrease;

        public float KnockbackIncreaseFlat;

        public float DefenseIncrease;

        public int DefenseIncreaseFlat;

        public float DefenseIncreaseChance;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is BasicStats property)
            {
                DamageIncrease += property.DamageIncrease;
                DamageIncreaseFlat += property.DamageIncreaseFlat;
                DamageIncreaseChance += property.DamageIncreaseChance;

                KnockbackIncrease += property.KnockbackIncrease;
                KnockbackIncreaseFlat += property.KnockbackIncreaseFlat;

                DefenseIncrease += property.DefenseIncrease;
                DefenseIncreaseFlat += property.DefenseIncreaseFlat;
                DefenseIncreaseChance += property.DefenseIncreaseChance;
            }
        }

        public int CalculateOutputOffChance(float input)
        {
            int Floor = (int)Math.Floor(input);
            return Floor + (Main.rand.NextFloat(1) <= (input - Floor) ? 1 : 0);
        }

        public override void PostUpdateMiscEffects(Player player)
		{
            player.allDamage += DamageIncrease;
            player.statDefense = (int)(player.statDefense * (1 + DefenseIncrease) + DefenseIncreaseFlat);
		}

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage += DamageIncreaseFlat + CalculateOutputOffChance(DamageIncreaseChance);
            knockback = knockback * (1 + KnockbackIncrease) + KnockbackIncreaseFlat;
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage += DamageIncreaseFlat + CalculateOutputOffChance(DamageIncreaseChance);
            knockback = knockback * (1 + KnockbackIncrease) + KnockbackIncreaseFlat;
        }

        public override void ModifyHitPvp(Player player, Item item, Player target, ref int damage, ref bool crit) => damage += DamageIncreaseFlat + CalculateOutputOffChance(DamageIncreaseChance);

        public override void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit) => damage += DamageIncreaseFlat + CalculateOutputOffChance(DamageIncreaseChance);

        public override void ModifyHitByNPC(Player player, NPC npc, ref int damage, ref bool crit) => damage -= CalculateOutputOffChance(DefenseIncreaseChance);

        public override void ModifyHitByProjectile(Player player, Projectile proj, ref int damage, ref bool crit) => damage -= CalculateOutputOffChance(DefenseIncreaseChance);
    }
}