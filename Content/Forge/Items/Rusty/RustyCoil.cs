using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Rusty
{
	public class RustyCoil : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Coil");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
		}

        public override void HoldItem(Player player)
        {
			DamageIncrementChance.ImplementChance(player, 0.2f);
		}

        public override void UpdateEquip(Player player)
        {
			DamageIncrementChance.ImplementChance(player, 0.2f);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			DamageIncrementChance.ImplementChance(player, 0.2f);
		}

        public override string ItemDescription() => "Seems like it use to be a multi-purpose chain. It's current condition is... poor but it may have some uses in 'The Forge'.";

		public override string ItemStatistics() => "20% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Found left behind in Wooden Chests and carried on the Undead.";

		public override string MiscDetails() => " ";
	}
}