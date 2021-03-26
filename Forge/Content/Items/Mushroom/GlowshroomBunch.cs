using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Mushroom
{
	public class GlowshroomBunch : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowshroom Bunch");
			Tooltip.SetDefault("Lights up the area near your cursor");
		}

		public override string GeneralDescription => "Not the most efficent way of distributing light, but it works better than a singular shroom.";

		public override string ItemStatistics => "Lights up the area surronding your cursor position by 0.3";

		public override string ObtainingGuide => "Crafted from multiple glowshrooms";

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 1000;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			Light light = AutoloadedClass.CreateNewInstance<Light>();
			light.CursorLightIntensity += 0.33f;
			PlayerProperty.ImplementProperty(player, light, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Glowshroom>(), 3);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}