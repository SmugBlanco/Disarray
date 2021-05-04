using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Core.GlobalPlayers
{
	public class ArmorPlayer : ModPlayer
	{
		public bool ProtectiveSights = false;

		public bool ManOfStone = false;

		public float GetManOfStoneEffectiveness { get => ManOfStoneEffectiveness; set => ManOfStoneEffectiveness = Utils.Clamp(value, 0, 0.9f); }
		private float ManOfStoneEffectiveness = 0.9f;

		public int ManOfStoneTimer;

		public bool Steadfast = false;

		public override void ResetEffects()
		{
			ProtectiveSights = false;
			ManOfStone = false;
			Steadfast = false;
		}

		public override void PostUpdateMiscEffects()
		{
			if (ProtectiveSights)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					if (!Filters.Scene["ProtectiveSights"].IsActive())
					{
						Filters.Scene.Activate("ProtectiveSights");
					}
					else
					{
						float mouseDirection = player.DirectionTo(Main.MouseWorld).ToRotation();
						Vector2 eyePosition = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - 10);
						Effect screenShader = Filters.Scene["ProtectiveSights"].GetShader().Shader;
						screenShader.Parameters["active"].SetValue(true);
						screenShader.Parameters["point1"].SetValue(eyePosition);
						screenShader.Parameters["point2"].SetValue(eyePosition + new Vector2(Main.screenWidth, 0).RotatedBy(mouseDirection) + new Vector2(0, Main.screenWidth / 3).RotatedBy(mouseDirection));
						screenShader.Parameters["point3"].SetValue(eyePosition + new Vector2(Main.screenWidth, 0).RotatedBy(mouseDirection) - new Vector2(0, Main.screenWidth / 3).RotatedBy(mouseDirection));
					}
				}
			}
			else if (Main.netMode != NetmodeID.Server && Filters.Scene["ProtectiveSights"].IsActive())
			{
				Filters.Scene["ProtectiveSights"].GetShader().Shader.Parameters["active"].SetValue(false);
				Filters.Scene["ProtectiveSights"].Deactivate();
			}

			ManOfStoneTimer++;

			if (ManOfStoneTimer > 600)
			{
				ManOfStoneEffectiveness = 0.9f;
			}
		}

		public override void PostUpdateEquips()
		{
			if (ProtectiveSights)
			{
				player.statDefense *= 2;
			}

			if (ManOfStone)
			{
				player.GetModPlayer<SpeedPlayer>().MovementSpeedMultiplier *= 0.33f;
			}
		}

		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			if (ManOfStone && player.statLife <= player.statLifeMax2 * 0.1f)
			{
				damage = (int)(damage * (1 - ManOfStoneEffectiveness));
			}
		}

		public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
		{
			if (ManOfStone && player.statLife <= player.statLifeMax2 * 0.1f)
			{
				damage = (int)(damage * (1 - ManOfStoneEffectiveness));
			}
		}

		public override void OnHitByNPC(NPC npc, int damage, bool crit)
		{
			if (ManOfStone && player.statLife <= player.statLifeMax2 * 0.1f)
			{
				GetManOfStoneEffectiveness -= 0.1f;
				ManOfStoneTimer = 0;
			}
		}

		public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
		{
			if (ManOfStone && player.statLife <= player.statLifeMax2 * 0.1f)
			{
				GetManOfStoneEffectiveness -= 0.1f;
				ManOfStoneTimer = 0;
			}
		}

		public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			if (Steadfast && player.velocity.X == 0 && Math.Abs(player.velocity.Y) <= 0.4f)
			{
				player.immune = true;
				player.immuneTime += 60;
				player.immuneNoBlink = true;
				for (int k = 0; k < player.hurtCooldowns.Length; k++)
				{
					player.hurtCooldowns[k] += 60;
				}
			}
		}
	}
}