using Disarray.Almanac.Core;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Books
{
	public class DeathThesis : ModItem, IAlmanacable
	{
		public string GeneralDescription { get; set; } = "Death is inevitable.";

		public string ItemStatistics { get; set; } = string.Empty;

		public string ObtainingGuide { get; set; } = string.Empty;

		public string Miscellaneous { get; set; } = "There seems to be a review on the back from a children's foundation, it says 'A great read'.";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("A Thesis on the Inevitability of Death");
			Tooltip.SetDefault("-A.D. Olph");
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