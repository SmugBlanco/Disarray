using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Projectiles.Clouds
{
	public class StormCloudYoyoProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 5f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 225;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 17.5f;
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;

			projectile.melee = true;
			projectile.penetrate = -1;

			projectile.aiStyle = 99;

			projectile.friendly = true;
			projectile.hostile = false;
		}
	}
}