using Disarray.Content.Reagents.Mushroom;
using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Mushroom
{
	public class Shineshroom : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shineshroom");
			Tooltip.SetDefault("Lights up the area near you");
		}

		public override string GeneralDescription => "Evolved to shine bright like a rare gemstone.";

		public override string ItemStatistics => "Lights up the area surronding you by 0.2";

		public override string ObtainingGuide => "Crafted from mushrooms and truffles at a workbench.";

		public override void SetDefaults()
		{
			item.width = 29;
			item.height = 26;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 500;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Light light = AutoloadedClass.CreateNewInstance<Light>();
			light.PlayerLightIntensity += 0.2f;
			PlayerProperty.ImplementProperty(player, light, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Truffle>(), 1);
			recipe.AddIngredient(ItemID.Mushroom, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}