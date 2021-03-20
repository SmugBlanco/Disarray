using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Cloud
{
	public class CloudSword : CloudItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override string ItemStatistics
		{
			get
			{
				string Damage = "Damage: " + item.damage;
				string CritChance = "Crit Chance: " + item.crit + "%";
				string Knockback = "Knockback: " + item.knockBack;
				string UseTime = "Use Time: " + item.useTime;
				string UseAnimation = "Use Animation: " + item.useAnimation;
				string Effect = "";
				return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + Effect;
			}
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud Sword");
		}

		public override void NonProductDefaults()
		{
			item.width = 50;
			item.height = 44;
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
			item.damage = 14;
			item.crit = 4;
			item.knockBack = 2.5f;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 18;
			item.useAnimation = 18;
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