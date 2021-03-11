using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Disarray.Core.Gardening.Tiles;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Disarray.Core.Data;
using Disarray.Core.Globals;
using Disarray.Core.Gardening;

namespace Disarray.Content.Gardening.Items
{
	public class GardeningHelper : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gardening Helper");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item1;
			item.useTime = 60;
			item.useAnimation = 60;
		}

		public override bool UseItem(Player player)
		{
			if (player.itemAnimation == player.itemAnimationMax - 1)
			{
				Point16 point = new Point16((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
				Tile tile = Framing.GetTileSafely(point);
				if (ModContent.GetModTile(tile.type) is FloraBase flora)
				{
					Point16 OriginTile = new Point16(point.X, point.Y) - new Point16(tile.frameX % flora.Width / 18, tile.frameY % flora.Height / 18);
					OriginTile += TileObjectData.GetTileData(tile).Origin;
					if (DisarrayWorld.GardenEntitiesByPosition.TryGetValue(OriginTile, out TileData tileData))
					{
						GardenEntity gardenEntity = tileData as GardenEntity;
						Main.NewText("Growth: " + gardenEntity.GetGrowth + " | " + (gardenEntity.GrowthTimer % gardenEntity.GrowthInfo.GrowthInterval) + "/" + gardenEntity.GrowthInfo.GrowthInterval + " @ " + gardenEntity.GrowthInfo.GrowthRate);
						foreach (PlantNeeds needs in gardenEntity.Needs)
						{
							needs.DisplayInformation();
						}
					}
				}
			}
			return base.UseItem(player);
		}

	}
}