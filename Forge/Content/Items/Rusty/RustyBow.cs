using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Rusty
{
	public class RustyBow : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Bow");
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
				string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
				string Ammunition = "Uses arrows as ammunition";
				return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Ammunition;
			}
		}

		public override void NonProductDefaults()
		{
			item.width = 20;
			item.height = 40;
			item.maxStack = 999;

			item.useStyle = 0;

			item.shoot = ProjectileID.None;
			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 22;
			item.height = 40;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item5;

			item.ranged = true;
			item.damage = 8;
			item.knockBack = 0.5f;
			item.crit = 6;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 45;
			item.useAnimation = 45;

			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 6.5f;
			item.useAmmo = AmmoID.Arrow;
		}
	}
}