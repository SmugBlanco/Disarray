using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Lunar
{
	public class BreathOfGalaxies : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Breath of Galaxies");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.rare = ItemRarityID.Cyan;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player) => PlayerProperty.ImplementProperty(player, new DamageIncrementChance() { Chance = 1f }, false);

		public override void UpdateEquip(Player player) => PlayerProperty.ImplementProperty(player, new DamageIncrementChance() { Chance = 1f }, false);

		public override void UpdateAccessory(Player player, bool hideVisual) => PlayerProperty.ImplementProperty(player, new DamageIncrementChance() { Chance = 1f }, false);

		public override string ItemDescription() => "";

		public override string ItemStatistics() => "100% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Crafted by combining every Lunar Fragment near a pool of lava";

		public override string MiscDetails() => " ";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentNebula, 2);
			recipe.AddIngredient(ItemID.FragmentSolar, 2);
			recipe.AddIngredient(ItemID.FragmentStardust, 2);
			recipe.AddIngredient(ItemID.FragmentVortex, 2);
			recipe.needLava = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}