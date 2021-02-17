using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Ocean
{
	public class SharkTooth : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shark Tooth");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.armorPenetration += 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.armorPenetration += 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.armorPenetration += 1;
		}

		public override string ItemDescription() => "A shark's tooth, evolutionarily designed to be one of nature's best knife, could be utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases Armor Penetration by 1";

		public override string ObtainingDetails() => "One of the few lasting items from a shark's corpse.";

		public override string MiscDetails() => " ";
	}
}