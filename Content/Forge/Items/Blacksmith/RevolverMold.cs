using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class RevolverMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Revolver Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom staff");
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
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;

			item.useStyle = 0;

			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 38;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item11;

			item.ranged = true;
			item.damage = 12;
			item.crit = 4;
			item.knockBack = 1f;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 45;
			item.useAnimation = 45;

			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 12;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}