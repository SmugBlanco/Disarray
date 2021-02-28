using Disarray.Content.Forge.Projectiles.Properties;
using Disarray.Core.Globals;
using Disarray.Core.ProjectileClasses;
using Disarray.Core.Properties;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace Disarray.Content.Forge.Projectiles.Cloud
{
	public class StormCloudSpearWeapon : SpearProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Cloud Spear");
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