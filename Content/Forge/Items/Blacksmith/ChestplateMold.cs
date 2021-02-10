using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class ChestplateMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Body, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Item");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chestplate Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom chestplate");
		}

		public override string ItemStatistics() => "Defense: " + Defense;

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.bodySlot = -1;
			item.maxStack = 999;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 28;
			item.height = 26;
			item.rare = ItemRarityID.Blue;
			SlotData.TryGetValue(base.item.type, out int slot);
			item.bodySlot = slot;

			Defense = 2;
		}
	}
}