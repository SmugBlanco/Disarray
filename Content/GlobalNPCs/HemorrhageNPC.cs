using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Disarray.Forge.Core.GlobalPlayers;
using System;

namespace Disarray.Forge.Core.GlobalNPCs
{
	public class HemorrhageNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public int PingTimer;

		public override void PostAI(NPC npc)
		{
			PingTimer--;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (npc.HasBuff(ModContent.BuffType<Hemorrhage>()))
			{
				if (npc.lifeRegen >= 0)
				{
					npc.lifeRegen = 0;
				}

				npc.lifeRegen -= 15;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (npc.HasBuff(ModContent.BuffType<Hemorrhage>()))
			{
				if (Main.rand.NextFloat() <= 0.1f)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, SpeedY: 3f);
				}
			}
		}

		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
		{
			if (npc.active && Main.player[npc.target].active && !Main.player[npc.target].dead && Main.player[npc.target].GetModPlayer<HemorrhagePlayer>().HuntersMark)
			{
				Texture2D huntersMark = ModContent.GetTexture("Disarray/Forge/Content/Items/Huntsman/HuntsmanGoggle_Mark");
				float sineFluctuation = (float)Math.Sin(Main.GameUpdateCount / 30f) * 10f;
				Vector2 drawWorldPosition = npc.Top + new Vector2(-huntersMark.Width / 2, sineFluctuation - huntersMark.Height * 1.5f);
				spriteBatch.Draw(huntersMark, drawWorldPosition - Main.screenPosition, Color.White);
			}
		}
	}
}