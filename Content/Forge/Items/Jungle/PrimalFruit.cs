using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Jungle
{
	public class PrimalFruit : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primal Fruit");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Lime;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.lifeRegen += 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.lifeRegen += 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.lifeRegen += 2;
		}

		public override string ItemDescription() => "This fruit seems to have some healing properties, perhaps that can be utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases Life Regeneration by 2";

		public override string ObtainingDetails() => "Seems to be a byproduct of Life Fruits.";

		public override string MiscDetails() => " ";
	}
}