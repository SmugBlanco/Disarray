using Disarray.Forge.Content.Items.Materials.Standard;
using Disarray.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 50);
			recipe.AddIngredient(ItemID.Glass, 20);
			recipe.AddIngredient(ModContent.ItemType<FaunaT2>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}