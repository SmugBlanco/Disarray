using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Forest
{
	public class WoodlandPole : WoodlandItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodland Pole");
		}

		public override void NonProductDefaults()
		{
			item.width = 34;
			item.height = 40;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 48;
			item.height = 48;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.noMelee = true;
			item.damage = 6;
			item.crit = 4;
			item.knockBack = 2.5f;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 30;
			item.useAnimation = 30;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<Projectiles.Forest.WoodlandPole>();
			item.shootSpeed = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 20);
			recipe.AddIngredient(ItemID.Mushroom, 2);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}