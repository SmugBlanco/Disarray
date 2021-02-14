using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Forest
{
	public class WoodlandPistol : WoodlandItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodland Pistol");
		}

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			string Ammunition = "Uses bullets as ammunition";
			string Effect = "Increases life regeneration by 1 when held.";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Ammunition + "\n" + Effect;
		}

		public override void NonProductDefaults()
		{
			item.width = 32;
			item.height = 22;
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 38;
			item.height = 24;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 11;
			item.crit = 4;
			item.knockBack = 0.5f;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 45;
			item.useAnimation = 45;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 12;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 16);
			recipe.AddIngredient(ItemID.Mushroom, 3);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}