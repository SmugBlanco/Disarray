using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.DataStructures;

namespace Disarray.Core.Gardening.Tiles
{
	public abstract class FloraBase : ModTile
	{
		public enum Growth : byte
		{
			Seed,
			Young,
			Mature,
			Harvestable
		}

		public static ICollection<FloraBase> LoadedBases = new Collection<FloraBase>();

        public sealed override bool Autoload(ref string name, ref string texture)
        {
			LoadedBases.Add(this);
			return true;
        }

		public static void Unload()
        {
			LoadedBases.Clear();
        }

		public Growth GetCurrentGrowth(int i, int j) => (Growth)(Framing.GetTileSafely(i, j).frameX / Width);

		public bool IsDead(int i, int j) => Framing.GetTileSafely(i, j).frameY / Height == 1;

		public virtual short Width => 0;

		public virtual short Height => 0;

		public virtual int MinimumLiquidRadius => 0;

		public virtual int RequiredLiquidType => 0;

		public virtual float MinimumLightLevel => 0f;

		public virtual float GrowthRate { get; set; } = 1;

		public virtual float Sturdiness { get; set; } = 0; //Influences the chance this plant has of dying on random update

		public virtual int DetectionRadius { get; set; } = 0; // Used for getting unqiue blocks on Random Update, defaults to 0 if you dont want to include this

		public virtual int SeedItem { get; set; } = 0;

		public virtual int HarvestItem { get; set; } = 0;

		public virtual bool BasicNecessities(int i, int j) => true;

		public bool HasMetBasicNecessities(int i, int j) => LightCheck(i, j) && LiquidCheck(new Rectangle(i, j, Width / 18, Height / 18), MinimumLiquidRadius, RequiredLiquidType) && BasicNecessities(i, j); // LightCheck could be inaccurate as its currently checking the almost center

		public virtual bool Harvest(int i, int j) => true;

		public virtual bool Destroyed(int i, int j) => true;

		public virtual void PreGrowthUpdate(int i, int j, ref float GrowthRate, ref float Sturdiness, IEnumerable<int> TileTypes) { }

		public virtual void NaturalSpawning(int i, int j, int type) { }

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height) => offsetY += 2;

		public override void RandomUpdate(int i, int j)
		{
			if (IsDead(i, j))
			{
				return;
			}

			Tile tile = Framing.GetTileSafely(i, j);
			Growth currentGrowth = GetCurrentGrowth(i, j);

			Point16 tileOrigin = new Point16(i - (tile.frameX % Width / 18), j - (tile.frameY % Height / 18));

			float plantGrowth = GrowthRate;
			float plantSturdiness = Sturdiness;

			if (DetectionRadius > 0)
			{
				ICollection<int> uniqueTiles = new HashSet<int>();
				for (int X = i - DetectionRadius; X < i + DetectionRadius + Width; X++)
				{
					for (int Y = j - DetectionRadius; Y < j + DetectionRadius + Height; Y++)
					{
						uniqueTiles.Add(Framing.GetTileSafely(X, Y).type);
					}
				}

				PreGrowthUpdate(i, j, ref plantGrowth, ref plantSturdiness, uniqueTiles);
			}
			else
			{
				PreGrowthUpdate(i, j, ref plantGrowth, ref plantSturdiness, null);
			}

			if (Main.rand.NextFloat(1) > plantSturdiness && !HasMetBasicNecessities(i, j))
			{
				for (int X = tileOrigin.X; X < tileOrigin.X + Width / 18; X++)
				{
					for (int Y = tileOrigin.Y; Y < tileOrigin.Y + Height / 18; Y++)
					{
						Tile fullTile = Framing.GetTileSafely(X, Y);

						if (fullTile.type != Type)
						{
							continue;
						}

						fullTile.frameY += Height;

						SyncTile(i, j);
					}
				}

				return;
			}

			if (currentGrowth != Growth.Harvestable && Main.rand.NextFloat(1) < plantGrowth)
			{
				for (int X = tileOrigin.X; X < tileOrigin.X + Width / 18; X++)
				{
					for (int Y = tileOrigin.Y; Y < tileOrigin.Y + Height / 18; Y++)
					{
						Tile fullTile = Framing.GetTileSafely(X, Y);

						if (fullTile.type != Type)
						{
							continue;
						}

						fullTile.frameX += Width;
					}
				}

				SyncTile(i, j);

				return;
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			if (frameY / Height == 1)
			{
				return;
			}

			Growth currentGrowth = (Growth)(frameX / Width);

			if (Destroyed(i, j))
			{
				Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), SeedItem);
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			if (IsDead(i, j) || GetCurrentGrowth(i, j) != Growth.Harvestable)
			{
				return false;
			}

			Tile tile = Framing.GetTileSafely(i, j);

			if (Harvest(i, j))
			{
				Point16 OriginTile = new Point16(i - (tile.frameX % Width / 18), j - (tile.frameY % Height / 18));

				Item.NewItem(OriginTile.ToWorldCoordinates(), HarvestItem);

				for (int X = OriginTile.X; X < OriginTile.X + Width / 18; X++)
				{
					for (int Y = OriginTile.Y; Y < OriginTile.Y + Height / 18; Y++)
					{
						Tile fullTile = Framing.GetTileSafely(X, Y);
						fullTile.frameX -= Width;
					}
				}

				SyncTile(i, j);
			}
			return true;
		}

		public bool OutOfWorldBoundsCheck(int i, int j) => i <= 0 || i >= Main.maxTilesX || j <= 0 || j >= Main.maxTilesY;

		//Probably will be moving this to a better place
		public bool LiquidCheck(Rectangle checkSource, int checkRadius, int liquidType)
		{
			for (int X = checkSource.X - checkRadius; X < checkSource.Right + checkRadius; X++)
			{
				for (int Y = checkSource.Y - checkRadius; Y < checkSource.Bottom + checkRadius; Y++)
				{
					if (OutOfWorldBoundsCheck(X, Y))
					{
						continue;
					}

					Tile tile = Framing.GetTileSafely(X, Y);
					if (tile.liquid > 0 && tile.liquidType() == liquidType)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool LightCheck(int i, int j)
        {
			i *= 16;
			j *= 16;
			float averageLightLevel = GetLightLevel(i + Width / 2, j + Height / 2) * 1.2f;
			float lightLevelNeeded = Main.dayTime ? MinimumLightLevel : MinimumLightLevel / 3;
			return averageLightLevel >= lightLevelNeeded;
		}

		public bool LightCheckWText(int i, int j)
		{
			i *= 16;
			j *= 16;
			float averageLightLevel = GetLightLevel(i + Width / 2, j + Height / 2) * 1.2f;
			Main.NewText(nameof(averageLightLevel) + ": " + averageLightLevel);
			float lightLevelNeeded = Main.dayTime ? MinimumLightLevel : MinimumLightLevel / 3;
			Main.NewText(nameof(lightLevelNeeded) + ": " + lightLevelNeeded);
			return averageLightLevel >= lightLevelNeeded;
		}

		public static float GetLightLevel(int i, int j)
        {
			Vector3 lighting = Lighting.GetSubLight(new Vector2(i, j).ToWorldCoordinates());
			return (lighting.X + lighting.Y + lighting.Z) / 3;
        }

		public void SyncTile(int i, int j)
		{
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendTileSquare(-1, i, j, Width > Height ? Width / 18 : Height / 18);
			}
		}
	}
}