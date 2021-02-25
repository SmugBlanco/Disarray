using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Disarray.Core.Gardening.Tiles;

namespace Disarray.Content.Gardening.Jungle.HoneySickle
{
	public class HoneySicklePlant : FloraBase
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

		public override int MinimumLiquidRadius => 5;

		public override int RequiredLiquidType => 2;

		public override float MinimumLightLevel => 0;

		public override float GrowthRate => 0.5f;

        public override float Sturdiness => 0.25f;

        public override int SeedItem => ModContent.ItemType<HoneySickleSeed>();

		public override int HarvestItem => ModContent.ItemType<HoneySickleFruit>();

		public override void NaturalSpawning(int i, int j, int type)
		{
			if ((type == TileID.JungleGrass || type == TileID.Grass) && Main.rand.Next(10) == 0)
			{
				if (HasMetBasicNecessities(i, j))
				{
					WorldGen.PlaceObject(i, j - 1, Type, true);
					SyncTile(i, j);
				}
			}
		}
	}
}