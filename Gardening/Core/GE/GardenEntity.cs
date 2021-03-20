using Disarray.Gardening.Core.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Disarray.Gardening.Core.GE
{
	public abstract partial class GardenEntity : TileData // I split this into 5 files, sometimes... my genius... it's frightening
	{
		public GrowthStages GetCurrentStage
		{
			get
			{
				if (Growth < 5)
				{
					return GrowthStages.Seed;
				}

				if (Growth < 25)
				{
					return GrowthStages.Young;
				}

				if (Growth < 50)
				{
					return GrowthStages.Adolescent;
				}

				if (Growth < 100)
				{
					return GrowthStages.Matured;
				}

				return GrowthStages.Elder;
			}
		}

		public virtual int TileCheckDistance { get; protected set; }

		public IDictionary<int, (float additive, float multiplicative)> NearbyUniqueTileInfluences = new Dictionary<int, (float additive, float multiplicative)>();

		public virtual (int GrowthInterval, float GrowthRate) GrowthInfo { get; protected set; } = (3600, 1);

		public int GrowthTimer;

		public float GetGrowth { get => Growth; set => Growth = Terraria.Utils.Clamp(value, 0, 100); }

		protected float Growth;

		public GardenEntity()
		{
			if (Disarray.Loading)
			{
				return;
			}

			SetUpNeeds();
		}

		public sealed override void AI()
		{
			PreAI();

			if (GetGrowth >= 100 || !UpdateAndCheckNeeds())
			{
				return;
			}

			GrowthTimer++;
			if (GrowthTimer % GrowthInfo.GrowthInterval == 0)
			{
				float growthRate = GrowthInfo.GrowthRate;
				ImpactModified(ref growthRate);
				PreUpdateGrowth(ref growthRate);
				Growth += growthRate;
				OnGrowth();
			}

			Update();
		}

		public virtual void PreUpdateGrowth(ref float GrowthRate) { }

		public virtual void PreAI() { }

		public virtual void Update() { }

		public virtual void OnGrowth() => UpdateFraming();

		public virtual void UpdateFraming()
		{
			Tile tile = Framing.GetTileSafely(Position);
			if (ModContent.GetModTile(tile.type) is FloraBase floraBase)
			{
				Point16 OriginTile = new Point16(Position.X - (tile.frameX % floraBase.Width / 18), Position.Y - (tile.frameY % floraBase.Height / 18));

				for (int X = OriginTile.X; X < OriginTile.X + floraBase.Width / 18; X++)
				{
					for (int Y = OriginTile.Y; Y < OriginTile.Y + floraBase.Height / 18; Y++)
					{
						int offsetX = (X - OriginTile.X) * 18;
						Tile fullTile = Framing.GetTileSafely(X, Y);
						int currentStageValue = (int)GetCurrentStage > 3 ? 3 : (int)GetCurrentStage;
						fullTile.frameX = (short)(currentStageValue * floraBase.Width + offsetX);
					}
				}
			}
		}

		public virtual void ImpactModified(ref float input)
		{
			if (TileCheckDistance == 0 || NearbyUniqueTileInfluences.Count == 0)
			{
				return;
			}

			HashSet<int> uniqueTiles = new HashSet<int>();
			for (int X = Position.X - TileCheckDistance; X < Position.X + TileCheckDistance; X++)
			{
				for (int Y = Position.Y - TileCheckDistance; Y < Position.Y + TileCheckDistance; Y++)
				{
					Tile tile = Framing.GetTileSafely(X, Y);
					if (!uniqueTiles.Contains(tile.type))
					{
						if (NearbyUniqueTileInfluences.TryGetValue(tile.type, out (float additive, float multiplicative) modifier))
						{
							input = input * modifier.multiplicative + modifier.additive;
						}

						uniqueTiles.Add(tile.type);
					}
				}
			}
		}
	}
}