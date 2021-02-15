using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Snakes
{
	public class SnakesEye : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snake's Eye");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 16;
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.magicCrit += 4;
			player.meleeCrit += 4;
			player.rangedCrit += 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 4;
			player.meleeCrit += 4;
			player.rangedCrit += 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicCrit += 4;
			player.meleeCrit += 4;
			player.rangedCrit += 4;
		}

		public override string ItemDescription() => "Some say a creature's eyes, through evolution, would go blind after generations in a dark environment; regardless of that, this may have some uses in 'The Forge'";

		public override string ItemStatistics() => "Increases most weapon type's Critical Strike Chance by 4";

		public override string ObtainingDetails() => "A rare chance to dropped intact from most serpentines.";

		public override string MiscDetails() => " ";
	}
}