using Disarray.Content.Reagents.Snow;
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
	public class IcicleMedium : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Medium Icicle");
		}

		public override string GeneralDescription => "Rarely found on the roofs of houses";

		public override string ItemStatistics => "Allows attacks to inflict 'Frostburn' onto targets." + "\nIncreases inflict chance by 2%" + "\nIncreases effect duration by 1 seconds" + "\nIncreases damage output on targets afflicted by 'Frostburn' by 4%";

		public override string ObtainingGuide => "Crafted from ice crystals and frozen mist at a source of water.";

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 34;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 1000;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Frostburn frostburnAttacks = AutoloadedClass.CreateNewInstance<Frostburn>();
			frostburnAttacks.InflictChance += 0.02f;
			frostburnAttacks.InflictDuration += 60;
			frostburnAttacks.DamageIncrease += 0.04f;
			PlayerProperty.ImplementProperty(player, frostburnAttacks, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FlawlessIceCrystal>(), 5);
			recipe.AddIngredient(ModContent.ItemType<FrozenMist>(), 10);
			recipe.needWater = true;
			recipe.SetResult(this);
		}
	}
}