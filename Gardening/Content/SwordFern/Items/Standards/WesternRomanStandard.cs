using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Microsoft.Xna.Framework;
using Disarray.Gardening.Content.SwordFern.Buffs;

namespace Disarray.Gardening.Content.SwordFern.Items.Standards
{
	public class WesternRomanStandard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Offensive Vexillum");
			Tooltip.SetDefault("Grants a 4% outgoing damage buff for 10 seconds when held");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 40;

			item.rare = ItemRarityID.Blue;

			item.holdStyle = 1;
		}

		public override void HoldItem(Player player)
		{
			player.itemLocation += new Vector2(player.direction == 1 ? -16 : 16, 4);

			foreach (Player teammates in from nearbyTeammates in Main.player where nearbyTeammates.DistanceSQ(player.Center) < 90000 select nearbyTeammates)
			{
				teammates.AddBuff(ModContent.BuffType<OffensiveVexillumBuff>(), 600);
			}
		}
	}
}