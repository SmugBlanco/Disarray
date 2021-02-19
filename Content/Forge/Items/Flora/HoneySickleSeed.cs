using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Flora
{
	public class HoneySickleSeed : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Honey Sickle Seed");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.createTile = mod.TileType("HoneySickle");

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.useTime = 15;
			item.useAnimation = 15;

			item.autoReuse = true;
			item.consumable = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HoneySickleFruit>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}