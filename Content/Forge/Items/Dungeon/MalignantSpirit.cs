using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Dungeon
{
	public class MalignantSpirit : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Malignant Spirit");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 34;
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			DamageIncrementChance.ImplementChance(player, 0.66f);
		}

		public override void UpdateEquip(Player player)
		{
			DamageIncrementChance.ImplementChance(player, 0.66f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DamageIncrementChance.ImplementChance(player, 0.66f);
		}

		public override string ItemDescription() => "Perhaps this is what makes dungeon spirits so hostile, it's properties could be utilised in 'The Forge'";

		public override string ItemStatistics() => "66% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Dropped from Dungeon Spirits.";

		public override string MiscDetails() => " ";
	}
}