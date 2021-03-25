using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Basics
{
	public abstract class TemplatePowder : Materials
	{
		public abstract string Material { get; }

		public abstract int Rarity { get; }

		public abstract int Value { get; }

		public virtual bool AutomaticallyCalculateStrength { get; } = false;

		public abstract float EffectStrength { get; }

		public float ActualEffectStrength => AutomaticallyCalculateStrength ? 0.05f + (EffectStrength * 0.0025f) : EffectStrength;

		public abstract int CraftingMaterial { get; }

		public abstract int CraftingStation { get; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(Material + " Enriched Protein Powder");
			Tooltip.SetDefault("Increases your outgoing knockback."
			+ "\n'The label seems to say 'For Horses'.'");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 22;
			item.rare = Rarity;
			item.maxStack = 999;
			item.value = Value;
		}

		public override string GeneralDescription => "The label also includes the production location: 'Made in Russia'";

		public override string ItemStatistics => "Increases outgoing knockback by " + (ActualEffectStrength * 100f) + "% ";

		public override string ObtainingGuide => "Grind 8 " + Material.ToLower() + " bars into powder at an anvil.";

		public override void ApplyToAllScenarios(Player player)
		{
			BasicStats stat = AutoloadedClass.CreateNewInstance<BasicStats>();
			stat.KnockbackIncreaseFlat += ActualEffectStrength;
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