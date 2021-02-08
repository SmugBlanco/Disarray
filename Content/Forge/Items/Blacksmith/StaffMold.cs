using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class StaffMold : Templates
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, item.modItem.Texture + "StaffMold_Weapon");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Staff Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom staff");
			Item.staff[item.type] = true;
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;
			item.useStyle = 0;
			item.shoot = ProjectileID.None;
			item.shootSpeed = 0;
			item.mana = 0;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 50;
			item.height = 50;
			item.magic = true;
			item.damage = 2;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.useAnimation = 30;
			item.crit = 4;
			item.rare = ItemRarityID.Blue;
			item.shoot = ProjectileID.HallowStar;
			item.shootSpeed = 8;
			item.mana = 2;
		}
	}
}