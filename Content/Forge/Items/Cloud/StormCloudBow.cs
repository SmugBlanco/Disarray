using Disarray.Content.Forge.Projectiles.Properties;
using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Cloud
{
	public class StormCloudBow : StormCloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Cloud Bow");
		}

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ReuseDelay = "Reuse Delay: " + item.reuseDelay;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			string Ammunition = "Uses arrows as ammunition";
			string Effect = "Fired arrows are electrified and has a 25% of inflicting that onto enemies for 5 seconds." + "\nThis effect is guaranteed if the arrow or enemy is at least partially submerged in water.";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ReuseDelay + "\n" + ShootSpeed + "\n" + Ammunition + "\n" + Effect; 
		}

		public override void NonProductDefaults()
		{
			item.width = 34;
			item.height = 78;
			item.maxStack = 999;

			item.useStyle = 0;

			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 36;
			item.height = 78;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 13;
			item.knockBack = 1f;
			item.crit = 4;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 10;
			item.useAnimation = 25;
			item.reuseDelay = 25;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
		}

		public override void ModifyFiredProjectiles(Projectile projectile) => ProjectileProperty.ImplementProperty(projectile, AutoloadedClass.CreateNewInstance<Electrified>());

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 15);
			recipe.AddIngredient(ItemID.RainCloud, 3);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CloudBow>());
			recipe.AddIngredient(ItemID.RainCloud, 3);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}