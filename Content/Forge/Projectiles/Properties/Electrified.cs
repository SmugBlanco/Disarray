using Disarray.Content.Forge.Dusts.Cloud;
using Disarray.Core.Data;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Projectiles.Properties
{
    public class Electrified : PropertiesProjectile
    {
        public override void PostAI(Projectile projectile)
        {
            int Chance = projectile.height + projectile.width;
            if (Main.rand.Next(Chance) == 0 || Main.GameUpdateCount % 15 == 0)
            {
                Dust.NewDust(projectile.Center - new Vector2(1), 2, 2, ModContent.DustType<Electricity>(), projectile.velocity.X, projectile.velocity.Y, 0, default, 1f);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit) 
        {
            float ChanceToInflict = 0.25f;
            if (target.wet || projectile.wet)
            {
                ChanceToInflict = 1f;
            }

            if (Main.rand.NextFloat(1) < ChanceToInflict)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Cloud.Electrified>(), 300);
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit) 
        {
            float ChanceToInflict = 0.25f;
            if (target.wet || projectile.wet)
            {
                ChanceToInflict = 1f;
            }

            if (Main.rand.NextFloat(1) < ChanceToInflict)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Cloud.Electrified>(), 300);
            }
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit) 
        {
            float ChanceToInflict = 0.25f;
            if (target.wet || projectile.wet)
            {
                ChanceToInflict = 1f;
            }

            if (Main.rand.NextFloat(1) < ChanceToInflict)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Cloud.Electrified>(), 300);
            }
        }
    }
}