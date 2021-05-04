using Disarray.Utility;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Atlastrium
{
	public class AtlastriumSMG : AtlastriumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlastrium Submachine Gun");
			Tooltip.SetDefault("It's fast fire rate makes it ideal for close quarters combat.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "10 template damage, 12 base damage, 16 max damage"
				+ "\n8 base use time and animation ( " + ItemUtilities.GetSpeedDescriptor(8, true) + " )"
				+ "\n14 base shoot speed"
				+ "\nWhen forged, gain 1 defense for every 25 quality percent.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults() => item.damage = 6;

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 46;
			item.height = 24;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 12 + (int)(4f * quality);

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 8;
			item.useAnimation = 8;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 14;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void HoldItem(Player player)
		{
			if (ImplementedItem != null)
			{
				player.statDefense += (int)(ImplementedItem.Quality * 4);
			}
		}

		public override IEnumerable<Projectile> ShootButBetter(Player player, Item baseItem, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 recoil = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
			return base.ShootButBetter(player, baseItem, item, ref position, ref recoil.X, ref recoil.Y, ref type, ref damage, ref knockBack);
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