using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Disarray.Forge.Content.Projectiles.Clouds
{
	public class ExpelledCloudPiece : ModProjectile
	{
		public float CurrentPhase { get => projectile.ai[0]; set => projectile.ai[0] = value; }

		public float TargetedNPCIndex { get => projectile.ai[1]; set => projectile.ai[1] = value; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Expelled Cloud");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 34;
			projectile.timeLeft = 150;

			projectile.magic = true;
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

			if (CurrentPhase == 0)
			{
				projectile.velocity *= 0.99f;

				if (TargetedNPCIndex < 0)
				{
					for (int indexer = 0; indexer < Main.npc.Length - 1; indexer++)
					{
						NPC indexNPC = Main.npc[indexer];
						if (Vector2.Distance(projectile.Center, indexNPC.Center) < 240 && indexNPC.CanBeChasedBy(projectile))
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
						projectile.velocity += PosTo / 2;
					}
					else
					{
						TargetedNPCIndex = -1;
					}
				}
			}

			if (projectile.timeLeft < 30)
			{
				projectile.alpha += 8;
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

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (CurrentPhase == 0)
            {
				if (projectile.timeLeft > 30)
                {
					projectile.timeLeft = 30;
                }

				CurrentPhase = 1;
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
			if (CurrentPhase == 0)
			{
				if (projectile.timeLeft > 30)
				{
					projectile.timeLeft = 30;
				}

				CurrentPhase = 1;
			}
		}
	}
}