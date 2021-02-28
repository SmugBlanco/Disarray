using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Dungeon
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

		public override void HoldItem(Player player) => PlayerProperty.ImplementProperty(player, new DefenseIncrementChance() { Chance = 0.5f }, false);

		public override void UpdateEquip(Player player) => PlayerProperty.ImplementProperty(player, new DefenseIncrementChance() { Chance = 0.5f }, false);

		public override void UpdateAccessory(Player player, bool hideVisual) => PlayerProperty.ImplementProperty(player, new DefenseIncrementChance() { Chance = 0.5f }, false);

		public override string ItemDescription() => "Could either be used to convict you for murder, or utilised in 'The Forge'.";

		public override string ItemStatistics() => "50% chance to decrease incoming damage by 1." + "\nIf the odds stack above 100%, the incoming damage decrease is guaranteed and the remaining odds will go towards guaranteed - 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Dropped from Skeletons.";

		public override string MiscDetails() => " ";
	}
}