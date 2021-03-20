using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Snow.IceCrystal
{
	public class FlawlessIceCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flawless Ice Crystal");
			Tooltip.SetDefault("'A rare product of the frigid cold, it owes it's allure to nature's desire for excellence'");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = 50;
		}
	}
}