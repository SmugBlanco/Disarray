using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Basics
{
	public abstract class TemplatePlating : Materials
	{
		public abstract string Material { get; }

		public abstract int Rarity { get; }

		public abstract int Value { get; }

		public abstract float EffectStrength { get; }

		public abstract int CraftingMaterial { get; }

		public abstract int CraftingStation { get; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(Material + " Plating");
			Tooltip.SetDefault("Increases your chance to reduce incoming damage.");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.rare = Rarity;
			item.maxStack = 999;
			item.value = Value;
		}

		public override string GeneralDescription => "A hardened plate that can be utilised to protect and withstand enemy onslaught.";

		public override string ItemStatistics => "Increases chance of reducing incoming damage by " + (EffectStrength * 100f) + "%."
		+ "\nIf the chance exceeds 100%, the reduction is guarenteed and the remaining odds will go towards guaranteed + 1."
		+ "\nEffect stacks indefinitely.";

		public override string ObtainingGuide => "Fashion 8 " + Material.ToLower() + " bars into a plate at an anvil.";

		public override void ApplyToAllScenarios(Player player)
		{
			BasicStats stat = AutoloadedClass.CreateNewInstance<BasicStats>();
			stat.DefenseIncreaseChance += EffectStrength;
			PlayerProperty.ImplementProperty(player, stat, false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(CraftingMaterial, 8);
			recipe.AddTile(CraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}