using Disarray.Content.Forge.Projectiles.Cloud;
using Disarray.Content.Forge.Projectiles.Properties;
using Disarray.Core.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Cloud
{
	public class StormCloudYoyo : CloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Cloud Yoyo");
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
		}

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string Conjures = "Throws itself out as a Yoyo on usage";
			string Effect = "Strikes are electrified and has a 25% of inflicting that onto enemies for 5 seconds." + "\nThis effect is guaranteed if the weapon or enemy is at least partially submerged in water.";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + Conjures + "\n" + Effect;
		}

		public override void NonProductDefaults()
		{
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 28;
			item.height = 20;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.noMelee = true;
			item.damage = 14;
			item.crit = 4;
			item.knockBack = 2;
			item.channel = true;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<StormCloudYoyoProjectile>();
			item.shootSpeed = 16f;
		}

		public override Projectile ShootButBetter(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile firedProjectile = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0, 0);
			firedProjectile.GetGlobalProjectile<DisarrayGlobalProjectile>().ActiveProperties.Add(new Electrified());
			return firedProjectile;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 10);
			recipe.AddIngredient(ItemID.RainCloud, 3);
			recipe.AddIngredient(ItemID.Cobweb, 8);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CloudYoyo>());
			recipe.AddIngredient(ItemID.RainCloud, 3);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}