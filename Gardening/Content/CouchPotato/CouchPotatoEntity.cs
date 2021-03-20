using Disarray.Gardening.Core;
using Disarray.Gardening.Core.GE;
using Disarray.Gardening.Core.Needs;
using Disarray.Gardening.Core.Needs.PestTypes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DustID = Disarray.ID.DustID;

namespace Disarray.Gardening.Content.CouchPotato
{
	public class CouchPotatoEntity : GardenEntity
	{
		public override (int GrowthInterval, float GrowthRate) GrowthInfo => (3600, 1f); //100 minutes for full growth

		public override void SetUpNeeds()
		{
			PlantNeeds plantNeeds = GetClass<PlantNeeds>();
			ICollection<PlantNeeds> newNeeds = new HashSet<PlantNeeds>
			{
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Thirst>(), this),
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Light>(), this),
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Hunger>(), this)
			};

			Harvest harvest = PlantNeeds.CreateNewInstance(plantNeeds.GetData<Harvest>(), this) as Harvest;
			harvest.Sturdiness = 72000;
			newNeeds.Add(harvest);

			Pests pest = PlantNeeds.CreateNewInstance(plantNeeds.GetData<Pests>(), this) as Pests;
			pest.ApplicablePests.Add(GetClass<PestEntity>().GetData<Flies>());
			newNeeds.Add(pest);
			Needs = newNeeds.ToArray();
		}

		public override int TileCheckDistance => 0;

		public override void OnHarvest(bool Elder)
		{
			for (int indexer = 0; indexer < 12; indexer++)
			{
				Dust.NewDustPerfect(Position.ToWorldCoordinates(), DustID.SapphireBolt, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5)));
			}

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				Item.NewItem(Position.ToWorldCoordinates(), ModContent.ItemType<CouchPotatoSpud>());

				if (Elder)
				{
					Item.NewItem(Position.ToWorldCoordinates(), ModContent.ItemType<CouchPotatoSeed>());
				}
			}
		}

		public override bool CanSurvive() => Framing.GetTileSafely(Position).type == ModContent.TileType<CouchPotatoPlant>();
	}
}