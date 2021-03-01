using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Content.Gardening.SwordFern
{
	public class SwordFernsBlade : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword Fern's Blade");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player) => PlayerProperty.ImplementProperty(player, new DamageIncrementChance() { Chance = 0.25f }, false);

		public override void UpdateEquip(Player player) => PlayerProperty.ImplementProperty(player, new DamageIncrementChance() { Chance = 0.25f }, false);

		public override void UpdateAccessory(Player player, bool hideVisual) => PlayerProperty.ImplementProperty(player, new DamageIncrementChance() { Chance = 0.25f }, false);

		public override string ItemDescription() => "The blades from a matured Sword-Fern plant, it may have some uses in 'The Forge'.";

		public override string ItemStatistics() => "25% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Obtained from Sword Ferns at harvest.";

		public override string MiscDetails() => " ";
	}
}