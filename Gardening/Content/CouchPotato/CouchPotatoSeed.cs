using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Gardening.Core.Items;
using Disarray.Gardening.Core;

namespace Disarray.Gardening.Content.CouchPotato
{
	public class CouchPotatoSeed : SeedItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Couch Potato Seed");
		}

		public override GardeningInformation GeneralInformation { get; protected set; } = new GardeningInformation("Disarray/Gardening/Content/CouchPotato/CouchPotato", "Couch Potato", "Testing", 0.5f, 2, (0, 5));

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