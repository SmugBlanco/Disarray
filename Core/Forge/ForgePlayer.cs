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
		public float MaxHealthPercent = 0f;
		public int MaxHealthFlat = 0;

		public float DamageOutPercent = 0f;
		public int DamageFlat = 0;

		public float ResistPercent = 0f;
		public int ResistFlat = 0;

		/// <summary>
		/// Not sure about the implementation of these two since terraria already has it's own crit system
		/// </summary>
		public int CritRating = 0;
		public int BlockRating = 0;

		public float PiercePercent = 0;
		public int PierceFlat = 0;

		public override void ResetEffects()
        {
			MaxHealthPercent = 0f;
			MaxHealthFlat = 0;
			DamageOutPercent = 0f;
			DamageFlat = 0;
			ResistPercent = 0f;
			ResistFlat = 0;
			CritRating = 0;
			BlockRating = 0;
			PiercePercent = 0;
			PierceFlat = 0;
        }

        public override void PostUpdate()
        {
			player.statLifeMax2 = (int)(player.statLifeMax2 * (MaxHealthPercent + 1)) + MaxHealthFlat;	
		}

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
			damage = (int)(damage * (DamageOutPercent + 1)) + DamageFlat;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			damage = (int)(damage * (DamageOutPercent + 1)) + DamageFlat;
		}

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
			ResistPercent = ResistPercent > 1 ? 1 : ResistPercent;
			damage = (int)(damage * (1 - ResistPercent)) - ResistFlat;
			damage = damage < 0 ? 0 : damage;
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
    }
}