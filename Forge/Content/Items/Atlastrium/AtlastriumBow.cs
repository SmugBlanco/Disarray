using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Atlastrium
{
	public class AtlastriumBow : AtlastriumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Atlastrium Bow");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "42 template damage, 45 base damage, 55 max damage"
				+ "\n2 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(2f, true) + " )"
				+ "\n32 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(32, true) + " )"
				+ "\n9 base shoot speed"
				+ "\nWhen forged, gain 1 defense for every 25 quality percent.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 42;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 48;
			item.height = 66;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 45 + (int)(10f * quality);
			item.knockBack = 2;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 32;
			item.useAnimation = 32;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
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