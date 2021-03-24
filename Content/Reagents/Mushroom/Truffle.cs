using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Mushroom
{
	public class Truffle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Truffle");
			Tooltip.SetDefault("'A rare delicacy'");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = 10000;
		}
	}
}