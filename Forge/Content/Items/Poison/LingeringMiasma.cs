using Disarray.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Poison
{
	public class LingeringMiasma : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lingering Miasma");
			Tooltip.SetDefault("Allows attacks the ability to 'Poison' targets."
			+ "\nIncreases chance of inflicting said debuff.");
		}

		public override string ItemStatistics => "Each component increases duration of 'Poisoned' when inflicted by 1 second.";

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 250;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<PoisonPlayer>().PoisonousAttackDuration += 60;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Stinger, 1);
			recipe.AddIngredient(ItemID.Cloud, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}