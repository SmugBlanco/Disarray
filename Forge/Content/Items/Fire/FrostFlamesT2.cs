using Disarray.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Fire
{
	public class FrostFlamesT2 : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frost Inferno");
			Tooltip.SetDefault("Allows attacks the ability to inflict 'Frostburn' onto target."
			+ "\nIncreases chance of inflicting said debuff.");
		}

		public override string GeneralDescription => "Ripe for a catharsis of embers.";

		public override string ItemStatistics => "Allows attacks the ability to inflict 'Frostburn' onto target." + "\nThis occurs with a default chance of 10%, and a default duration of 3 seconds." + "\nEach component increases inflict chance by 5%.";

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 500;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<FirePlayer>().FrostburnChance += 0.05f;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Torch, 1);
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.IceBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Torch, 1);
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.SnowBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FlamesT2>());
			recipe.AddIngredient(ItemID.IceBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FlamesT2>());
			recipe.AddIngredient(ItemID.SnowBlock, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}