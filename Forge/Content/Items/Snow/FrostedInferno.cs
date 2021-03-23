using Disarray.Content.Reagents.Caverns.Lava;
using Disarray.Content.Reagents.Snow.IceCrystal;
using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Snow
{
	public class FrostedInferno : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frosted Inferno");
			Tooltip.SetDefault("Allows attacks to inflict 'Frostburn'.");
		}

		public override string GeneralDescription => "A frosted inferno...";

		public override string ItemStatistics => "Allows attacks to inflict 'Frostburn' onto targets." + "\nIncreases inflict chance by 10%" + "\nIncreases effect duration by 9 seconds";

		public override string ObtainingGuide => "Crafted from perfect ice crystals and embers.";

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 36;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 10000;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Frostburn frostburnAttacks = AutoloadedClass.CreateNewInstance<Frostburn>();
			frostburnAttacks.InflictChance += 0.10f;
			frostburnAttacks.InflictDuration += 540;
			PlayerProperty.ImplementProperty(player, frostburnAttacks, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<PerfectIceCrystal>(), 5);
			recipe.AddIngredient(ModContent.ItemType<Embers>(), 100);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FrostedFlame>(), 3);
			recipe.AddIngredient(ModContent.ItemType<PerfectIceCrystal>(), 4);
			recipe.AddIngredient(ModContent.ItemType<Embers>(), 80);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FrostedSpark>(), 9);
			recipe.AddIngredient(ModContent.ItemType<PerfectIceCrystal>(), 4);
			recipe.AddIngredient(ModContent.ItemType<Embers>(), 80);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
		}
	}
}