using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Crystal
{
	public class CrystallizedGeode : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystallized Geode");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.rare = ItemRarityID.Orange;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			DamageIncrementChance.ImplementChance(player, 0.5f);
		}

		public override void UpdateEquip(Player player)
		{
			DamageIncrementChance.ImplementChance(player, 0.5f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DamageIncrementChance.ImplementChance(player, 0.5f);
		}

		public override string ItemDescription() => "A gift from the earths below; it's crystalline features may have some purpose in 'The Forge'";

		public override string ItemStatistics() => "50% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Created through thousands, if not, millions of years of geological activity... Or you can just forcefully create one with some crystal shards and rocks.";

		public override string MiscDetails() => " ";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrystalShard, 5);
			recipe.AddIngredient(ItemID.StoneBlock, 10);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}