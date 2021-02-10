using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Rusty
{
	public class RustySword : RustyItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Sword");
		}

		public override void NonProductDefaults()
		{
			item.width = 42;
			item.height = 42;
			item.maxStack = 999;
			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 42;
			item.height = 42;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 15;
			item.crit = 6;
			item.knockBack = 4;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 42;
			item.useAnimation = 42;
		}
	}
}