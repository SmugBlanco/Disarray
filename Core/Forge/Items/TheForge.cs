using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Forge.Items
{
	public class TheForge : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Forge");
			Tooltip.SetDefault("Allows one to customize and create their own unique weapon");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 14;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Blue;
			item.consumable = true;
			item.createTile = mod.TileType("TheForge");
		}
	}
}