using Disarray.Core.Globals;
using Disarray.Core.Properties;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace Disarray.PlayerProperties
{
    public class Frostburn : PlayerProperty
    {
        public float InflictChance;

        public int InflictDuration;

        public float ExtendChance;

        public int ExtendDuration;

        public float DamageIncrease;

        public int DamageIncreaseFlat;

        public float KnockbackIncrease;

        public float KnockbackIncreaseFlat;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is Frostburn property)
            {
                InflictChance += property.InflictChance;
                InflictDuration += property.InflictDuration;
                ExtendChance += property.ExtendChance;
                ExtendDuration += property.ExtendDuration;
                DamageIncrease += property.DamageIncrease;
                DamageIncreaseFlat += property.DamageIncreaseFlat;
                KnockbackIncrease += property.KnockbackIncrease;
                KnockbackIncreaseFlat += property.KnockbackIncreaseFlat;
            }
        }

		public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < InflictChance)
            {
                target.AddBuff(BuffID.Frostburn, InflictDuration);
            }

            if (target.HasBuff(BuffID.Frostburn) && Main.rand.NextFloat(1) < ExtendChance)
			{
                target.buffTime[target.FindBuffIndex(BuffID.Frostburn)] += ExtendDuration;
            }
        }

		public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
            if (Main.rand.NextFloat(1) < InflictChance)
            {
                target.AddBuff(BuffID.Frostburn, InflictDuration);
            }

            if (target.HasBuff(BuffID.Frostburn) && Main.rand.NextFloat(1) < ExtendChance)
            {
                target.buffTime[target.FindBuffIndex(BuffID.Frostburn)] += ExtendDuration;
            }
        }

		public override void OnHitPvp(Player player, Item item, Player target, int damage, bool crit)
		{
            if (Main.rand.NextFloat(1) < InflictChance)
            {
                target.AddBuff(BuffID.Frostburn, InflictDuration);
            }

            if (target.HasBuff(BuffID.Frostburn) && Main.rand.NextFloat(1) < ExtendChance)
            {
                target.buffTime[target.FindBuffIndex(BuffID.Frostburn)] += ExtendDuration;
            }
        }

		public override void OnHitPvpWithProj(Player player, Projectile proj, Player target, int damage, bool crit)
		{
            if (Main.rand.NextFloat(1) < InflictChance)
            {
                target.AddBuff(BuffID.Frostburn, InflictDuration);
            }

            if (target.HasBuff(BuffID.Frostburn) && Main.rand.NextFloat(1) < ExtendChance)
            {
                target.buffTime[target.FindBuffIndex(BuffID.Frostburn)] += ExtendDuration;
            }
        }

		public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.HasBuff(BuffID.Frostburn))
			{
                damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);
                knockback = knockback * (1 + KnockbackIncrease) + KnockbackIncreaseFlat;
            }
		}

		public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
            if (target.HasBuff(BuffID.Frostburn))
            {
                damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);
                knockback = knockback * (1 + KnockbackIncrease) + KnockbackIncreaseFlat;
            }
        }

		public override void ModifyHitPvp(Player player, Item item, Player target, ref int damage, ref bool crit)
		{
            if (target.HasBuff(BuffID.Frostburn))
            {
                damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);
            }
        }

		public override void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit)
		{
            if (target.HasBuff(BuffID.Frostburn))
            {
                damage = (int)(damage * (1 + DamageIncrease) + DamageIncreaseFlat);
            }
        }
	}
}