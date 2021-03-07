using Disarray.Core.Data;
using Disarray.Core.Gardening.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Disarray.Core.Gardening
{
	//might want to abuse partial classes because i like seperation
	public abstract class GardenEntity : TileData
	{
		public enum Stages
		{
			Seed,
			Young,
			Mature,
			Harvestable,
			Elder
		}

		public Stages CurrentStage
		{
			get
			{
				if (Growth < 5)
				{
					return Stages.Seed;
				}

				if (Growth < 25)
				{
					return Stages.Young;
				}

				if (Growth >= 100)
				{
					return Stages.Elder;
				}

				return Harvestable ? Stages.Harvestable : Stages.Mature;
			}
		}

		public static int BobbingTimer;

		public bool Harvestable => SetHarvestTimer >= HarvestableTime || GetGrowth >= 100;

		public virtual int HarvestableTime { get; protected set; }

		private int HarvestTimer;

		public int SetHarvestTimer { get => HarvestTimer; set => HarvestTimer = Utils.Clamp(value, 0, int.MaxValue); }

		//-------------------------------------------------------

		public virtual (int GrowthInterval, float GrowthRate) GrowthInfo { get; protected set; } = (3600, 1);

		public int GrowthTimer;

		public float GetGrowth { get => Growth; set => Growth = Utils.Clamp(value, 0, 100); }

		protected float Growth;

		//---------------------------------------------------------

		public virtual IEnumerable<PlantNeeds> Needs { get; protected set; }

		public bool UpdateAndCheckNeeds()
		{
			bool continueUpdate = true;
			foreach (PlantNeeds needs in Needs)
			{
				needs.Update();
				if (continueUpdate && !needs.FulfilledNeeds())
				{
					continueUpdate = false;
				}
			}
			return continueUpdate;
		}

		//---------------------------------------------------------

		public virtual int TileCheckDistance { get; protected set; }
		public IDictionary<int, (bool multiplication, float valueChange)> NearbyUniqueTileInfluences = new Dictionary<int, (bool multiplication, float valueChange)>();

		public sealed override void AI()
		{
			PreAI();

			if (GetGrowth >= 100 || !UpdateAndCheckNeeds())
			{
				return;
			}

			if (Growth >= 20)
			{
				SetHarvestTimer++;
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

		public virtual void OnGrowth()
		{
			UpdateFraming();
		}

		public void UpdateFraming()
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
						if (Growth < 5)
						{
							fullTile.frameX = (short)(0 + offsetX);
						}
						else if (Growth < 20)
						{
							fullTile.frameX = (short)(floraBase.Width + offsetX);
						}
						else
						{
							fullTile.frameX = (short)((Harvestable ? floraBase.Width * 3 : floraBase.Width * 2) + offsetX);
						}
					}
				}
			}
		}

		public virtual bool PreHarvest() => true;

		public virtual void OnHarvest(bool Elder) { }

		public void Harvest()
		{
			if (!Harvestable || !PreHarvest())
			{
				return;
			}

			bool Elder = CurrentStage == Stages.Elder;

			OnHarvest(Elder);
			SetHarvestTimer = 0;
			UpdateFraming();

			if (Elder)
			{
				WorldGen.KillTile(Position.X, Position.Y);
			}
		}

		public void ImpactModified(ref float input)
		{
			if (TileCheckDistance == 0 || NearbyUniqueTileInfluences.Count == 0)
			{
				return;
			}

			HashSet<int> UniqueTiles = new HashSet<int>();
			for (int X = Position.X - TileCheckDistance; X < Position.X + TileCheckDistance; X++)
			{
				for (int Y = Position.Y - TileCheckDistance; Y < Position.Y + TileCheckDistance; Y++)
				{
					UniqueTiles.Add(Framing.GetTileSafely(X, Y).type);
				}
			}

			foreach (int type in UniqueTiles)
			{
				if (NearbyUniqueTileInfluences.TryGetValue(type, out (bool multiplication, float valueChange) modifier))
				{
					if (input < 0)
					{
						modifier.valueChange = 1 / modifier.valueChange;
					}

					input = modifier.multiplication ? input * modifier.valueChange : input + modifier.valueChange;
				}
			}
		}

		public sealed override TagCompound Save()
		{
			TagCompound data = new TagCompound()
			{
				{ "GrowthTimer", GrowthTimer % GrowthInfo.GrowthInterval },
				{ "Growth", Growth },
				{ "HarvestTimer", SetHarvestTimer >= HarvestableTime ? HarvestableTime : SetHarvestTimer },
				{ "Extra", SaveExtra() },
			};

			foreach (PlantNeeds needs in Needs)
			{
				data.Add(needs.Name, needs.Save());
			}

			return data;
		}

		public virtual TagCompound SaveExtra() => null;

		public sealed override void Load(TagCompound tagCompound)
		{
			GrowthTimer = tagCompound.Get<int>("GrowthTimer");
			GetGrowth = tagCompound.Get<float>("Growth");
			SetHarvestTimer = tagCompound.Get<int>("HarvestTimer");
			LoadExtra(tagCompound.Get<TagCompound>("Extra"));

			if (Needs is null)
			{
				return;
			}

			foreach (PlantNeeds needs in Needs)
			{
				if (tagCompound.ContainsKey(needs.Name))
				{
					needs.Load(tagCompound.Get<TagCompound>(needs.Name));
				}
			}
		}

		public virtual void LoadExtra(TagCompound tagCompound) { }
	}
}