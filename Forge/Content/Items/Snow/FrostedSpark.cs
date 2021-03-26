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
	public class FrostedSpark : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frosted Spark");
			Tooltip.SetDefault("Allows attacks to inflict 'Frostburn'.");
		}

		public override string GeneralDescription => "The sparks to a begin a frosted inferno...";

		public override string ItemStatistics => "Allows attacks to inflict 'Frostburn' onto targets." + "\nIncreases inflict chance by 5%" + "\nIncreases effect duration by 1 seconds";

		public override string ObtainingGuide => "Crafted from ice crystals and embers.";

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 500;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Frostburn frostburnAttacks = AutoloadedClass.CreateNewInstance<Frostburn>();
			frostburnAttacks.InflictChance += 0.05f;
			frostburnAttacks.InflictDuration += 60;
			PlayerProperty.ImplementProperty(player, frostburnAttacks, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<IceCrystal>(), 5);
			recipe.AddIngredient(ModContent.ItemType<Embers>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}