using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public class HelmetMold : BlacksmithItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Head, (GetType().Namespace + "." + GetType().Name).Replace('.', '/') + "_Item");

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Helmet Mold");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "4 base defense, 5 max defense";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void NonProductDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.headSlot = -1;
			item.maxStack = 999;
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 18;
			item.height = 20;
			item.rare = ItemRarityID.Blue;
			SlotData.TryGetValue(base.item.type, out int slot);
			item.headSlot = slot;
		}

		public override void UpdateEquip(Player player) => player.statDefense += 4 + (int)(1f * ImplementedItem.Quality);
	}
}