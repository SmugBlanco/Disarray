using Disarray.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Poison
{
	public class PoisonedStrikes : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Poisoned Strikes");
			Tooltip.SetDefault("Increases outgoing damage on enemies currently 'Poisoned'");
		}

		public override string ItemStatistics => "Increases outgoing damage on enemies currently 'Poisoned', by 3%.";

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 38;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 250;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<PoisonPlayer>().PoisonousDamage += 0.03f;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Stinger, 4);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}