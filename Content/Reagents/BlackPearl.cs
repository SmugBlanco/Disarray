using Terraria.ModLoader;

namespace Disarray.Content.Reagents
{
	public class BlackPearl : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Pearl");
			Tooltip.SetDefault("'A fragment of the ocean's beauty'");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 14;
			item.maxStack = 999;
			item.value = 250;
		}
	}
}