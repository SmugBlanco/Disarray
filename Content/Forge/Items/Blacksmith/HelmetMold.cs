using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class HelmetMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Head, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Item");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Helmet Mold");
			Tooltip.SetDefault("Serves as a basic template for creating a custom helmet");
		}

		public override string ItemStatistics() => "Defense: " + Defense;

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.headSlot = -1;
			item.maxStack = 999;
		}

		public override void SafeDefaults(Item item)
		{
			item.width = 20;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
            SlotData.TryGetValue(base.item.type, out int slot);
			item.headSlot = slot;

			Defense = 1;
		}
    }
}