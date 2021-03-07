using Disarray.Content.Gardening.Needs;
using Disarray.Content.Gardening.Needs.PestTypes;
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

		public override (int GrowthInterval, float GrowthRate) GrowthInfo => (3600, 1f); //100 minutes for full growth

		public CouchPotatoEntity()
		{
			base.Needs = new HashSet<PlantNeeds>();
			PlantNeeds plantNeeds = GetClass<PlantNeeds>();
			ICollection<PlantNeeds> newNeeds = new HashSet<PlantNeeds>
			{
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Thirst>(), this),
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Light>(), this)
			};
			Pests pest = PlantNeeds.CreateNewInstance(plantNeeds.GetData<Pests>(), this) as Pests;
			newNeeds.Add(pest);
			Needs = newNeeds.ToArray();
		}

		public override void OnPlace()
		{
			if (Needs.FirstOrDefault(needs => needs is Pests) is Pests pest)
			{
				pest.CurrentPests.Add(PestEntity.CreateNewInstance(GetClass<PestEntity>().GetData<Flies>(), this));
			}
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