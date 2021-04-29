using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanStaffProjectile : ModProjectile
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Blood Bolt");

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 180;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.alpha = 255;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				int spawnedDust = Dust.NewDust(projectile.position, 0, 0, DustID.Blood, 0, 0, 0, default, 1.5f);
				Main.dust[spawnedDust].velocity = projectile.velocity;
				Main.dust[spawnedDust].noGravity = true;
			}
		}
	}
}