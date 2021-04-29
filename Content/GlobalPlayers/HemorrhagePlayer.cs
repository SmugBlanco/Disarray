using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Disarray.Core.Map;
using System;
using Disarray.Forge.Core.GlobalNPCs;
using System.Collections.Generic;

namespace Disarray.Forge.Core.GlobalPlayers
{
	public class HemorrhagePlayer : ModPlayer
	{
		public float HemorrhageChance;

		public int HemorrhageDuration;

		public float HemorrhageDamageBoost;

		public bool HuntersMark = false;

		public bool HeartBeatSensor = false;

		public int MaxHearBeatSensorRadius;

		public int CurrentHeartBeatSensorRadius
		{
			get
			{
				int tickDistance = MaxHearBeatSensorRadius / PingTime;
				int loopProgress = HeartBeatSensorPingTimer % (PingTime + PingInterval);
				if (loopProgress > PingTime)
				{
					return 0;
				}
				return (int)(tickDistance * loopProgress);
			}
		}

		public int HeartBeatSensorPingTimer;

		public int PingTime;

		public int PingInterval;

		public override void ResetEffects()
		{
			HemorrhageChance = 0;
			HemorrhageDuration = 0;
			HemorrhageDamageBoost = 0;
			HuntersMark = false;
			if (!HeartBeatSensor)
			{
				HeartBeatSensorPingTimer = 0;
			}
			HeartBeatSensor = false;
			MaxHearBeatSensorRadius = 0;
			PingTime = 0;
			PingInterval = 0;
		}

		public override void UpdateBadLifeRegen()
		{
			if (player.HasBuff(ModContent.BuffType<Hemorrhage>()))
			{
				if (player.lifeRegen > 0)
				{
					player.lifeRegen = 0;
				}

				player.lifeRegen -= 15;
			}
		}

		public override void PostUpdateMiscEffects()
		{
			Disarray disarray = ModContent.GetInstance<Disarray>();

			if (HeartBeatSensor)
			{
				HeartBeatSensorPingTimer++;

				Texture2D huntersPing = ModContent.GetTexture("Disarray/Forge/Content/Items/Huntsman/HuntsmanRadar_Ping");

				for (int index = 0; index < Main.maxNPCs; index++)
				{
					NPC npc = Main.npc[index];

					if (!npc.active || npc.townNPC)
					{
						continue;
					}

					HemorrhageNPC hemorrhageNPC = npc.GetGlobalNPC<HemorrhageNPC>();

					if (npc.DistanceSQ(player.Center) < Math.Pow(CurrentHeartBeatSensorRadius, 2) * 1.05f && npc.DistanceSQ(player.Center) > Math.Pow(CurrentHeartBeatSensorRadius, 2) * 0.95f)
					{
						hemorrhageNPC.PingTimer = PingTime;
					}

					if (hemorrhageNPC.PingTimer <= 0)
					{
						continue;
					}

					float alpha = hemorrhageNPC.PingTimer > PingTime * 0.25f ? 1f : hemorrhageNPC.PingTimer / (PingTime * 0.25f);
					disarray.MapEntries.Add(new MapEntry(huntersPing, npc.Center + new Vector2(0f, npc.gfxOffY), drawColor: Color.White * alpha, origin: new Vector2(6, 8)));
				}
			}
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.HasBuff(ModContent.BuffType<Hemorrhage>()))
			{
				damage = (int)(damage * (1 + HemorrhageDamageBoost));
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.HasBuff(ModContent.BuffType<Hemorrhage>()))
			{
				damage = (int)(damage * (1 + HemorrhageDamageBoost));
			}
		}

		public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
		{
			if (target.HasBuff(ModContent.BuffType<Hemorrhage>()))
			{
				damage = (int)(damage * (1 + HemorrhageDamageBoost));
			}
		}

		public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
		{
			if (target.HasBuff(ModContent.BuffType<Hemorrhage>()))
			{
				damage = (int)(damage * (1 + HemorrhageDamageBoost));
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (HemorrhageChance > 0 && (Main.rand.NextFloat() < HemorrhageChance | crit))
			{
				target.AddBuff(ModContent.BuffType<Hemorrhage>(), HemorrhageDuration);
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (HemorrhageChance > 0 && (Main.rand.NextFloat() < HemorrhageChance | crit))
			{
				target.AddBuff(ModContent.BuffType<Hemorrhage>(), HemorrhageDuration);
			}
		}

		public override void OnHitPvp(Item item, Player target, int damage, bool crit)
		{
			if (HemorrhageChance > 0 && (Main.rand.NextFloat() < HemorrhageChance | crit))
			{
				target.AddBuff(ModContent.BuffType<Hemorrhage>(), HemorrhageDuration);
			}
		}

		public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
		{
			if (HemorrhageChance > 0 && (Main.rand.NextFloat() < HemorrhageChance | crit))
			{
				target.AddBuff(ModContent.BuffType<Hemorrhage>(), HemorrhageDuration);
			}
		}
	}
}