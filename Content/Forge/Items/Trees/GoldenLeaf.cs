using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Trees
{
	public class GoldenLeaf : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Golden Leaf");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 46;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.lifeRegen += 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.lifeRegen += 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.lifeRegen += 1;
		}

		public override string ItemDescription() => "These leaves seems to have some healing properties, perhaps that can be utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases Life Regeneration by 1";

		public override string ObtainingDetails() => "A rare mutation of a coloration gene that seems to occur on trees of all variety.";

		public override string MiscDetails() => " ";
	}
}