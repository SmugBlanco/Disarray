using Disarray.Core.Data;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace Disarray.Core.Gardening
{
	public abstract class GardenEntity : TileData
	{
		public float GetHealth { get => Health; set => Health = Utils.Clamp(value, 0, 100); }
		protected float Health;

		public virtual (int Sturdiness, int CheckInterval) WateringTimerInfo { get; protected set; }
		public int TimeSinceLastWatering;
		public virtual (float NegativeImpact, float PositiveImpact) WaterImpacts { get; protected set; } = (-1, 1);

		public float GetMinimumLight { get => MinimumLight; set => MinimumLight = Utils.Clamp(value, 0f, 1f); }
		protected float MinimumLight;

		public virtual (int Sturdiness, int CheckInterval) LightingTimerInfo { get; protected set; }
		public int TimeSinceLightNeedsMet;
		public virtual (float NegativeImpact, float PositiveImpact) LightImpacts { get; protected set; } = (-1, 1);

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
				return Average(light.X, light.Y, light.Z) > GetMinimumLight;
			}
		}

		public sealed override void AI()
		{
			Dictionary<string, Dictionary<int, (int intname, List<string> listname)>> l = new Dictionary<string, Dictionary<int, (int intname, List<string> listname)>>();
			TimeSinceLastWatering++;
			if (TimeSinceLastWatering % WateringTimerInfo.CheckInterval == 0)
			{
				if (TimeSinceLastWatering >= WateringTimerInfo.Sturdiness)
				{
					GetHealth += HealthImpactModified(WaterImpacts.NegativeImpact);
				}
				else
				{
					GetHealth += HealthImpactModified(WaterImpacts.PositiveImpact);
				}
			}

			TimeSinceLightNeedsMet++;
			if (TimeSinceLightNeedsMet % LightingTimerInfo.CheckInterval == 0)
			{
				if (TimeSinceLightNeedsMet >= LightingTimerInfo.Sturdiness)
				{
					if (LightCheck)
					{
						GetHealth += HealthImpactModified(LightImpacts.NegativeImpact);
					}
					else
					{
						TimeSinceLightNeedsMet = 0;
					}
				}
				else
				{
					GetHealth += HealthImpactModified(LightImpacts.PositiveImpact);
				}
			}

			Update();
		}

		public virtual void Update() { }

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
	}
}