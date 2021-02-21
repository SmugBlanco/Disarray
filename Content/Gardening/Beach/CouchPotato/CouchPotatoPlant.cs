using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Disarray.Core.Data;

namespace Disarray.Content.Gardening.Beach.CouchPotato
{
	public class CouchPotatoPlant : FloraBase
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(90, 100, 150));

			soundType = SoundID.Grass;
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Vector2 position = new Vector2(i, j).ToWorldCoordinates();
			Dust.NewDust(position - new Vector2(Width / 4, Height / 4), Width / 2, Height / 2, 153);
			return false;
		}

        public override short Height => 36;

		public override short Width => 54;

		public override float GrowthRate => 0.1f;

		public override float Sturdiness => 0f;

		public override int SeedItem => ModContent.ItemType<CouchPotatoSeed>();

		public override int HarvestItem => ModContent.ItemType<CouchPotatoSpud>();

		public override bool Perennial => false;

		public override bool BasicNecessities(int i, int j)
		{
			return LiquidCheck(i, j, 10, 1);
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY += 2;
		}
	}
}