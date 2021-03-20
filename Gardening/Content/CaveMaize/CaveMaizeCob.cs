using Terraria.ModLoader;

namespace Disarray.Gardening.Content.CaveMaize
{
	public class CaveMaizeCob : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cave Maize");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
		}
	}
}