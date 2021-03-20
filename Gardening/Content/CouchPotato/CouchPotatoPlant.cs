using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Disarray.Gardening.Core.Tiles;
using Disarray.Gardening.Core;

namespace Disarray.Gardening.Content.CouchPotato
{
	public class CouchPotatoPlant : FloraBase
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(55, 100, 130));

			soundType = SoundID.Grass;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Vector2 position = new Vector2(i, j).ToWorldCoordinates();
			Dust.NewDust(position - new Vector2(Width / 4, Height / 4), Width / 2, Height / 2, DustID.Dirt);
			return false;
		}

		public override void PlaceInWorld(int i, int j, Item item) => TileData.PlaceEntity(new Point16(i, j), "CouchPotatoEntity");
	}
}