using Disarray.Content.Reagents.Desert;
using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Desert
{
	public class Lotus : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lotus");
			Tooltip.SetDefault("Allows attacks to grant 'Secrets of the Sand'"
			+ "\nWhile 'Secrets of the Sand' is active your life regeneration increases by 1.");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 32;
			item.rare = ItemRarityID.Orange;
			item.maxStack = 999;
			item.value = 10000;
		}

		public override string GeneralDescription => "It seems the artifact had magical properties";

		public override string ItemStatistics => "Allows attacks a default 10% ( 20% while in a desert ) chance to imbue you with 'Secrets of the Sands' for 6 ( 10 while in a desert ) seconds" + "\nWhile 'Secrets of the Sands' is active, you'll be resistant to sandstorms and find a movement speed buff of 10%" + "\nIncreases life regeneration by 1 while 'Secrets of the Sands' is in effect.";

		public override string ObtainingGuide => "Obtained by uncovering the secrets of an artifact...";

		public override void ApplyToAllScenarios(Player player)
		{
			Artifacts artifacts = AutoloadedClass.CreateNewInstance<Artifacts>();
			artifacts.LifeRegenerationIncrease += 1;
			PlayerProperty.ImplementProperty(player, artifacts, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Artifact>());
			recipe.AddIngredient(ModContent.ItemType<Fulgurite>());
			recipe.SetResult(this);

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Artifact>());
			recipe.AddIngredient(ModContent.ItemType<DustDevil>(), 25);
			recipe.needWater = true;
			recipe.SetResult(this);
		}
	}
}