using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.DukeFishron
{
	public class FishronsTusk : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishron's Tusk");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 16;
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.armorPenetration += 3;
		}

		public override void UpdateEquip(Player player)
		{
			player.armorPenetration += 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.armorPenetration += 3;
		}

		public override string ItemDescription() => "These tusks were probably used to penetrate the toughest of armor; could be utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases Armor Penetration by 3";

		public override string ObtainingDetails() => "You can snatch a pair of these from the Duke's corpse.";

		public override string MiscDetails() => " ";
	}
}