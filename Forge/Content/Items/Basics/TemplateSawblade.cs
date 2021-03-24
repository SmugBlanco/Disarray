using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Basics
{
	public abstract class TemplateSawblade : Materials
	{
		public abstract string Material { get; }

		public abstract int Rarity { get; }

		public abstract int Value { get; }

		public abstract float EffectStrength { get; }

		public abstract int CraftingMaterial { get; }

		public abstract int CraftingStation { get; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(Material + " Sawblade");
			Tooltip.SetDefault("Increases your chance to increase outgoing damage.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = Rarity;
			item.maxStack = 999;
			item.value = Value;
		}

		public override string GeneralDescription => "A sawblade, perhaps it sharpens your weapons? Dunno. All we know is that something has happened to increase your weapon's lethality.";

		public override string ItemStatistics => "Increases chance of increasing outgoing damage by " + (EffectStrength * 100) +"%."
		+ "\nIf the chance exceeds 100%, the increase is guarenteed and the remaining odds will go towards guaranteed + 1."
		+ "\nEffect stacks indefinitely.";

		public override string ObtainingGuide => "Fashion 8 " + Material.ToLower() + " bars into a sawblade at an anvil.";

		public override void ApplyToAllScenarios(Player player)
		{
			BasicStats stat = AutoloadedClass.CreateNewInstance<BasicStats>();
			stat.DamageIncreaseChance += 0.50f;
			PlayerProperty.ImplementProperty(player, stat, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(CraftingMaterial, 8);
			recipe.AddTile(CraftingStation);
			recipe.SetResult(this);
		}
	}
}