using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disarray.Content.Forge.Projectiles.Rusty
{
	public class MudGlob : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coprolite");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 60;

			projectile.penetrate = 2;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public override void AI()
		{
			projectile.velocity.X *= 0.995f;
			projectile.velocity.Y += 0.125f;

			for (int Indexer = 0; Indexer < 2; Indexer++)
			{
				Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 38, projectile.velocity.X, projectile.velocity.Y);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int Indexer = 0; Indexer < 3; Indexer++)
			{
				Dust.NewDustDirect(projectile.Center, 0, 0, 36, (projectile.velocity.X) + Main.rand.NextFloat(-3, 3), (projectile.velocity.Y / 2) + Main.rand.NextFloat(-3, 3));
			}

			for (int Indexer = 0; Indexer < 5; Indexer++)
			{
				Dust.NewDustDirect(projectile.position, (int)(projectile.width * 1.5), (int)(projectile.height * 1.5), 38, projectile.velocity.X + Main.rand.NextFloat(-2, 2), projectile.velocity.Y + Main.rand.NextFloat(-2, 2));
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			return false;
        }
    }
}