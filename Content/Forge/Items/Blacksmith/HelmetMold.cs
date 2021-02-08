using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class HelmetMold : Templates
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Head, item.modItem.Texture + "HelmetMold_Item");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Helmet Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom helmet");
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.headSlot = -1;
			item.maxStack = 999;
		}

		public override void SafeDefaults(Item newItem)
		{
			newItem.width = 20;
			newItem.height = 18;
			newItem.rare = ItemRarityID.Blue;
			SlotData.TryGetValue(item.type, out int slot);
			newItem.headSlot = slot;
		}
    }
}