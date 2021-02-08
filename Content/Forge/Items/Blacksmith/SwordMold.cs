using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class SwordMold : Templates
	{
		public override bool Autoload(ref string name) => AutoloadWeapon(name, item, string.Empty, item.modItem.Texture + "SwordMold_Weapon");

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
			item.melee = true;
			item.damage = 2;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 30;
			item.useAnimation = 30;
			item.crit = 4;
			item.rare = ItemRarityID.Blue;
		}
	}
}