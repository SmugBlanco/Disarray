using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Disarray.Core.Gardening.Tiles;
using Terraria.DataStructures;
using Disarray.Core.Data;
using DustID = Disarray.Core.Data.DustID;

namespace Disarray.Content.Gardening.PricklyPearBear
{
	public class PricklyPearBearPlant : FloraBase
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.AnchorValidTiles = new int[] { TileID.Sand };
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(75, 120, 20));

			soundType = SoundID.Grass;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Vector2 position = new Vector2(i, j).ToWorldCoordinates();
			Dust.NewDust(position - new Vector2(Width / 4, Height / 4), Width / 2, Height / 2, DustID.JungleGrass);
			return false;
		}

		public override void PlaceInWorld(int i, int j, Item item) => TileData.PlaceEntity(new Point16(i, j), "PricklyPearBearEntity");
	}
}