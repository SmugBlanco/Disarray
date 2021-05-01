using Disarray.Forge.Content.Items.Materials.Standard;
using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Utility;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanStaff : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huntsman's Staff");
			Item.staff[item.type] = true;
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "6 template damage, 8 base damage, 10 max damage"
				+ "\n1 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(1f, true) + " )"
				+ "\n30 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(30, true) + " )"
				+ "\n6.66 base shoot speed"
				+ "\n10 mana consumption on use"
				+ "\nConjures numerous bolts of blood energy."
				+ "\nWhen forged, as long as the forge item's quality is equal to or above 33%, attacks gain a 25% chance to cause a hemorrhage. This is guarenteed on a critical strike."
				+ "\nIf said quality is equal to or above 50%, attacks gain a 15% damage boost to enemies that are currently hemorrhaging.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 6;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 40;
			item.height = 40;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item7;

			item.magic = true;
			item.damage = 8 + (int)(2f * quality);
			item.knockBack = 1;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;

			item.shoot = ModContent.ProjectileType<HuntsmanStaffProjectile>();
			item.shootSpeed = 6.66f;
			item.mana = 10;
		}

		public override void HoldItem(Player player)
		{
			if (ImplementedItem != null && ImplementedItem.Quality >= 0.33f)
			{
				player.GetModPlayer<HemorrhagePlayer>().HemorrhageChance += 0.25f;

				player.GetModPlayer<HemorrhagePlayer>().HemorrhageDuration += 300;

				if (ImplementedItem.Quality >= 0.5f)
				{
					player.GetModPlayer<HemorrhagePlayer>().HemorrhageDamageBoost += 0.15f;
				}
			}
		}

		public override IEnumerable<Projectile> ShootButBetter(Player player, Item baseItem, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float mousePlayerRotation = (player.Center - Main.MouseWorld).ToRotation() - MathHelper.PiOver4;
			ICollection<Projectile> firedProjectiles = new Collection<Projectile>();
			for (int count = 0; count < 5; count++)
			{
				Vector2 staffTipOffset = new Vector2(-66);

				switch (count)
				{
					case 1:
						staffTipOffset.X += 10;
						break;

					case 2:
						staffTipOffset.X += 20;
						staffTipOffset.Y -= 4;
						break;

					case 3:
						staffTipOffset.Y += 10;
						break;

					case 4:
						staffTipOffset.X -= 4;
						staffTipOffset.Y += 20;
						break;
				}

				Vector2 spawnPosition = player.Center + staffTipOffset.RotatedBy(mousePlayerRotation);
				float mouseSpawnRotation = (Main.MouseWorld - spawnPosition).ToRotation() - MathHelper.PiOver4;
				Vector2 velocity = Vector2.One.RotatedBy(mouseSpawnRotation) * item.shootSpeed;
				firedProjectiles.Add(Projectile.NewProjectileDirect(spawnPosition, velocity, type, damage, knockBack, player.whoAmI));
			}
			return firedProjectiles;
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