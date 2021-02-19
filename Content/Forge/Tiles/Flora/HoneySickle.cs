using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Disarray.Content.Forge.Items.Flora;

namespace Disarray.Content.Forge.Tiles.Flora
{
	public class HoneySickle : FloraBase
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.AnchorValidTiles = new int[] { TileID.Grass, TileID.JungleGrass };
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(200, 175, 60));

			soundType = SoundID.Grass;
		}

        public override bool CreateDust(int i, int j, ref int type)
        {
			Vector2 position = new Vector2(i, j).ToWorldCoordinates();
			Dust.NewDust(position - new Vector2(Width / 4, Height / 4), Width / 2, Height / 2, 153);
			return false;
        }

        public override short Height => 36;

        public override short Width => 36;

		public override float GrowthRate => 0.75f;

        public override float Sturdiness => 0.25f;

        public override int SeedItem => ModContent.ItemType<HoneySickleSeed>();

		public override int HarvestItem => ModContent.ItemType<HoneySickleFruit>();

        public override bool BasicNecessities(int i, int j)
        {
			return LiquidCheck(i, j, 6, 2);
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY += 2;
		}
	}
}