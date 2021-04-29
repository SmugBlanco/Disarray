using Disarray.Forge.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanRadar : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huntsman's Radar");
			Tooltip.SetDefault("When equipped, allows you to visualize the location of nearby enemies, via their hearbeats, on your map.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "When equipped, allows you to visualize the location of nearby enemies, via their hearbeats, on your map."
				+ "\nDetection Radius: 100 blocks"
				+ "\nPing Speed: 50 blocks per second"
				+ "\nPing Interval: 1 second"
				+ "\nWhen forged, gain a 5 block Detection Radius boost for each 10 quality percent.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 32;
			item.height = 30;
			item.rare = ItemRarityID.Green;

			item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			HemorrhagePlayer hemorrhagePlayer = player.GetModPlayer<HemorrhagePlayer>();
			hemorrhagePlayer.HeartBeatSensor = true;
			hemorrhagePlayer.MaxHearBeatSensorRadius += 1600;
			hemorrhagePlayer.PingTime += 120;
			hemorrhagePlayer.PingInterval += 60;

			if (ImplementedItem != null)
			{
				hemorrhagePlayer.MaxHearBeatSensorRadius += 80 * (int)(ImplementedItem.Quality * 10);
			}
		}
	}
}