using Disarray.Core.Properties;
using System;
using Terraria;

namespace Disarray.Forge.Content.PlayerProperties
{
    public class DefenseIncrementChance : PlayerProperty
    {
        public float Chance;

        public int DefenseIncrementFromDefenseIncrementChance
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
            if (newProperty is DefenseIncrementChance property)
            {
                Chance += property.Chance;
            }
        }

        public override void ModifyHitByNPC(Player player, NPC npc, ref int damage, ref bool crit)
        {
            damage -= DefenseIncrementFromDefenseIncrementChance;

            if (damage < 0)
            {
                damage = 0;
            }
        }

        public override void ModifyHitByProjectile(Player player, Projectile proj, ref int damage, ref bool crit)
        {
            damage -= DefenseIncrementFromDefenseIncrementChance;

            if (damage < 0)
            {
                damage = 0;
            }
        }
    }
}