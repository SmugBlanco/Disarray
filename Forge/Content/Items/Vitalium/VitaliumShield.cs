using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Vitalium
{
	public class VitaliumShield : VitaliumItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Flora", 1f } };

		public override void SetStaticDefaults() => DisplayName.SetDefault("Vitalium Shield");

		public override string ItemStatistics
		{
			get
			{
				string statistic = "3 base defense when equipped"
				+ "\nWhen forged and equipped, you gain immunity to the debuff 'Poisoned'";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 54;
			item.height = 58;
			item.rare = ItemRarityID.Blue;

			item.accessory = true;

			item.defense = 3;
		}

		public override void UpdateEquip(Player player) => player.buffImmune[BuffID.Poisoned] = true;
	}
}