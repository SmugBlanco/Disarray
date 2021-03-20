using Disarray.Core.Properties;
using System;
using Terraria;

namespace Disarray.Forge.Content.PlayerProperties
{
    public class DamageIncrementChance : PlayerProperty
    {
        public float Chance;

        public int DamageIncrementFromDamageIncrementChance
        {
            get
            {
                int Floor = (int)Math.Floor(Chance);
                float Randomizer = Main.rand.NextFloat(1);
                return Floor + (Randomizer <= (Chance - Floor) ? 1 : 0);
            }
        }

		public override void Combine(PlayerProperty newProperty)
		{
			if (newProperty is DamageIncrementChance property)
			{
                Chance += property.Chance;
			}
		}

		public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage += DamageIncrementFromDamageIncrementChance;
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            damage += DamageIncrementFromDamageIncrementChance;
        }
    }
}