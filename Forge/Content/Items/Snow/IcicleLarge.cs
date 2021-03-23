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
	public class IcicleLarge : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Large Icicle");
			Tooltip.SetDefault("Allows attacks to inflict 'Frostburn'."
			+ "Increases damage on enemies afflicted by 'Frostburn'.");
		}

		public override string GeneralDescription => "A gargantuan creation of nature.";

		public override string ItemStatistics => "Allows attacks to inflict 'Frostburn' onto targets." + "\nIncreases inflict chance by 2%" + "\nIncreases effect duration by 1 seconds" + "\nIncreases damage output on targets afflicted by 'Frostburn' by 7.5%%";

		public override string ObtainingGuide => "Crafted from ice crystals and frozen mist at a source of water.";

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 76;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 10000;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Frostburn frostburnAttacks = AutoloadedClass.CreateNewInstance<Frostburn>();
			frostburnAttacks.InflictChance += 0.02f;
			frostburnAttacks.InflictDuration += 60;
			frostburnAttacks.DamageIncrease += 0.075f;
			PlayerProperty.ImplementProperty(player, frostburnAttacks, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<PerfectIceCrystal>(), 5);
			recipe.AddIngredient(ModContent.ItemType<FrozenMist>(), 100);
			recipe.needWater = true;
			recipe.SetResult(this);
		}
	}
}