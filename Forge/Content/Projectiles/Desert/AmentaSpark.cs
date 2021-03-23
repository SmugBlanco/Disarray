using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using DustID = Disarray.ID.DustID;

namespace Disarray.Forge.Content.Projectiles.Desert
{
	public class AmentaSpark : ModProjectile
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Amenta Spark");

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.timeLeft = 60;

			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public override void AI()
		{
			projectile.velocity.X *= 0.995f;
			projectile.velocity.X *= 0.995f;
			Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.AmberBolt, projectile.velocity.X, projectile.velocity.Y, Scale : 0.75f);
			dust.noGravity = true;
			dust.velocity = Vector2.Zero;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffID.OnFire, 300);

		public override void OnHitPvp(Player target, int damage, bool crit) => target.AddBuff(BuffID.OnFire, 300);

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => false;
    }
}