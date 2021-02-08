using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class ChestplateMold : Templates
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Body, item.modItem.Texture + "ChestplateMold_Item");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chestplate Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom chestplate");
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.bodySlot = -1;
			item.maxStack = 999;
		}

		public override void SafeDefaults(Item newItem)
		{
			newItem.width = 28;
			newItem.height = 26;
			newItem.rare = ItemRarityID.Blue;
			SlotData.TryGetValue(item.type, out int slot);
			newItem.bodySlot = slot;
		}
	}
}