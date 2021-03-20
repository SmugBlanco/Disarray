using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Disarray.Forge.Content.Projectiles.Clouds
{
	public class StormNimbus : ModProjectile
	{
		public int RainTimer { get => (int)projectile.ai[0]; set => projectile.ai[0] = value; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Nimbus");
			Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = 54;
			projectile.height = 28;
			projectile.timeLeft = 240;

			projectile.penetrate = -1;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			if (++projectile.frameCounter >= 10)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}

			projectile.velocity = Vector2.Zero;

			if (projectile.timeLeft < 30)
			{
				projectile.alpha += 8;
			}
			else if (++RainTimer > 30 && Main.netMode != NetmodeID.MultiplayerClient)
            {
				Projectile.NewProjectile(projectile.Center + new Vector2(0, projectile.height / 2), new Vector2(0, 8), ProjectileID.RainFriendly, projectile.damage, projectile.knockBack, projectile.owner);
            }
		}

		public override bool? CanHitNPC(NPC target) => false;

		public override bool CanHitPlayer(Player target) => false;

		public override bool CanHitPvp(Player target) => false;
    }
}