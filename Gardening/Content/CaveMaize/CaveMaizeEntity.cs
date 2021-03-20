using Disarray.Gardening.Content.CaveMaize.Items.Accessories;
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

namespace Disarray.Gardening.Content.CaveMaize
{
	public class CaveMaizeEntity : GardenEntity
	{
		public override (int GrowthInterval, float GrowthRate) GrowthInfo => (3600, 1f); //100 minutes for full growth

		public override void SetUpNeeds()
		{
			PlantNeeds plantNeeds = GetClass<PlantNeeds>();
			ICollection<PlantNeeds> newNeeds = new HashSet<PlantNeeds>
			{
				PlantNeeds.CreateNewInstance(plantNeeds.GetData<Thirst>(), this),
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
			Vector2 worldPosition = Position.ToWorldCoordinates();

			for (int indexer = 0; indexer < 12; indexer++)
			{
				Dust.NewDustPerfect(worldPosition, DustID.Stone, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5))).noGravity = true;
			}

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				Item.NewItem(worldPosition, ModContent.ItemType<CaveMaizeCob>());

				if (Elder)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.MiningHelmet, ItemID.MiningShirt, ItemID.MiningPants, ModContent.ItemType<CoalRing>(), ItemID.MagicLantern));
				}

				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem(worldPosition, ItemID.SpelunkerGlowstick, Main.rand.Next(3, 11));
				}

				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.CopperOre, ItemID.TinOre), Main.rand.Next(25, 34));
				}

				Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.IronOre, ItemID.LeadOre), Main.rand.Next(25, 34));

				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.SilverOre, ItemID.TungstenOre), Main.rand.Next(25, 34));
				}

				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.GoldOre, ItemID.PlatinumOre), Main.rand.Next(25, 34));
				}

				for (int gemLoop = 0; gemLoop < (NPC.downedBoss1 ? 3 : 1); gemLoop++)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.Amethyst, ItemID.Topaz, ItemID.Sapphire, ItemID.Emerald, ItemID.Ruby, ItemID.Amber, ItemID.Diamond), Main.rand.Next(3, 11));
				}

				if (Main.rand.Next(NPC.downedBoss2 ? 3 : 5) == 0)
				{
					Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.DemoniteOre, ItemID.CrimtaneOre), NPC.downedBoss2 ? Main.rand.Next(25, 34) : Main.rand.Next(10, 16));
				}

				if (WorldGen.shadowOrbSmashed)
				{
					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(worldPosition, ItemID.Meteorite, NPC.downedBoss2 ? Main.rand.Next(25, 34) : Main.rand.Next(5, 16));
					}
				}

				if (Main.rand.Next(NPC.downedBoss2 ? 3 : 5) == 0)
				{
					Item.NewItem(worldPosition, ItemID.Obsidian, NPC.downedBoss2 ? Main.rand.Next(10, 16) : Main.rand.Next(5, 11));
				}

				if (NPC.downedBoss2)
				{
					if (Main.rand.Next(Main.hardMode ? 3 : 5) == 0)
					{
						Item.NewItem(worldPosition, ItemID.Hellstone, Main.hardMode ? Main.rand.Next(25, 34) : Main.rand.Next(10, 16));
					}
				}

				if (Main.hardMode)
				{
					if (Main.rand.Next(NPC.downedMechBossAny ? 2 : 3) == 0)
					{
						Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.CobaltOre, ItemID.PalladiumOre), Main.rand.Next(20, 31));
					}

					if (Main.rand.Next(NPC.downedMechBossAny ? 2 : 3) == 0)
					{
						Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.MythrilOre, ItemID.OrichalcumOre), NPC.downedMechBossAny ? Main.rand.Next(20, 31) : Main.rand.Next(10, 21));
					}

					if (Main.rand.Next(NPC.downedMechBossAny ? 3 : 5) == 0)
					{
						Item.NewItem(worldPosition, Utils.SelectRandom(Main.rand, ItemID.AdamantiteOre, ItemID.TitaniumOre), NPC.downedMechBossAny ? Main.rand.Next(20, 31) : Main.rand.Next(10, 21));
					}
				}

				if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
				{
					if (Main.rand.Next(NPC.downedPlantBoss ? 4 : 6) == 0)
					{
						Item.NewItem(worldPosition, ItemID.ChlorophyteOre, NPC.downedPlantBoss ? Main.rand.Next(10, 21) : Main.rand.Next(5, 11));
					}
				}
			}
		}

		public override bool CanSurvive() => Framing.GetTileSafely(Position).type == ModContent.TileType<CaveMaizePlant>();
	}
}