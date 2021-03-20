using Terraria.ModLoader;

namespace Disarray.Content.Reagents
{
	public class MartianBlood : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Martian Blood");
			Tooltip.SetDefault("'Someone lays dead for this to be in your hands'");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 999;
			item.value = 250;
		}
	}
}