using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Forest
{
	public class WoodlandBow : WoodlandItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodland Bow");
		}

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			string Ammunition = "Uses arrows as ammunition";
			string Effect = "Increases life regeneration by 1 when held.";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Ammunition + "\n" + Effect;
		}

		public override void NonProductDefaults()
		{
			item.width = 22;
			item.height = 44;
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 22;
			item.height = 44;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 7;
			item.knockBack = 0.5f;
			item.crit = 4;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 40;
			item.useAnimation = 40;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 7f;
			item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 18);
			recipe.AddIngredient(ItemID.Mushroom, 2);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}