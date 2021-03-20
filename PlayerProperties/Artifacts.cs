using Disarray.Core.Properties;
using Disarray.Forge.Content.Buffs.Desert;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.PlayerProperties
{
    public class Artifacts : PlayerProperty
    {
        public static float InnateApplyChance(Player player) => (player.ZoneDesert || player.ZoneUndergroundDesert) ? 0.2f : 0.1f;

        public static int InnateApplyDuration(Player player) => (player.ZoneDesert || player.ZoneUndergroundDesert) ? 600 : 360;

        public float ApplyChance;

        public int ApplyDuration;

        public float DamageIncrease;

        public int DamageIncreaseFlat;

        public float KnockbackIncrease;

        public float KnockbackIncreaseFlat;

        public float HealthIncrease;

        public int HealthIncreaseFlat;

        public float DefenseIncrease;

        public int DefenseIncreaseFlat;

        public float DamageReduction;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is Artifacts property)
            {
                ApplyChance += property.ApplyChance;
                ApplyDuration += property.ApplyDuration;
                DamageIncrease += property.DamageIncrease;
                DamageIncreaseFlat += property.DamageIncreaseFlat;
                KnockbackIncrease += property.KnockbackIncrease;
                KnockbackIncreaseFlat += property.KnockbackIncreaseFlat;
                HealthIncrease += property.HealthIncrease;
                HealthIncreaseFlat += property.HealthIncreaseFlat;
                DefenseIncrease += property.DefenseIncrease;
                DefenseIncreaseFlat += property.DefenseIncreaseFlat;
                DamageReduction += property.DamageReduction;
            }
        }

        public override void PostUpdateMiscEffects(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<SecretsOfTheSands>()))
            {
                player.allDamage += DamageIncrease;
                player.statLifeMax2 += (int)(player.statDefense * (1 + DefenseIncrease) + DefenseIncreaseFlat);
                player.statDefense = (int)(player.statDefense * (1 + DefenseIncrease) + DefenseIncreaseFlat);
                player.endurance += DamageReduction;
            }
        }

        public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < InnateApplyChance(player) + ApplyChance)
            {
                player.AddBuff(ModContent.BuffType<SecretsOfTheSands>(), InnateApplyDuration(player) + ApplyDuration);
            }
        }

        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < InnateApplyChance(player) + ApplyChance)
            {
                player.AddBuff(ModContent.BuffType<SecretsOfTheSands>(), InnateApplyDuration(player) + ApplyDuration);
            }
        }

        public override void OnHitPvp(Player player, Item item, Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat(1) < InnateApplyChance(player) + ApplyChance)
            {
                player.AddBuff(ModContent.BuffType<SecretsOfTheSands>(), InnateApplyDuration(player) + ApplyDuration);
            }
        }

        public override void OnHitPvpWithProj(Player player, Projectile proj, Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat(1) < InnateApplyChance(player) + ApplyChance)
            {
                player.AddBuff(ModContent.BuffType<SecretsOfTheSands>(), InnateApplyDuration(player) + ApplyDuration);
            }
        }

		public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<SecretsOfTheSands>()))
			{
                damage += DamageIncreaseFlat;
                knockback = knockback * (1 + KnockbackIncrease) + KnockbackIncreaseFlat;
			}
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<SecretsOfTheSands>()))
            {
                damage += DamageIncreaseFlat;
                knockback = knockback * (1 + KnockbackIncrease) + KnockbackIncreaseFlat;
            }
        }

        public override void ModifyHitPvp(Player player, Item item, Player target, ref int damage, ref bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<SecretsOfTheSands>()))
            {
                damage += DamageIncreaseFlat;
            }
        }

        public override void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<SecretsOfTheSands>()))
            {
                damage += DamageIncreaseFlat;
            }
        }
    }
}