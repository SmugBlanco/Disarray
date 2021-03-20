using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using DustID = Disarray.ID.DustID;
using Disarray.Gardening.Core.Tiles;
using Disarray.Gardening.Core;

namespace Disarray.Gardening.Content.CaveMaize
{
	public class CaveMaizePlant : FloraBase
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			int[] CoordinateHeight = new int[3];
			for (int Indexer = 0; Indexer < CoordinateHeight.Length; Indexer++)
			{
				CoordinateHeight[Indexer] = 16;
			}
			TileObjectData.newTile.CoordinateHeights = CoordinateHeight;
			TileObjectData.newTile.AnchorValidTiles = new int[] { TileID.Dirt, TileID.Stone };
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(50, 50, 60));

			soundType = SoundID.Grass;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Vector2 position = new Vector2(i, j).ToWorldCoordinates();
			Dust.NewDust(position - new Vector2(Width / 4, Height / 4), Width / 2, Height / 2, DustID.Stone);
			return false;
		}

		public override void PlaceInWorld(int i, int j, Item item) => TileData.PlaceEntity(new Point16(i, j), "CaveMaizeEntity");
	}
}