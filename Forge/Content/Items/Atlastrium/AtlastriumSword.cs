using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Atlastrium
{
	public class AtlastriumSword : AtlastriumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Atlastrium Sword");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "38 template damage, 40 base damage, 50 max damage"
				+ "\n5 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(5f, true) + " )"
				+ "\n25 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(25, true) + " )"
				+ "\nWhen forged, gain 1 defense for every 25 quality percent.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 38;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 46;
			item.height = 46;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 40 + (int)(10f * quality);
			item.knockBack = 5;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 25;
			item.useAnimation = 25;
		}

		public override void HoldItem(Player player)
		{
			if (ImplementedItem != null)
			{
				player.statDefense += (int)(ImplementedItem.Quality * 4);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AtlastriumBar>(), 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}