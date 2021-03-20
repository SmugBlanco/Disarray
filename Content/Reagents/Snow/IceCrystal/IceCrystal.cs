using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Snow.IceCrystal
{
	public class IceCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Crystal");
			Tooltip.SetDefault("'A product of the frigid cold, it owes it's allure to nature's desire for excellence'");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 14;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = 5;
		}
	}
}