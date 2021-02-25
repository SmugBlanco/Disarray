using Disarray.Content.Forge.Items.Jungle;
using Disarray.Content.Forge.Items.Trees;
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
    }
}