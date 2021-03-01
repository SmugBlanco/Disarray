using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Disarray.Content.Gardening.SwordFern.Projectiles
{
	public abstract class SparkProjectile : ModProjectile
	{
        public virtual int DustType { get; private set; }

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.timeLeft = 180;

			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

			Dust spawnedDust = Dust.NewDustDirect(projectile.Center - new Vector2(4), 0, 0, DustType);
			spawnedDust.velocity = Vector2.Zero;
			spawnedDust.noGravity = true;
		}

		public override void Kill(int timeLeft)
		{
			for (int Indexer = 0; Indexer < 3; Indexer++)
			{
				Dust spawnedDust = Dust.NewDustDirect(projectile.position, 0, 0, DustType, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3));
				spawnedDust.noGravity = true;
			}
		}
	}
}