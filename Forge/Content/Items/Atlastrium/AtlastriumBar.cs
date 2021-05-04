using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Atlastrium
{
	public class AtlastriumBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlastrium Bar");
			Tooltip.SetDefault("A product of the earth's vast riches.");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;

			item.rare = ItemRarityID.Orange;

			item.maxStack = 999;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 3);
			recipe.AddRecipeGroup("Disarray:DemoniteBar", 1);
			recipe.AddRecipeGroup("Disarray:GoldBar", 1);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
			recipe.AddRecipeGroup("Disarray:CopperBar", 1);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this, 3);
			recipe.AddRecipe();
		}
	}
}