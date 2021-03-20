using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Crafted
{
	public class Barrel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Barrel");
			Tooltip.SetDefault("'A barrel, possibly for storing contents? It has many uses.'");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.maxStack = 999;
			item.value = 500;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
		}
	}
}