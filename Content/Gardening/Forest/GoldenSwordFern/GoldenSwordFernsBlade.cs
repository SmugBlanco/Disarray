using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Terraria;

namespace Disarray.Content.Gardening.Forest.GoldenSwordFern
{
	public class GoldenSwordFernsBlade : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Golden Sword Fern's Blade");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			DamageIncrementChance.ImplementThis(player, 0.75f);
		}

		public override void UpdateEquip(Player player)
		{
			DamageIncrementChance.ImplementThis(player, 0.75f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DamageIncrementChance.ImplementThis(player, 0.75f);
		}

		public override string ItemDescription() => "The blades from a matured Golden Sword-Fern plant, it may have some uses in 'The Forge'.";

		public override string ItemStatistics() => "75% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Obtained from Golden Sword Ferns at harvest.";

		public override string MiscDetails() => " ";
	}
}