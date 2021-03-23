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
	public class FrozenFist : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Fist");
			Tooltip.SetDefault("Allows attacks to inflict 'Frostburn'."
			+ "Increases knockback on enemies afflicted by 'Frostburn'.");
		}

		public override string GeneralDescription => "A fist made of ice, this is sure to be a long term investment!";

		public override string ItemStatistics => "Allows attacks to inflict 'Frostburn' onto targets." + "\nIncreases inflict chance by 2%" + "\nIncreases effect duration by 1 seconds" + "\nIncreases knockback on targets afflicted by 'Frostburn' by 5%";

		public override string ObtainingGuide => "Crafted from ice crystals at a source of water.";

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 250;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Frostburn frostburnAttacks = AutoloadedClass.CreateNewInstance<Frostburn>();
			frostburnAttacks.InflictChance += 0.02f;
			frostburnAttacks.InflictDuration += 60;
			frostburnAttacks.KnockbackIncrease += 0.05f;
			PlayerProperty.ImplementProperty(player, frostburnAttacks, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<IceCrystal>(), 5);
			recipe.needWater = true;
			recipe.SetResult(this);
		}
	}
}