using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class LeggingMold : Templates
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Legs, item.modItem.Texture + "LeggingMold_Item");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Legging Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom legging");
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.legSlot = -1;
			item.maxStack = 999;
		}

		public override void SafeDefaults(Item newItem)
		{
			newItem.width = 22;
			newItem.height = 18;
			newItem.rare = ItemRarityID.Blue;
			SlotData.TryGetValue(item.type, out int slot);
			newItem.legSlot = slot;
		}
	}
}