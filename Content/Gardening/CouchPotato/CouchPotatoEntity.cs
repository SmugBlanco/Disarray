using Disarray.Core.Gardening;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Gardening.CouchPotato
{
	public class CouchPotatoEntity : GardenEntity
	{
		public override int HarvestableTime => 72000;

		public override (int GrowthInterval, float GrowthRate, float RequiredMinimumHealth) GrowthInfo => (3600, 1f, 33f); //100 minutes for full growth

		public CouchPotatoEntity()
		{
			base.Needs = new HashSet<PlantNeeds>();
			ICollection<PlantNeeds> newNeeds = new HashSet<PlantNeeds>
			{
				PlantNeeds.CreateNewInstance(PlantNeeds.GetPlantNeeds("Thirst")),
				PlantNeeds.CreateNewInstance(PlantNeeds.GetPlantNeeds("Light"))
			};
			Needs = newNeeds.ToArray();
		}

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