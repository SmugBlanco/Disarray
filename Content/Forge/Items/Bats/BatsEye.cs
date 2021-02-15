using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Bats
{
	public class BatsEye : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bat's Eye");
		}

		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 8;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.magicCrit += 1;
			player.meleeCrit += 1;
			player.rangedCrit += 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 1;
			player.meleeCrit += 1;
			player.rangedCrit += 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicCrit += 1;
			player.meleeCrit += 1;
			player.rangedCrit += 1;
		}

		public override string ItemDescription() => "Considering an old adage is 'As blind as a bat', these probably won't have many uses. Maybe they'll have some purpose in 'The Forge'";

		public override string ItemStatistics() => "Increases most weapon type's Critical Strike Chance by 1";

		public override string ObtainingDetails() => "A rare chance to dropped intact from most bats.";

		public override string MiscDetails() => " ";
	}
}