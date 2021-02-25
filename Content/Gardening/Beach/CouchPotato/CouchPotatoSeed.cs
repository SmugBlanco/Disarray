using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Core.Gardening.Items;

namespace Disarray.Content.Gardening.Beach.CouchPotato
{
	public class CouchPotatoSeed : SeedItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Couch Potato Seed");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 20;
			item.maxStack = 999;
			item.createTile = ModContent.TileType<CouchPotatoPlant>();

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
			recipe.AddIngredient(ModContent.ItemType<CouchPotatoSpud>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}