using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Content.Forge.Items
{
	public class TileTestItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tile Test Item");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.consumable = false;
			item.createTile = mod.TileType("HoneySickle");
		}
	}
}