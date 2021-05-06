using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Disarray.Core.GlobalPlayers;

namespace Disarray.Content.Projectiles
{
	public class CruiseMissile : ModProjectile
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Cruise Missile");

		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 9999;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public float Target { get => projectile.ai[0]; set => projectile.ai[0] = value; }

		public override void AI()
		{
			Target = -1;
			int currentMostHealth = 0;
			for (int index = 0; index < Main.maxNPCs; index++)
			{
				NPC npc = Main.npc[index];

				if (!npc.active || npc.friendly || npc.life <= 0|| npc.DistanceSQ(Main.player[projectile.owner].Center) > 1000000)
				{
					continue;
				}

				if (npc.DistanceSQ(projectile.Center) <= 1444)
				{
					projectile.Kill();
				}

				if (npc.life > currentMostHealth)
				{
					Target = index;
					currentMostHealth = npc.life;
				}
			}

			if (Target >= 0)
			{
				Vector2 direction = projectile.DirectionTo(Main.npc[(int)Target].Center);
				projectile.velocity = direction * 24;
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

			int dustIndex = Dust.NewDust(projectile.Center + new Vector2(0, 86).RotatedBy(projectile.rotation), 0, 0, DustID.Fire, 0f, 0f, 100, default, Main.rand.NextFloat(2f, 4f));
			Main.dust[dustIndex].noGravity = true;
			Main.dust[dustIndex].velocity = Vector2.Zero;
		}

		public override void Kill(int timeLeft)
		{
			for (int index = 0; index < Main.maxNPCs; index++)
			{
				NPC npc = Main.npc[index];

				if (!npc.active || npc.friendly || npc.dontTakeDamage)
				{
					continue;
				}

				float distance = npc.Distance(projectile.Center);

				if (distance < 800)
				{
					if (Main.player[projectile.owner].accDreamCatcher)
					{
						Main.player[projectile.owner].addDPS(projectile.damage);
					}

					npc.StrikeNPC(projectile.damage, 8f, Math.Sign(npc.Center.X - Main.player[projectile.owner].Center.X));
				}
				else if (distance < 1600)
				{
					int damage = (int)(projectile.damage / 800f * (distance - 800));

					if (damage > 0)
					{
						if (Main.player[projectile.owner].accDreamCatcher)
						{
							Main.player[projectile.owner].addDPS(damage);
						}

						npc.StrikeNPC(damage, 8f, Math.Sign(npc.Center.X - Main.player[projectile.owner].Center.X));
					}
				}
			}

			for (int index = 0; index < Main.maxPlayers; index++)
			{
				Player player = Main.player[index];

				if (!player.active || player.dead)
				{
					continue;
				}

				float distance = player.Distance(projectile.Center);

				if (distance < 1600)
				{
					float distanceReversed = 1600 - distance;
					ScreenshakePlayer screenshakePlayer = player.GetModPlayer<ScreenshakePlayer>();
					screenshakePlayer.Duration = (int)(distanceReversed / 10);
					screenshakePlayer.Intensity = (int)(distanceReversed / 200) + 1;

					FlashbangPlayer flashbangPlayer = player.GetModPlayer<FlashbangPlayer>();
					flashbangPlayer.FlashbangTimer = (int)(distanceReversed / 40);
					flashbangPlayer.GetFlashbangFadeTime = (int)(distanceReversed / 16);
				}
			}

			Main.PlaySound(SoundID.Item14, projectile.Center);

			for (int i = 0; i < 33; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X - 50, projectile.Center.Y - 50), 100, 100, DustID.Smoke, 0f, 0f, 100, default, 2f);
				Main.dust[dustIndex].velocity *= Main.rand.NextFloat(0.5f, 2f);
			}

			for (int i = 0; i < 100; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X - 50, projectile.Center.Y - 50), 100, 100, DustID.Smoke, 0f, 0f, 100, default, 2f);
				Main.dust[dustIndex].velocity *= Main.rand.NextFloat(3.33f, 16f);
			}

			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X - 100, projectile.Center.Y - 100), 100, 100, DustID.Fire, 0f, 0f, 100, default, 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.Center.X - 100, projectile.Center.Y - 100), 100, 100, DustID.Fire, 0f, 0f, 100, default, 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}

			for (int g = 0; g < 100; g++)
			{
				int goreIndex = Gore.NewGore(projectile.Center + new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)), default, Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = Main.rand.NextFloat (1f, 2f);
				Main.gore[goreIndex].velocity = new Vector2(Main.rand.NextFloat(-24f, 24f), Main.rand.NextFloat(-24f, 24f));
				Main.gore[goreIndex].timeLeft = 360;
			}
		}

		public override bool? CanHitNPC(NPC target) => false;

		public override bool CanHitPvp(Player target) => false;
	}
}