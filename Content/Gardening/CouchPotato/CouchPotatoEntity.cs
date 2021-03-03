using Disarray.Core.Data;
using Disarray.Core.Gardening;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Gardening.CouchPotato
{
	public class CouchPotatoEntity : GardenEntity
	{
		public override int HarvestableTime => 3600;

		public override (int GrowthInterval, float GrowthRate, float RequiredMinimumHealth) GrowthInfo => (600, 0.5f, 25f);

		public override (int Sturdiness, int CheckInterval) WateringTimerInfo => (3600, 300);

		public override (float NegativeImpact, float PositiveImpact) WaterImpacts => (-0.5f, 0.5f);

		public override float MinimumLight => 0.75f;

		public override (int Sturdiness, int CheckInterval) LightingTimerInfo => (60 * 60, 60 * 5);

		public override (float NegativeImpact, float PositiveImpact) LightImpacts => (-0.75f, 0.5f);

		public override int TileCheckDistance => 0;

		public override void Update()
		{
		
		}

		public override void OnHarvest(bool Elder)
		{
			for (int indexer = 0; indexer < 12; indexer++)
			{
				Dust.NewDustPerfect(Position.ToWorldCoordinates(), DustID.SapphireBolt, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5)));
			}
		}

		public override bool CanSurvive()
		{
			return Framing.GetTileSafely(Position).type == ModContent.TileType<CouchPotatoPlant>();
		}
	}
}