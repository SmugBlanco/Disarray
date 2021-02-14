using Disarray.Core.ProjectileClasses;

namespace Disarray.Content.Forge.Projectiles.Forest
{
	public class WoodlandPole : PoleProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodland Pole");
		}

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 64;
			projectile.timeLeft = 30;

			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 15;

			projectile.ownerHitCheck = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.friendly = true;
		}
	}
}