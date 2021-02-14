using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Bones
{
	public class SkeletonsFemur : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Human Femur");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.rare = ItemRarityID.Green;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			DefenseIncrementChance.ImplementChance(player, 0.5f);
		}

		public override void UpdateEquip(Player player)
		{
			DefenseIncrementChance.ImplementChance(player, 0.5f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DefenseIncrementChance.ImplementChance(player, 0.5f);
		}

		public override string ItemDescription() => "Could either be used to convict you for murder, or utilised in 'The Forge'.";

		public override string ItemStatistics() => "50% chance to decrease incoming damage by 1." + "\nIf the odds stack above 100%, the incoming damage decrease is guaranteed and the remaining odds will go towards guaranteed - 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Dropped from Skeletons.";

		public override string MiscDetails() => " ";
	}
}