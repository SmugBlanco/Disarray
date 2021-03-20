using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Desert
{
	public class Artifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Artifact");
			Tooltip.SetDefault("'Created near the dawn of civilization'");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 34;
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
			item.value = 10000;
		}
	}
}