using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.CaveMaize.Items.Accessories
{
	public class CoalRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coal Ring");
			Tooltip.SetDefault("Increases mining speed by 3%" + "\n'Beautiful, clean coal...'");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.value = 2500;
			item.rare = ItemRarityID.Blue;
			item.accessory = true;
		}

		public override void UpdateEquip(Player player) => player.pickSpeed -= 0.03f;
	}
}