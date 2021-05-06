using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Technodrium
{
	public class TechnodriumBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Techndroium Bar");
			Tooltip.SetDefault("'Engineered using the best science in the world!'");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;

			item.rare = ItemRarityID.LightRed;

			item.maxStack = 999;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AdamantiteBar, 1);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TitaniumBar, 1);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}