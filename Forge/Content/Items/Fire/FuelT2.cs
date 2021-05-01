using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Fire
{
	public class FuelT2 : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Wood");
			Tooltip.SetDefault("Increases duration of flame based debuffs when inflicted.");
		}

		public override string GeneralDescription => "Ripe for a catharsis of embers.";

		public override string ItemStatistics => "Each component increases duration of flame based debuffs when inflicted by 2 second.";

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 26;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 500;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<FirePlayer>().GeneralDuration += 120;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(ItemID.Wood, 50);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}