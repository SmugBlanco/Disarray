using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disarray.Content.Forge.Projectiles.Graveyard
{
	public class DeadMansSpark : ModProjectile
	{
		public int DustChance => 4 - (int)(3f * (projectile.timeLeft / 180f));

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dead Man's Spark");
		}

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.timeLeft = 180;

			projectile.penetrate = -1;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public override void AI()
		{
			projectile.velocity.X *= 0.99f;
			projectile.velocity.Y += 0.2f;

			for (int Indexer = 0; Indexer < 2; Indexer++)
			{
				if (Main.rand.Next(DustChance) == 0)
				{
					Dust spawnedDust = Dust.NewDustDirect(projectile.Center, 0, 0, 91);
					spawnedDust.velocity = new Vector2(0, -1f);
					spawnedDust.noGravity = true;
				}
			}
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			projectile.velocity = new Vector2(oldVelocity.X * 1.25f, oldVelocity.Y / -2);
			return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			return false;
        }
    }
}