using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace Disarray.Content.Paintings
{
	public abstract class PaintingTile : ModTile
	{
		public abstract (int Width, int Height) Dimensions { get; internal set; }

		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Width = Dimensions.Width;
			TileObjectData.newTile.Height = Dimensions.Height;
			int[] CoordinateHeight = new int[Dimensions.Height];
			for (int Indexer = 0; Indexer < CoordinateHeight.Length; Indexer++)
			{
				CoordinateHeight[Indexer] = 16;
			}
			TileObjectData.newTile.CoordinateHeights = CoordinateHeight;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);

			disableSmartCursor = true;
			ModTranslation modTranslation = CreateMapEntryName();
			modTranslation.SetDefault("Painting");
			AddMapEntry(new Color(100, 125, 150), modTranslation);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(i * 16, j * 16, Dimensions.Width * 16, Dimensions.Height * 16, mod.ItemType(GetType().Name + "Item"));

		public override void NumDust(int i, int j, bool fail, ref int num) => num = 0;
	}
}