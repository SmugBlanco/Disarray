using Disarray.Core.Data;
using Disarray.Core.Gardening.Tiles;
using Microsoft.Xna.Framework;
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

		public bool Harvestable => HarvestTimer >= HarvestableTime || GetGrowth >= 100;

		public virtual int HarvestableTime { get; protected set; }

		public int HarvestTimer;

		//-------------------------------------------------------

		public virtual (int GrowthInterval, float GrowthRate, float RequiredMinimumHealth) GrowthInfo { get; protected set; } = (600, 1, 0);

		public int GrowthTimer;

		public float GetGrowth { get => Growth; set => Growth = Utils.Clamp(value, 0, 100); }

		protected float Growth;

		//---------------------------------------------------------

		public float GetHealth { get => Health; set => Health = Utils.Clamp(value, 0, 100); }

		protected float Health = 100;

		//---------------------------------------------------------

		public virtual (int Sturdiness, int CheckInterval) WateringTimerInfo { get; protected set; }

		public int TimeSinceLastWatering;

		public virtual (float NegativeImpact, float PositiveImpact) WaterImpacts { get; protected set; } = (-1, 1);

		//---------------------------------------------------------

		public virtual float MinimumLight { get; protected set;  } = 0.5f;

		//---------------------------------------------------------

		public virtual (int Sturdiness, int CheckInterval) LightingTimerInfo { get; protected set; }

		public int TimeSinceLightNeedsMet;

		public virtual (float NegativeImpact, float PositiveImpact) LightImpacts { get; protected set; } = (-1, 1);

		//---------------------------------------------------------

		public virtual int TileCheckDistance { get; protected set; }
		public IDictionary<int, (bool multiplication, float valueChange)> NearbyUniqueTileInfluences = new Dictionary<int, (bool multiplication, float valueChange)>();

		public static float Average(params float[] input) //lmk if there is better alternative
		{
			float total = 0;
			for (int indexer = 0; indexer < input.Length; indexer++)
			{
				total += input[indexer];
			}
			return total / input.Length;
		}

		public void Water()
		{
			TimeSinceLastWatering = 0;
		}

		public bool LightCheck
		{
			get
			{
				Vector2 worldPosition = Position.ToWorldCoordinates();
				Vector3 light = Lighting.GetSubLight(worldPosition);
				return Average(light.X, light.Y, light.Z) * 1.2f >= MinimumLight;
			}
		}

		public sealed override void AI()
		{
			if (GetHealth <= 0 || GetGrowth >= 100)
			{
				return;
			}

			if (Growth >= 20)
			{
				HarvestTimer++;
			}

			GrowthTimer++;
			if (GrowthTimer % GrowthInfo.GrowthInterval == 0)
			{
				if (GetHealth >= GrowthInfo.RequiredMinimumHealth)
				{
					float growthRate = GrowthInfo.GrowthRate;
					PreUpdateGrowth(ref growthRate);
					Growth += growthRate;
					OnGrowth();
				}
			}

			TimeSinceLastWatering++;
			if (TimeSinceLastWatering % WateringTimerInfo.CheckInterval == 0)
			{
				if (TimeSinceLastWatering >= WateringTimerInfo.Sturdiness)
				{
					float currentHealthImpact = HealthImpactModified(WaterImpacts.NegativeImpact);
					PreUpdateHealth(ref currentHealthImpact);
					float oldHealth = GetHealth;
					GetHealth += HealthImpactModified(currentHealthImpact);
					OnHealthChange(oldHealth);
				}
				else
				{
					float currentHealthImpact = HealthImpactModified(WaterImpacts.PositiveImpact);
					PreUpdateHealth(ref currentHealthImpact);
					float oldHealth = GetHealth;
					GetHealth += HealthImpactModified(currentHealthImpact);
					OnHealthChange(oldHealth);
				}
			}

			TimeSinceLightNeedsMet++;
			if (TimeSinceLightNeedsMet % LightingTimerInfo.CheckInterval == 0)
			{
				if (TimeSinceLightNeedsMet >= LightingTimerInfo.Sturdiness)
				{
					if (LightCheck)
					{
						float currentHealthImpact = HealthImpactModified(LightImpacts.NegativeImpact);
						PreUpdateHealth(ref currentHealthImpact);
						float oldHealth = GetHealth;
						GetHealth += HealthImpactModified(currentHealthImpact);
						OnHealthChange(oldHealth);
						TimeSinceLightNeedsMet = 0;
					}
					else
					{
						float currentHealthImpact = HealthImpactModified(LightImpacts.PositiveImpact);
						PreUpdateHealth(ref currentHealthImpact);
						float oldHealth = GetHealth;
						GetHealth += HealthImpactModified(currentHealthImpact);
						OnHealthChange(oldHealth);
					}
				}
			}

			Update();
		}

		public virtual void PreUpdateGrowth(ref float GrowthRate) { }

		public virtual void PreUpdateHealth(ref float HealthImpact) { }

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

		public virtual void OnHealthChange(float OldHealth) { }

		public virtual bool PreHarvest() => true;

		public virtual void OnHarvest(bool Elder) { }

		public virtual bool FulfilledExtraNeeds() => true;

		public void Harvest()
		{
			if (!Harvestable || !PreHarvest())
			{
				return;
			}

			bool Elder = CurrentStage == Stages.Elder;

			OnHarvest(Elder);
			HarvestTimer = 0;
			UpdateFraming();

			if (Elder)
			{
				WorldGen.KillTile(Position.X, Position.Y);
			}
		}

		public float HealthImpactModified(float input)
		{
			if (TileCheckDistance == 0 || NearbyUniqueTileInfluences.Count == 0)
			{
				return input;
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
			return input;
		}

		public sealed override TagCompound Save()
		{
			TagCompound data = new TagCompound()
			{
				{ "GrowthTimer", GrowthTimer % GrowthInfo.GrowthInterval },
				{ "Growth", Growth },
				{ "Health", Health },
				{ "WateringTimer", TimeSinceLastWatering >= WateringTimerInfo.Sturdiness ? (TimeSinceLastWatering % WateringTimerInfo.CheckInterval) + WateringTimerInfo.Sturdiness : TimeSinceLastWatering },
				{ "LightTimer", TimeSinceLightNeedsMet >= LightingTimerInfo.Sturdiness ? (TimeSinceLightNeedsMet % LightingTimerInfo.CheckInterval) + LightingTimerInfo.Sturdiness : TimeSinceLightNeedsMet },
				{ "HarvestTimer", HarvestTimer >= HarvestableTime ? HarvestableTime : HarvestTimer },
				{ "Extra", SaveExtra() },
			};
			return data;
		}

		public virtual TagCompound SaveExtra() => null;

		public sealed override void Load(TagCompound tagCompound)
		{
			GrowthTimer = tagCompound.Get<int>("GrowthTimer");
			GetGrowth = tagCompound.Get<float>("Growth");
			GetHealth = tagCompound.Get<float>("Health");
			TimeSinceLastWatering = tagCompound.Get<int>("WateringTimer");
			TimeSinceLightNeedsMet = tagCompound.Get<int>("LightTimer");
			HarvestTimer = tagCompound.Get<int>("HarvestTimer");
			LoadExtra(tagCompound.Get<TagCompound>("Extra"));
		}

		public virtual void LoadExtra(TagCompound tagCompound) { }
	}
}