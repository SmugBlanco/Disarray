using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Disarray.Core.Gardening.Tiles;
using System;
using Disarray.Core.Gardening;
using Terraria.DataStructures;

namespace Disarray.Content.Gardening.CouchPotato
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
			Dust.NewDust(position - new Vector2(Width / 4, Height / 4), Width / 2, Height / 2, 0);
			return false;
		}

        public override short Height => 36;

		public override short Width => 54;

        public override int MinimumLiquidRadius => 8;

		public override int RequiredLiquidType => 0;

        public override float MinimumLightLevel => 0.75f;

        public override float GrowthRate => 0.1f;

		public override float Sturdiness => 0f;

		public override int SeedItem => ModContent.ItemType<CouchPotatoSeed>();

		public override void PlaceInWorld(int i, int j, Item item)
		{
			Main.NewText("attempting place");
			GardenEntity.PlaceEntity(new Point16(i, j), "CouchPotatoEntity");
		}

		public override void NaturalSpawning(int i, int j, int type)
        {
			if (type == TileID.Sand && (i < Main.maxTilesX * 0.1f || i > Main.maxTilesX * 0.9f) && Main.rand.Next(8) == 0)
			{
				Tile tile = Framing.GetTileSafely(i, j - 1);
				if (tile.liquid == 0 && HasMetBasicNecessities(i, j))
				{
					WorldGen.PlaceObject(i, j - 2, Type, true);
					SyncTile(i, j);
				}
			}
		}

        public override bool Harvest(int i, int j)
        {
			Vector2 spawnPosition = new Vector2(i, j).ToWorldCoordinates();

			if (!Main.hardMode)
            {
				Item.NewItem(spawnPosition, ItemID.SilverCoin, Main.rand.Next(5, 20));
				Item.NewItem(spawnPosition, ItemID.CopperCoin, Main.rand.Next(0, 100));

				Item.NewItem(spawnPosition, ItemID.HealingPotion, Main.rand.Next(1, 3));
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem(spawnPosition, ItemID.GreaterHealingPotion);
				}
			}
			else
            {
				Item.NewItem(spawnPosition, ItemID.GoldCoin, Main.rand.Next(1, 5));
				Item.NewItem(spawnPosition, ItemID.SilverCoin, Main.rand.Next(0, 100));
				Item.NewItem(spawnPosition, ItemID.CopperCoin, Main.rand.Next(0, 100));

				Item.NewItem(spawnPosition, ItemID.GreaterHealingPotion, Main.rand.Next(1, 3));
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem(spawnPosition, ItemID.SuperHealingPotion);
				}
			}

			Item.NewItem(spawnPosition, ModContent.ItemType<CouchPotatoSpud>(), Main.rand.Next(1, 4));
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem(spawnPosition, Utils.SelectRandom(Main.rand, ItemID.BowlofSoup, ItemID.Pho, ItemID.PadThai, ItemID.GrubSoup));
			}

			WorldGen.KillTile(i, j);
			SyncTile(i, j);

			return false;
        }
    }
}