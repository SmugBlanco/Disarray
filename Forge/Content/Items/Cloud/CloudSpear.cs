using Disarray.Forge.Content.Projectiles.Clouds;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Cloud
{
	public class CloudSpear : CloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud Spear");
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
				string ThrustSpeed = "Thrust Speed: " + item.shootSpeed;
				return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ThrustSpeed;
			}
		}

		public override void NonProductDefaults()
		{
			item.width = 46;
			item.height = 40;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 44;
			item.height = 44;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			
			item.melee = true;
			item.noMelee = true;
			item.damage = 18;
			item.crit = 4;
			item.knockBack = 2f;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<CloudSpearWeapon>();
			item.shootSpeed = 3.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 24);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}