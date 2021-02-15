using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Birds
{
	public class BirdsEye : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bird's Eye");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.magicCrit += 2;
			player.meleeCrit += 2;
			player.rangedCrit += 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 2;
			player.meleeCrit += 2;
			player.rangedCrit += 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicCrit += 2;
			player.meleeCrit += 2;
			player.rangedCrit += 2;
		}

		public override string ItemDescription() => "Able to spot and calculate an almost perfect trajectory to cleanly kill their prey, the eyes of an avian could be probably be utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases most weapon type's Critical Strike Chance by 2";

		public override string ObtainingDetails() => "A rare chance to dropped intact from most avians.";

		public override string MiscDetails() => " ";
	}
}