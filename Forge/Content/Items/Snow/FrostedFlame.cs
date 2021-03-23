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
	public class FrostedFlame : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frosted Flame");
			Tooltip.SetDefault("Allows attacks to inflict 'Frostburn'.");
		}

		public override string GeneralDescription => "The flames of a frosted inferno...";

		public override string ItemStatistics => "Allows attacks to inflict 'Frostburn' onto targets." + "\nIncreases inflict chance by 7.5%" + "\nIncreases effect duration by 3 seconds";

		public override string ObtainingGuide => "Crafted from flawless ice crystals and embers.";

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 28;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 5000;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Frostburn frostburnAttacks = AutoloadedClass.CreateNewInstance<Frostburn>();
			frostburnAttacks.InflictChance += 0.075f;
			frostburnAttacks.InflictDuration += 180;
			PlayerProperty.ImplementProperty(player, frostburnAttacks, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FlawlessIceCrystal>(), 5);
			recipe.AddIngredient(ModContent.ItemType<Embers>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FrostedSpark>(), 3);
			recipe.AddIngredient(ModContent.ItemType<FlawlessIceCrystal>(), 4);
			recipe.AddIngredient(ModContent.ItemType<Embers>(), 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
		}
	}
}