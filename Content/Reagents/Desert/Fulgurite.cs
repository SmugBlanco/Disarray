using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Desert
{
	public class Fulgurite : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite");
			Tooltip.SetDefault("'Lightning fried sand'");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 32;
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
			item.value = 500;
		}
	}
}