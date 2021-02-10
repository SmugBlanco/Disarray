using Terraria;
using Terraria.ModLoader;
using System;

namespace Disarray.Core.Forge
{
    public partial class ForgePlayer : ModPlayer
    {
        public float Damage = 1f;
        public int DamageFlat = 0;
        public float DamageIncrementChance = 0;

        public int DamageIncrementFromDamageIncrementChance
        {
            get
            {
                int Floor = (int)Math.Floor(DamageIncrementChance);
                float Chance = Main.rand.NextFloat(1);
                return Floor + (Chance <= (DamageIncrementChance - Floor) ? 1 : 0);
            }
        }

        public override void ResetEffects()
        {
            Damage = 1f;
            DamageFlat = 0;
            DamageIncrementChance = 0;
        }
    }
}