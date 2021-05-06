using Disarray.Forge.Content.Items.Materials.Standard;
using Disarray.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanRadar : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huntsman's Radar");
			Tooltip.SetDefault("When equipped or held, allows you to visualize the location of nearby enemies, via their hearbeats, on your map.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "When equipped or held, allows you to visualize the location of nearby enemies, via their hearbeats, on your map."
				+ "\nDetection Radius: 100 blocks ( Held ), 50 blocks ( Equipped )"
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

		public override void HoldItem(Player player)
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

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			HemorrhagePlayer hemorrhagePlayer = player.GetModPlayer<HemorrhagePlayer>();
			hemorrhagePlayer.HeartBeatSensor = true;
			hemorrhagePlayer.MaxHearBeatSensorRadius += 800;
			hemorrhagePlayer.PingTime += 120;
			hemorrhagePlayer.PingInterval += 60;

			if (ImplementedItem != null)
			{
				hemorrhagePlayer.MaxHearBeatSensorRadius += 80 * (int)(ImplementedItem.Quality * 10);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 66);
			recipe.AddIngredient(ItemID.Wire, 25);
			recipe.AddIngredient(ModContent.ItemType<FaunaT2>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}