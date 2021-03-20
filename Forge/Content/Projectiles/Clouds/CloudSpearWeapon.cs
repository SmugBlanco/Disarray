using Disarray.Content.ProjectileTypes;

namespace Disarray.Forge.Content.Projectiles.Clouds
{
	public class CloudSpearWeapon : Spear
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud Spear");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;

			projectile.aiStyle = 19;

			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 15;

			projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}
    }
}