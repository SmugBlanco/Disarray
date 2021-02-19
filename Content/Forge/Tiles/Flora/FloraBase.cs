using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Disarray.Content.Forge.Tiles.Flora
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

		public Growth GetCurrentGrowth(int i, int j) => (Growth)(Framing.GetTileSafely(i, j).frameX / Width);

		public bool IsDead(int i, int j) => Framing.GetTileSafely(i, j).frameY / Height == 1;

		public virtual short Width => 0;

		public virtual short Height => 0;

		public virtual float Difficulty { get; set; } = 1;

		public virtual float GrowthRate { get; set; } = 1;

		public virtual float Sturdiness { get; set; } = 0;

		public virtual int SeedItem { get; set; } = 0;

		public virtual int HarvestItem { get; set; } = 0;

		public virtual bool BasicNecessities(int i, int j) => false;

		/// <summary>
		/// Return false to prevent default harvest logic
		/// </summary>
		public virtual bool Harvest(int i, int j) => true;

		/// <summary>
		/// Return false to prevent default destoryed logic
		/// </summary>
		public virtual bool Destroyed(int i, int j) => true;

		/// <summary>
		/// Useful for changing growth and sturdiness right before an update
		/// </summary>
		public virtual void PreGrowthUpdate(ref float GrowthRate, ref float Sturdiness) { }

		public override void RandomUpdate(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			Growth currentGrowth = GetCurrentGrowth(i, j);

			if (IsDead(i, j))
            {
				return;
            }

			if (tile.frameX % Width == 0 && tile.frameY % Height == 0)
			{
				float plantGrowth = GrowthRate;
                float plantSturdiness = Sturdiness;

				PreGrowthUpdate(ref plantGrowth, ref plantSturdiness);

				if (!(Main.rand.NextFloat(1) < plantSturdiness) && !BasicNecessities(i, j))
                {
					for (int X = i; X < i + Width / 18; X++)
					{
						for (int Y = j; Y < j + Height / 18; Y++)
						{
							Tile fullTile = Framing.GetTileSafely(X, Y);

							if (fullTile.type != Type)
                            {
								continue;
							}

							fullTile.frameY += Height;
						}
					}

					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendTileSquare(-1, i, j, Width >= Height ? Width / 18 : Height / 18);
					}
					return;
				}

				if (Main.rand.NextFloat(1) < plantGrowth && currentGrowth != Growth.Harvestable)
				{
					for (int X = i; X < i + Width / 18; X++)
					{
						for (int Y = j; Y < j + Height / 18; Y++)
						{
							Tile fullTile = Framing.GetTileSafely(X, Y);

							if (fullTile.type != Type)
							{
								continue;
							}

							fullTile.frameX += Width;
						}
					}

					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendTileSquare(-1, i, j, Width >= Height ? Width / 18 : Height / 18);
					}
					return;
				}
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

				if (currentGrowth == Growth.Harvestable)
				{
					Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), HarvestItem);
				}
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			Growth currentGrowth = GetCurrentGrowth(i, j);

			if (IsDead(i, j) || currentGrowth != Growth.Harvestable)
			{
				return false;
			}

			if (Harvest(i, j))
			{
				Vector2 OriginTile = new Vector2(i - (tile.frameX % Width / 18), j - (tile.frameY % Height / 18));

				Item.NewItem(OriginTile.ToWorldCoordinates(), HarvestItem);

				for (int X = (int)OriginTile.X; X < OriginTile.X + Width / 18; X++)
				{
					for (int Y = (int)OriginTile.Y; Y < OriginTile.Y + Height / 18; Y++)
					{
						Tile fullTile = Framing.GetTileSafely(X, Y);
						fullTile.frameX -= Width;
					}
				}

				if (Main.netMode != NetmodeID.SinglePlayer)
				{
					NetMessage.SendTileSquare(-1, i, j, Width / 18);
				}
			}
			return true;
		}

		//Probably will be moving this to a better place
		public static bool LiquidCheck(int i, int j, int checkRadius, int liquidType)
		{
			for (int X = i - checkRadius; X < i + checkRadius; X++)
			{
				for (int Y = j - checkRadius; Y < j + checkRadius; Y++)
				{
					if (X <= 0 || X >= Main.maxTilesX || Y <= 0 || Y >= Main.maxTilesY)
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
	}
}