using Disarray.Core.Globals;
using System;
using System.Linq;
using Terraria;

namespace Disarray.Core.Data
{
    public class DefenseIncrementChance : PropertiesPlayer
    {
        public float Chance;

        public static void ImplementChance(Player player, float Chance)
        {
            DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
            PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DefenseIncrementChance);
            if (property is DefenseIncrementChance defenseIncrementChanceProperty)
            {
                defenseIncrementChanceProperty.Chance += Chance;
            }
            else
            {
                player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DefenseIncrementChance(Chance));
            }
        }

        public int DefenseIncrementFromDefenseIncrementChance
        {
            get
            {
                int Floor = (int)Math.Floor(Chance);
                float Randomizer = Main.rand.NextFloat(1);
                return Floor + (Randomizer <= (Chance - Floor) ? 1 : 0);
            }
        }

        public DefenseIncrementChance(float Chance)
        {
            this.Chance += Chance;
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