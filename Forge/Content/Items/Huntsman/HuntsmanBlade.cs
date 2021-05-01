using Disarray.Forge.Content.Items.Materials.Standard;
using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Utility;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanBlade : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Huntsman's Blade");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "11 template damage, 17 base damage, 21 max damage"
				+ "\n6 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(6f, true) + " )"
				+ "\n12 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(12, true) + " )"
				+ "\nWhen forged, as long as the forge item's quality is equal to or above 33%, attacks gain a 25% chance to cause a hemorrhage. This is guarenteed on a critical strike."
				+ "\nIf said quality is equal to or above 50%, attacks gain a 25% damage boost to enemies that are currently hemorrhaging.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 11;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 54;
			item.height = 56;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 17 + (int)(4f * quality);
			item.knockBack = 6;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 12;
			item.useAnimation = 12;
		}

		public override void HoldItem(Player player)
		{
			if (ImplementedItem != null && ImplementedItem.Quality >= 0.33f)
			{
				player.GetModPlayer<HemorrhagePlayer>().HemorrhageChance += 0.25f;

				player.GetModPlayer<HemorrhagePlayer>().HemorrhageDuration += 300;

				if (ImplementedItem.Quality >= 0.5f)
				{
					player.GetModPlayer<HemorrhagePlayer>().HemorrhageDamageBoost += 0.25f;
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