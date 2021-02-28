using Disarray.Content.Forge.Dusts.Misc;
using Disarray.Core.Data;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Projectiles.Properties
{
    public class Napalmed : ProjectileProperty
    {
        public float DefaultInflictChance = 0.2f;

        public float InflictChance;

        public float TotalInflictChance => DefaultInflictChance + InflictChance;

		public override void Combine(ProjectileProperty newProperty)
		{
			if (newProperty is Napalmed napalm)
			{
                InflictChance += napalm.InflictChance;
			}
		}

		public override void PostAI(Projectile projectile)
        {
            int Chance = projectile.height + projectile.width;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 15 == 0)
            {
                Dust.NewDust(projectile.Center - Vector2.One, 2, 2, ModContent.DustType<Napalm>(), projectile.velocity.X, projectile.velocity.Y, 0, default, 1f);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalInflictChance)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Misc.Napalmed>(), 900);
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalInflictChance)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Misc.Napalmed>(), 900);
            }
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Main.rand.NextFloat(1) < TotalInflictChance)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Misc.Napalmed>(), 900);
            }
        }
    }
}