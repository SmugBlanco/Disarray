using Disarray.Forge.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanGoggle : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huntsman's Goggles");
			Tooltip.SetDefault("When equipped, marks enemies that are currently targetting you.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "When equipped, marks enemies that are currently targetting you.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 52;
			item.height = 30;
			item.rare = ItemRarityID.Green;

			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<HemorrhagePlayer>().HuntersMark = true;
	}
}