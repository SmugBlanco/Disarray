using Disarray.Content.Gardening.Needs;
using Disarray.Content.Gardening.Needs.PestTypes;
using Disarray.Content.Gardening.SwordFern.Items;
using Disarray.Core.Gardening;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DustID = Disarray.Core.Data.DustID;

namespace Disarray.Content.Gardening.SwordFern
{
	public class SwordFernEntity : GardenEntity
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
				Dust.NewDustPerfect(Position.ToWorldCoordinates(), DustID.Grass, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5))).noGravity = true;
			}

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				Item.NewItem(Position.ToWorldCoordinates(), ModContent.ItemType<SwordFernsBlade>());

				if (Elder)
				{
					Item.NewItem(Position.ToWorldCoordinates(), ModContent.ItemType<SwordFernSeed>());
				}

				if (Elder || Main.rand.Next(5) == 0)
				{
					Item.NewItem(Position.ToWorldCoordinates(), Utils.SelectRandom(Main.rand, ModContent.ItemType<TheStrike>(), ModContent.ItemType<ThePierce>(), ModContent.ItemType<ThePort>(), ModContent.ItemType<TheDash>(), ModContent.ItemType<TheTank>(), ModContent.ItemType<TheBetrayal>(), ModContent.ItemType<ThePact>()));
				}

				if (Main.hardMode)
				{
					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(Position.ToWorldCoordinates(), Utils.SelectRandom(Main.rand, ItemID.SharpeningStation, ItemID.AmmoBox, ItemID.CrystalBall, ItemID.BewitchingTable));
					}
				}
			}
		}

		public override bool CanSurvive() => Framing.GetTileSafely(Position).type == ModContent.TileType<SwordFernPlant>();
	}
}