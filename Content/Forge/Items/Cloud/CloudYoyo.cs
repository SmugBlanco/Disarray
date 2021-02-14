using Disarray.Content.Forge.Projectiles.Cloud;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Cloud
{
	public class CloudYoyo : CloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud Yoyo");
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
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + Conjures;
		}

		public override string ObtainingDetails() => "Crafted at a pool of water from solidified clouds, bound together by a few pieces of cobwebs.";

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
			item.damage = 15;
			item.crit = 4;
			item.knockBack = 2;
			item.channel = true;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<CloudYoyoProjectile>();
			item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 10);
			recipe.AddIngredient(ItemID.Cobweb, 8);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}