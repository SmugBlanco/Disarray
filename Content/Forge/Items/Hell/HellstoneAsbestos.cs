using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Hell
{
	public class HellstoneAsbestos : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellstone Asbestos");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.rare = ItemRarityID.Green;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			DamageIncrementChance.ImplementChance(player, 0.33f);
		}

		public override void UpdateEquip(Player player)
		{
			DamageIncrementChance.ImplementChance(player, 0.33f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DamageIncrementChance.ImplementChance(player, 0.33f);
		}

		public override string ItemDescription() => "You probably shouldn't be touching this, but the item may have some uses in 'The Forge'.";

		public override string ItemStatistics() => "33% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Grind up Hellstone Bars at an anvil.";

		public override string MiscDetails() => " ";

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}