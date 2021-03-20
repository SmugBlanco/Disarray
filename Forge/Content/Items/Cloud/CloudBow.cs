using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Cloud
{
	public class CloudBow : CloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud Bow");
		}

		public override string ItemStatistics
		{
			get
			{
				string Damage = "Damage: " + item.damage;
				string CritChance = "Crit Chance: " + item.crit + "%";
				string Knockback = "Knockback: " + item.knockBack;
				string UseTime = "Use Time: " + item.useTime;
				string UseAnimation = "Use Animation: " + item.useAnimation;
				string ReuseDelay = "Reuse Delay: " + item.reuseDelay;
				string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
				string Ammunition = "Uses arrows as ammunition";
				return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ReuseDelay + "\n" + ShootSpeed + "\n" + Ammunition;
			}
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
			item.useAnimation = 20;
			item.reuseDelay = 20;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 15);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}