using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class StaffMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Staff Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom staff");
			Item.staff[item.type] = true;
		}

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string Knockback = "Knockback: " + item.knockBack;
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			string Mana = "Uses " + item.mana + " mana per cast";
			return Damage + "\n" + CritChance + "\n" + Knockback + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Mana;
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;

			item.useStyle = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 50;
			item.height = 50;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item7;

			item.magic = true;
			item.damage = 12;
			item.crit = 4;
			item.knockBack = 3;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 32;
			item.useAnimation = 32;

			item.shoot = ProjectileID.HallowStar;
			item.shootSpeed = 8;
			item.mana = 2;
		}
	}
}