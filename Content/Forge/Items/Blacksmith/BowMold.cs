using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class BowMold : Templates
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, item.modItem.Texture + "BowMold_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bow Mold");
		}

		public override string ItemDescription() => "Serves as one of the most basic templates to create a custom item.";

        public override string ItemStatistics()
        {
			string Damage = "Damage: " + (item.damage + DamageFlat);
			string CritChance = "Crit Chance: " + item.crit + "%";
			string UseTime = "Use Time: " + item.useTime;
			string UseAnimation = "Use Animation: " + item.useAnimation;
			string ShootSpeed = "Shoot Speed: " + item.shootSpeed;
			string Ammunition = "Uses arrows as ammunition";
			return Damage + "\n" + CritChance + "\n" + UseTime + "\n" + UseAnimation + "\n" + ShootSpeed + "\n" + Ammunition;
		}

        public override string ObtainingDetails() => "Purchasable from your local Blacksmith.";

		public override string MiscDetails() => "Most materials and components effects are boosted on molds";

        public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;
			item.useStyle = 0;
			item.shoot = ProjectileID.None;
			item.shootSpeed = 0;
			item.useAmmo = AmmoID.None;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 34;
			item.height = 34;
			item.ranged = true;
			item.damage = 2;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;
			item.crit = 4;
			item.rare = ItemRarityID.Blue;
			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 7;
			item.useAmmo = AmmoID.Arrow;
		}
    }
}