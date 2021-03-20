using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Content.Paintings
{
	public abstract class PaintingItem : ModItem
	{
		public abstract (int Width, int Height) Dimensions { get; internal set; }

		public override void SetDefaults()
		{
			item.width = Dimensions.Width;
			item.height = Dimensions.Height;
			item.maxStack = 999;
			
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.autoReuse = true;

			item.consumable = true;
			item.createTile = mod.TileType(GetType().Name.Remove(GetType().Name.Length - 4));
		}
	}
}