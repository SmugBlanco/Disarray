using Terraria;
using Terraria.ModLoader;
using System;

namespace Disarray.Core.Forge
{
    public partial class ForgePlayer : ModPlayer
    {
        public float Damage = 1f;
        public int DamageFlat = 0;

        public override void ResetEffects()
        {
            Damage = 1f;
            DamageFlat = 0;
        }
    }
}