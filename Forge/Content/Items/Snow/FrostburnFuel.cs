using Disarray.PlayerProperties;
using Disarray.Content.Reagents.Crafted;
using Disarray.Content.Reagents.Snow;
using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Snow
{
	public class FrostburnFuel : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostburn Fuel");
		}

		public override string GeneralDescription => "Often found on the roofs of houses";

		public override string ItemStatistics => "Allows attacks to extend the duration of 'Frostburn' on targets." + "\nIncreases duration extension chance by 20% seconds" + "\nIncreases duration extension by 1 seconds";

		public override string ObtainingGuide => "Created by storing frozen mist in a barrel.";

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 42;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.value = 100;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Frostburn frostburnAttacks = AutoloadedClass.CreateNewInstance<Frostburn>();
			frostburnAttacks.ExtendChance += 0.2f;
			frostburnAttacks.ExtendDuration += 60;
			PlayerProperty.ImplementProperty(player, frostburnAttacks, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Barrel>());
			recipe.AddIngredient(ModContent.ItemType<FrozenMist>(), 25);
			recipe.SetResult(this);
		}
	}
}