using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Demons
{
	public class DemonsEye : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demon's Eye");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 22;
			item.rare = ItemRarityID.Orange;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.magicCrit += 3;
			player.meleeCrit += 3;
			player.rangedCrit += 3;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 3;
			player.meleeCrit += 3;
			player.rangedCrit += 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicCrit += 3;
			player.meleeCrit += 3;
			player.rangedCrit += 3;
		}

		public override string ItemDescription() => "Utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases most weapon type's Critical Strike Chance by 3";

		public override string ObtainingDetails() => "A rare chance to dropped intact from most demons.";

		public override string MiscDetails() => "Bears a striking resemblence to those eyes who fly in the night sky.";
	}
}