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

		public override string ItemDescription() => "Serves as one of the most basic templates to create a custom item.";

		public override string ItemStatistics()
		{
			string Damage = "Damage: " + item.damage + DamageFlat;
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
			item.bodySlot = -1;
			item.maxStack = 999;
		}

		public override void SafeDefaults(Item newItem)
		{
			newItem.width = 28;
			newItem.height = 26;
			Defense = 1;
			newItem.rare = ItemRarityID.Blue;
			SlotData.TryGetValue(item.type, out int slot);
			newItem.bodySlot = slot;
		}
	}
}