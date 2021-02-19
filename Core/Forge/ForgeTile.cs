using Disarray.Content.Forge.Items.Jungle;
using Disarray.Content.Forge.Items.Trees;
using Disarray.Content.Forge.Tiles.Flora;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
	public class ForgeTile : GlobalTile
	{
		public override bool Drop(int i, int j, int type)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient && !WorldGen.noTileActions && !WorldGen.gen)
			{
				Tile tile = Framing.GetTileSafely(i, j);
				if (type == TileID.Trees && tile.frameX == 22)
				{
					Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<GoldenLeaf>());
				}

				if (tile.frameX % 36 == 0 && tile.frameY == 0)
				{
					if (type == TileID.LifeFruit)
					{
						Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<PrimalFruit>());
					}
				}
			}

			return base.Drop(i, j, type);
		}

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
			//Main.NewText(Framing.GetTileSafely(i, j).type + " | " + Framing.GetTileSafely(i, j).frameX + " | " + Framing.GetTileSafely(i, j).frameY);
		}

        public override void RandomUpdate(int i, int j, int type)
        {
			bool LiquidCheck(int checkRadius, int liquidType)
            {
				for (int X = i - checkRadius; X < i + checkRadius; X++)
				{
					for (int Y = j - checkRadius; Y < j + checkRadius; Y++)
					{
						if (X <= 0 || X >= Main.maxTilesX || Y <= 0 || Y >= Main.maxTilesY)
						{
							continue;
						}

						Tile tile = Framing.GetTileSafely(X, Y);
						if (tile.liquid > 0 && tile.liquidType() == liquidType)
						{
							return true;
						}
					}
				}
				return false;
			}

			if (type == TileID.JungleGrass || type == TileID.Grass)
			{
				if (Main.rand.Next(5) == 0 && LiquidCheck(10, 2))
				{
					WorldGen.PlaceObject(i, j - 1, ModContent.TileType<HoneySickle>(), true);
				}
			}
        }
    }
}