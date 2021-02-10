using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class LeggingMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Legs, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Item");

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

		public override string ItemStatistics() => "Defense: " + Defense;

		public override void SafeDefaults(Item item)
		{
			item.width = 22;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
            SlotData.TryGetValue(base.item.type, out int slot);
			item.legSlot = slot;

			Defense = 1;
		}
	}
}