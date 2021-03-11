using Disarray.Content.Gardening.Needs;
using Disarray.Content.Gardening.Needs.PestTypes;
using Disarray.Core.Gardening;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DustID = Disarray.Core.Data.DustID;

namespace Disarray.Content.Gardening.PricklyPearBear
{
	public class PricklyPearBearEntity : GardenEntity
	{
		public override (int GrowthInterval, float GrowthRate) GrowthInfo => (3600, 1f); //100 minutes for full growth

		public override void SetUpNeeds()
		{
			PlantNeeds plantNeeds = GetClass<PlantNeeds>();
			ICollection<PlantNeeds> newNeeds = new HashSet<PlantNeeds>
			{
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Light>(), this),
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Hunger>(), this)
			};

			Harvest harvest = PlantNeeds.CreateNewInstance(plantNeeds.GetData<Harvest>(), this) as Harvest;
			harvest.Sturdiness = 3600;
			newNeeds.Add(harvest);

			Thirst thirst = PlantNeeds.CreateNewInstance(plantNeeds.GetData<Thirst>(), this) as Thirst;
			thirst.Sturdiness = 172800;
			newNeeds.Add(thirst);

			Pests pest = PlantNeeds.CreateNewInstance(plantNeeds.GetData<Pests>(), this) as Pests;
			pest.ApplicablePests.Add(GetClass<PestEntity>().GetData<Flies>());
			newNeeds.Add(pest);
			Needs = newNeeds.ToArray();
		}

		public override int TileCheckDistance => 0;

		public override void OnHarvest(bool Elder)
		{
			Vector2 worldPosition = Position.ToWorldCoordinates();

			for (int indexer = 0; indexer < 12; indexer++)
			{
				Dust.NewDustPerfect(worldPosition, DustID.JungleGrass, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5))).noGravity = true;
			}

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (Elder)
				{
					Item.NewItem(worldPosition, ModContent.ItemType<PricklyPearBearSeed>());

					Item.NewItem(worldPosition, ItemID.FallenStar, Main.rand.Next(10, 26));

					if (NPC.downedMoonlord)
					{
						Item.NewItem(worldPosition, ItemID.MoonlordArrow, Main.rand.Next(15, 51));

						Item.NewItem(worldPosition, ItemID.MoonlordBullet, Main.rand.Next(15, 51));
					}
				}

				Item.NewItem(worldPosition, ItemID.WoodenArrow, Main.rand.Next(15, 51));
				Item.NewItem(worldPosition, ItemID.Snowball, Main.rand.Next(25, 101));
				Item.NewItem(worldPosition, ItemID.Seed, Main.rand.Next(25, 76));

				if (WorldGen.shadowOrbSmashed)
				{
					Item.NewItem(worldPosition, ItemID.MusketBall, Main.rand.Next(15, 51));
				}

				if (NPC.downedSlimeKing)
				{
					Item.NewItem(worldPosition, ItemID.Gel, Main.rand.Next(5, 21));
				}

				if (NPC.downedBoss1)
				{
					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.FlamingArrow, Main.rand.Next(5, 26));
					}

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.BoneArrow, Main.rand.Next(5, 26));
					}

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.Flare, ItemID.BlueFlare), Main.rand.Next(10, 34));
					}
				}

				if (NPC.downedBoss2)
				{
					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.FrostburnArrow, Main.rand.Next(5, 16));
					}

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.SilverBullet, Main.rand.Next(5, 26));
					}

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.MeteorShot, Main.rand.Next(5, 26));
					}
				}

				if (NPC.downedQueenBee)
				{
					Item.NewItem(worldPosition, ItemID.PoisonDart, Main.rand.Next(5, 16));
				}

				if (NPC.downedBoss3)
				{
					Item.NewItem(worldPosition, ItemID.Bone, Main.rand.Next(5, 11));
				}

				if (Main.hardMode)
				{
					if (Main.rand.Next(2) == 0)
					{
						Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.CrystalDart, ItemID.CursedDart, ItemID.IchorDart), Main.rand.Next(5, 16));
					}

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.UnholyArrow, Main.rand.Next(5, 16));
					}

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.JestersArrow, Main.rand.Next(5, 16));
					}

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.HellfireArrow, Main.rand.Next(5, 16));
					}
				}

				if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.GreenSolution, ItemID.PurpleSolution, ItemID.RedSolution, ItemID.BlueSolution), Main.rand.Next(1, 6));
				}

				if (NPC.downedGolemBoss)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.RocketI, ItemID.RocketII, ItemID.RocketIII, ItemID.RocketIV), Main.rand.Next(5, 26));

					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.StyngerBolt, Main.rand.Next(5, 26));
					}
				}

				if (NPC.downedFishron)
				{
					if (Main.rand.Next(5) == 0)
					{
						Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.GreenSolution, ItemID.PurpleSolution, ItemID.RedSolution, ItemID.BlueSolution), Main.rand.Next(1, 6));
					}
				}
			}
		}

		public override bool CanSurvive() => Framing.GetTileSafely(Position).type == ModContent.TileType<PricklyPearBearPlant>();
	}
}