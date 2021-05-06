using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Disarray.Core.GlobalPlayers;

namespace Disarray.Forge.Core.GlobalNPCs
{
	public class TechnodriumNPC : GlobalNPC
	{
		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (Main.LocalPlayer.GetModPlayer<TechnodriumPlayer>().InformationalVisor)
			{
				drawColor = new Color(2.55f, 0.5f, 0.5f);
			}
		}
	}
}