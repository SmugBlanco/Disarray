using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class SwordMold : BlacksmithItem
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
			DisplayName.SetDefault("Sword Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom sword");
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
			item.UseSound = SoundID.Item1;

			item.melee = true;
			item.damage = 12;
			item.crit = 4;
			item.knockBack = 5;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 28;
			item.useAnimation = 28;
		}
	}
}