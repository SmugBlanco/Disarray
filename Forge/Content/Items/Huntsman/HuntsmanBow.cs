using Disarray.Forge.Content.Items.Materials.Standard;
using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanBow : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Huntsman's Bow");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "16 template damage, 22 base damage, 27 max damage"
				+ "\n1 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(1f, true) + " )"
				+ "\n18 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(18, true) + " )"
				+ "\n7.5 base shoot speed"
				+ "\nWhen forged, as long as the forge item's quality is equal to or above 33%, attacks gain a 25% chance to cause a hemorrhage. This is guarenteed on a critical strike."
				+ "\nIf said quality is equal to or above 50%, attacks gain a 10% damage boost to enemies that are currently hemorrhaging.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 16;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 48;
			item.height = 66;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 22 + (int)(5f * quality);
			item.knockBack = 1;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 18;
			item.useAnimation = 18;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 7.5f;
			item.useAmmo = AmmoID.Arrow;
		}

		public override void HoldItem(Player player)
		{
			if (ImplementedItem != null && ImplementedItem.Quality >= 0.33f)
			{
				player.GetModPlayer<HemorrhagePlayer>().HemorrhageChance += 0.25f;

				player.GetModPlayer<HemorrhagePlayer>().HemorrhageDuration += 300;

				if (ImplementedItem.Quality >= 0.5f)
				{
					player.GetModPlayer<HemorrhagePlayer>().HemorrhageDamageBoost += 0.1f;
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 50);
			recipe.AddIngredient(ModContent.ItemType<FaunaT2>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}