using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Fire
{
	public class FrostFlamingWhetstone : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frost Flaming Whetstone");
			Tooltip.SetDefault("Increases outgoing damage on enemies currently inflicted with 'Frostburn'.");
		}

		public override string GeneralDescription => "Ripe for a catharsis of embers.";

		public override string ItemStatistics => "Increases outgoing damage on enemies currently inflicted with 'Frostburn' by 3%.";

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 38;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 1000;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<FirePlayer>().FrostburnDamage += 0.03f;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Torch, 1);
			recipe.AddIngredient(ItemID.Gel, 25);
			recipe.AddIngredient(ItemID.IceBlock, 25);
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Torch, 1);
			recipe.AddIngredient(ItemID.Gel, 25);
			recipe.AddIngredient(ItemID.SnowBlock, 25);
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FrostFlames>());
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FrostFlamesT2>());
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Flames>());
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddIngredient(ItemID.IceBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Flames>());
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddIngredient(ItemID.SnowBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FlamesT2>());
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddIngredient(ItemID.IceBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FlamesT2>());
			recipe.AddIngredient(ItemID.ClayBlock, 25);
			recipe.AddIngredient(ItemID.SnowBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}