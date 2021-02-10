using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Projectiles.Cloud
{
	public class CloudYoyoProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 5f; // vanilla ranges from 3f(Wood) to 16f(Chik) with -1 as infinite
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 250; // vanilla ranges from 130f(Wood) to 400f(Terrarian)
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 18; // vanilla ranges from 9f(Wood) to 17.5f(Terrarian)
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;

			projectile.melee = true;
			projectile.penetrate = -1;

			projectile.aiStyle = 99;

			projectile.friendly = true;
			projectile.hostile = false;
		}

        // notes for aiStyle 99: 
        // localAI[0] is used for timing up to YoyosLifeTimeMultiplier
        // localAI[1] can be used freely by specific types
        // ai[0] and ai[1] usually point towards the x and y world coordinate hover point
        // ai[0] is -1f once YoyosLifeTimeMultiplier is reached, when the player is stoned/frozen, when the yoyo is too far away, or the player is no longer clicking the shoot button.
        // ai[0] being negative makes the yoyo move back towards the player
        // Any AI method can be used for dust, spawning projectiles, etc specific to your yoyo.
    }
}