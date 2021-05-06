using Disarray.Content.Buffs;
using Disarray.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.GlobalPlayers
{
	public class TechnodriumPlayer : ModPlayer
	{
		public bool InformationalVisor = false;

		public bool MissileDefenseSystem = false;

		public bool RocketPoweredThrusters = false;

		public override void ResetEffects()
		{
			InformationalVisor = false;
			MissileDefenseSystem = false;
			RocketPoweredThrusters = false;
		}

		public override void PostUpdateMiscEffects()
		{
			if (RocketPoweredThrusters)
			{
				Player.jumpHeight += 5;
				Player.jumpSpeed += 3f;
				player.wingTime += 60;
			}

			if (MissileDefenseSystem)
			{
				if (player.HasBuff(ModContent.BuffType<CruiseMissileCooldown>()))
				{
					return;
				}

				bool CanLaunchMissile()
				{
					for (int indexer = 0; indexer < Main.maxNPCs; indexer++)
					{
						NPC npc = Main.npc[indexer];

						if (!npc.friendly && player.DistanceSQ(npc.Center) <= 640000)
						{
							return true;
						}
					}

					return false;
				}

				if ((Main.npc.Any(npc => npc.boss && npc.active) || (player.statLife != player.statLifeMax2)) && CanLaunchMissile())
				{
					Main.NewText("Launching Cruise Missile");
					float launchY = player.Center.Y - Main.screenHeight;
					if (launchY < 0)
					{
						launchY = 0;
					}
					Projectile.NewProjectile(new Vector2(player.Center.X, launchY), new Vector2(0, 16), ModContent.ProjectileType<CruiseMissile>(), 1000, 0f, player.whoAmI);
					player.AddBuff(ModContent.BuffType<CruiseMissileCooldown>(), 3600);
				}
			}
		}
	}
}