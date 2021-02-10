using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Rusty
{
	public class RustyPistol : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Pistol");
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
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Ammunition;
		}

		public override void NonProductDefaults()
		{
			item.width = 30;
			item.height = 22;
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 36;
			item.height = 22;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 10;
			item.crit = 6;
			item.knockBack = 0.8f;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 42;
			item.useAnimation = 42;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 12;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}