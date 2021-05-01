using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Poison
{
	public class RarePoisonVial : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rare Poison Vial");
			Tooltip.SetDefault("Increases the duration of 'Poison' inflicted.");
		}

		public override string ItemStatistics => "Allows attacks the ability to inflict 'Poisoned' onto target." + "\nThis occurs with a default chance of 10%, and a default duration of 3 seconds." + "\nEach component increases inflict chance by 5%.";

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 30;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 250;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<PoisonPlayer>().PoisonousAttackChance += 0.05f;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Stinger, 10);
			recipe.AddIngredient(ItemID.Bottle, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}