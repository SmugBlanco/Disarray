using Terraria.ModLoader;

namespace Disarray.Gardening.Content.PricklyPearBear
{
	public class PricklyPearBearFruit : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prickly Pear Bear Fruit");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
		}
	}
}