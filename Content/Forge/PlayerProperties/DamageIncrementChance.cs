using System;
using Terraria;

namespace Disarray.Core.Data
{
    public class DamageIncrementChance : PropertiesPlayer
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

        public DamageIncrementChance(float Chance)
        {
            this.Chance += Chance;
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