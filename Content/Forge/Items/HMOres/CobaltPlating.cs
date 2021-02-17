using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.HMOres
{
	public class CobaltPlating : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cobalt Plating");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.LightRed;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			DefenseIncrementChance.ImplementThis(player, 0.75f);
		}

		public override void UpdateEquip(Player player)
		{
			DefenseIncrementChance.ImplementThis(player, 0.75f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DefenseIncrementChance.ImplementThis(player, 0.75f);
		}

		public override string ItemDescription() => "Plating such as this one may have many purposes, a notable one is a usage in 'The Forge'.";

		public override string ItemStatistics() => "75% chance to decrease incoming damage by 1." + "\nIf the odds stack above 100%, the incoming damage decrease is guaranteed and the remaining odds will go towards guaranteed - 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Crafted from Cobalt Bars at an anvil.";

		public override string MiscDetails() => " ";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltBar, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}