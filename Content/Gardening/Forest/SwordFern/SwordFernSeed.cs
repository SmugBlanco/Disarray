using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Core.Gardening.Items;

namespace Disarray.Content.Gardening.Forest.SwordFern
{
	public class SwordFernSeed : SeedItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword Fern Seed");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.createTile = ModContent.TileType<SwordFernPlant>();

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.useTime = 15;
			item.useAnimation = 15;

			item.autoReuse = true;
			item.consumable = true;
		}
	}
}