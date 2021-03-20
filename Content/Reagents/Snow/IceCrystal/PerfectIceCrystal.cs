using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Snow.IceCrystal
{
	public class PerfectIceCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Perfect Ice Crystal");
			Tooltip.SetDefault("'A rare product of the frigid cold, it owes it's allure to nature's desire for excellence'");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = 500;
		}
	}
}