using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Disarray.Content.Forge.Projectiles.Marble
{
	public class MarbleEnergyOrb : ModProjectile
	{
		public float TargetedNPCIndex { get => projectile.ai[1]; set => projectile.ai[1] = value; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Energy Orb");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 180;

			projectile.penetrate = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public override void AI()
		{
			projectile.rotation += MathHelper.ToRadians(2f);

			Dust spawnedDust = Dust.NewDustDirect(projectile.position, 0, 0, Main.GameUpdateCount % 4 == 0 ? 87 : 91);
			spawnedDust.velocity = Vector2.Zero;
			spawnedDust.noGravity = true;

			if (projectile.timeLeft < 30)
			{
				projectile.alpha += 8;
			}

			if (TargetedNPCIndex < 0)
			{
				for (int indexer = 0; indexer < Main.npc.Length - 1; indexer++)
				{
					NPC indexNPC = Main.npc[indexer];
					if (Vector2.Distance(projectile.Center, indexNPC.Center) < 180 && indexNPC.CanBeChasedBy(projectile))
					{
						TargetedNPCIndex = indexer;
						break;
					}
				}
			}
			else
			{
				NPC npc = Main.npc[(int)TargetedNPCIndex];
				if (npc.CanBeChasedBy(projectile))
				{
					Vector2 PosTo = (npc.Center - projectile.Center);
					PosTo.Normalize();
					projectile.velocity += PosTo / 3;
				}
				else
				{
					TargetedNPCIndex = -1;
				}
			}
		}

		public override bool? CanHitNPC(NPC target)
		{
			return projectile.alpha < byte.MaxValue / 2 ? base.CanHitNPC(target) : false;
		}

		public override bool CanHitPvp(Player target)
		{
			return projectile.alpha < byte.MaxValue / 2 ? base.CanHitPvp(target) : false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			for (int i = 0; i < projectile.oldPos.Length; i++)
			{
				Main.spriteBatch.Draw(texture, projectile.oldPos[i] - Main.screenPosition + new Vector2(projectile.width * 0.5f, projectile.height * 0.5f) + new Vector2(0f, projectile.gfxOffY), null, projectile.GetAlpha(lightColor) * (1f / i), projectile.rotation, new Vector2(projectile.width * 0.5f, projectile.height * 0.5f), projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}