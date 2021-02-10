using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using System;
using System.Collections.Generic;
using Steamworks;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using Terraria.UI.Chat;

namespace Disarray.Core.Forge
{
	public class ForgePlayer : ModPlayer
	{
        public float Damage = 1f;
        public int DamageFlat = 0;

		public override void ResetEffects()
        {
            Damage = 1f;
            DamageFlat = 0;
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
			damage = (int)((float)damage * Damage) + DamageFlat;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			damage = (int)((float)damage * Damage) + DamageFlat;
		}
    }
}