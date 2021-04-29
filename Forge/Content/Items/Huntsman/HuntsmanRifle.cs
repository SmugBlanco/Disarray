using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Utility;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanRifle : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huntsman's Rifle");
			Tooltip.SetDefault("A powerful double barreled assault rifle, capable of lethal close range damage and substantial distance accuracy.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "6 template damage, 9 base damage, 11 max damage"
				+ "\n1 base knockback ( " + ItemUtilities.GetKnockbackDescriptor(1f, true) + " )"
				+ "\n15 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(15, true) + " )"
				+ "\n15 base shoot speed"
				+ "\nWhen forged, as long as the forge item's quality is equal to or above 33%, attacks gain a 20% chance to cause a hemorrhage. This is guarenteed on a critical strike."
				+ "\nIf said quality is equal to or above 50%, attacks gain a 10% damage boost to enemies that are currently hemorrhaging.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 6;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 38;
			item.height = 22;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 9 + (int)(2f * quality);
			item.knockBack = 1;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 15;
			item.useAnimation = 15;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 15;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void HoldItem(Player player)
		{
			if (ImplementedItem != null && ImplementedItem.Quality >= 0.33f)
			{
				player.GetModPlayer<HemorrhagePlayer>().HemorrhageChance += 0.2f;

				player.GetModPlayer<HemorrhagePlayer>().HemorrhageDuration += 300;

				if (ImplementedItem.Quality >= 0.5f)
				{
					player.GetModPlayer<HemorrhagePlayer>().HemorrhageDamageBoost += 0.1f;
				}
			}
		}

		public override IEnumerable<Projectile> ShootButBetter(Player player, Item baseItem, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = new Vector2(56, 0).RotatedBy((Main.MouseWorld - player.Center).ToRotation());
			ICollection<Projectile> firedProjectiles = new Collection<Projectile>();
			for (int count = 0; count < 2; count++)
			{
				firedProjectiles.Add(Projectile.NewProjectileDirect(player.Center + muzzleOffset, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)), type, damage, knockBack, player.whoAmI));
			}
			return firedProjectiles;
		}
	}
}