using Disarray.Core.Data;
using Disarray.Core.Gardening;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Gardening.CouchPotato
{
	public class CouchPotatoEntity : GardenEntity
	{
		public override int HarvestableTime => 72000;

		public override (int GrowthInterval, float GrowthRate, float RequiredMinimumHealth) GrowthInfo => (3600, 1f, 33f); //1 hour for full growth

		public override (int Sturdiness, int CheckInterval) WateringTimerInfo => (18000, 1800);

		public override (float NegativeImpact, float PositiveImpact) WaterImpacts => (-2.5f, 0.5f);

		public override float MinimumLight => 0.75f;

		public override (int Sturdiness, int CheckInterval) LightingTimerInfo => (54000, 3600);

		public override (float NegativeImpact, float PositiveImpact) LightImpacts => (-1f, 0.5f);

		public override int TileCheckDistance => 0;

		public override void Update()
		{
		
		}

		public override void OnHarvest(bool Elder)
		{
			for (int indexer = 0; indexer < 12; indexer++)
			{
				Dust.NewDustPerfect(Position.ToWorldCoordinates(), Core.Data.DustID.SapphireBolt, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5)));
			}
		}

		public override bool CanSurvive()
		{
			return Framing.GetTileSafely(Position).type == ModContent.TileType<CouchPotatoPlant>();
		}
	}
}