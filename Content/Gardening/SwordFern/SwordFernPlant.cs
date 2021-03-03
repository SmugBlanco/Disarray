using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Disarray.Core.Gardening.Tiles;
using Disarray.Content.Gardening.SwordFern.Items;

namespace Disarray.Content.Gardening.SwordFern
{
	public class SwordFernPlant : FloraBase
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.AnchorValidTiles = new int[] { TileID.Grass };
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(40, 175, 90));

			soundType = SoundID.Grass;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Vector2 position = new Vector2(i, j).ToWorldCoordinates();
			Dust.NewDust(position - new Vector2(Width / 4, Height / 4), Width / 2, Height / 2, 0);
			return false;
		}

		public override short Height => 36;

		public override short Width => 36;
	}
}