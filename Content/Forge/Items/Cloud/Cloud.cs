using Disarray.Core.Forge;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Cloud
{
	public class Cloud : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			if (Main.numClouds > 20)
			{
				player.endurance += 0.005f;
			}

			if (Main.numClouds > 80)
            {
				player.GetModPlayer<ForgePlayer>().DamageFlat += 1;
			}
		}

		public override void UpdateEquip(Player player)
		{
			if (Main.numClouds > 20)
			{
				player.endurance += 0.005f;
			}

			if (Main.numClouds > 80)
			{
				player.GetModPlayer<ForgePlayer>().DamageFlat += 1;
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (Main.numClouds > 20)
			{
				player.endurance += 0.005f;
			}

			if (Main.numClouds > 80)
			{
				player.GetModPlayer<ForgePlayer>().DamageFlat += 1;
			}
		}

		public override string ItemDescription() => "Somehow you got your hands on a cloud; traditonally they're suppose to be just water droplets, so how it's staying in your hands is a mystery.";

		public override string ItemStatistics() => "Increases damage output by 1 if the skies are at least cloudy." + "\nIncreases damage reduction by 0.5% if the skies are at least partly cloudy.";

		public override string ObtainingDetails() => "Crafted at a pool of water from solidified clouds; can also be found carried on various high altitude enemies.";

		public override string MiscDetails() => " ";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 8);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}