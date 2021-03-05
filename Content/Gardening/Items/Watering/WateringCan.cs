using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Disarray.Core.Gardening.Tiles;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Disarray.Core.Data;
using Disarray.Core.Globals;
using Disarray.Core.Gardening;
using System;
using Disarray.Core.Gardening.UI;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Disarray.Core.Properties;

namespace Disarray.Content.Gardening.Items.Watering
{
	public class WateringCan : ModItem
	{
		public override bool CloneNewInstances => true;

		public int MaxDistance => 4;

		public int WateringEffectiveness = 25;

		public float GetWaterLevel { get => WaterLevel; set => WaterLevel = Utils.Clamp(value, 0, 100); }

		private float WaterLevel;

		public int TimeSinceLastInteract;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Watering Can");
			Tooltip.SetDefault("Watering speed: 5 ( average )"
			+ "\nWatering reach: " + MaxDistance
			+ "\nWatering capacity: 400 uses per refill");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.LiquidsHoneyWater;
			item.useTime = 5;
			item.useAnimation = 5;
			item.holdStyle = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override bool UseItem(Player player)
		{
			TimeSinceLastInteract = 0;

			if (GetWaterLevel <= 0)
			{
				return false;
			}

			Dust.NewDustPerfect(player.itemLocation + new Vector2(36 * player.direction, 8), Core.Data.DustID.Water, new Vector2(1 * player.direction, 0));

			GetWaterLevel -= 0.25f;

			Point16 point = new Point16((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
			if (Vector2.DistanceSquared(point.ToVector2(), player.Center.ToTileCoordinates().ToVector2()) < Math.Pow(MaxDistance + 2, 2))
			{
				if (player.itemAnimation == 1)
				{
					Tile tile = Framing.GetTileSafely(point);
					if (ModContent.GetModTile(tile.type) is FloraBase flora)
					{
						Point16 OriginTile = new Point16(point.X, point.Y) - new Point16(tile.frameX % flora.Width / 18, tile.frameY % flora.Height / 18);
						OriginTile += TileObjectData.GetTileData(tile).Origin;
						if (DisarrayWorld.GardenEntitiesByPosition.TryGetValue(OriginTile, out TileData tileData))
						{
							for (int indexer = 0; indexer < 3; indexer++)
							{
								Dust.NewDustPerfect(Main.MouseWorld, Core.Data.DustID.Water, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2)));
							}

							GardenEntity gardenEntity = tileData as GardenEntity;
							if (gardenEntity.SetTimeSinceLastWatering > gardenEntity.WateringTimerInfo.Sturdiness)
							{
								gardenEntity.SetTimeSinceLastWatering = gardenEntity.WateringTimerInfo.Sturdiness;
							}

							gardenEntity.SetTimeSinceLastWatering -= WateringEffectiveness;

							if (gardenEntity.SetTimeSinceLastWatering < 0)
							{
								gardenEntity.SetTimeSinceLastWatering = 0;
							}
						}
					}
				}
			}
			return false;
		}

		public override void HoldItem(Player player)
		{
			if (ModContent.GetInstance<Disarray>().GardeningInterface.CurrentState == null)
			{
				ModContent.GetInstance<Disarray>().GardeningInterface?.SetState(new WaterDisplay());
				TimeSinceLastInteract = 0;
			}

			player.itemLocation += new Vector2(player.direction == 1 ? -12 : 12, 14);

			if (player.altFunctionUse == 2 && GetWaterLevel < 100)
			{
				TimeSinceLastInteract = 0;
				Tile tile = Framing.GetTileSafely(player.Bottom.ToTileCoordinates16() + new Point16(0, -1));
				if (tile.liquidType() == 0 && tile.liquid >= 51)
				{
					GetWaterLevel++;
				}
			}
		}

		public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player)
		{
			if (GetWaterLevel <= 0)
			{
				item.UseSound = null;
			}
			else
			{
				item.UseSound = SoundID.LiquidsHoneyWater;
			}

			return player.altFunctionUse != 2;
		}

		public override void UpdateInventory(Player player) => TimeSinceLastInteract++;

		public override void UseStyle(Player player)
		{
			player.itemLocation += new Vector2(player.direction == 1 ? 4 : -4, -12);
			player.itemRotation = player.direction == 1 ? MathHelper.ToRadians(45) : MathHelper.ToRadians(-45);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "GardeningWaterPercentage", GetWaterLevel + "/100% full."));
		}

		public override TagCompound Save()
		{
			return new TagCompound()
			{
				{ "Water", GetWaterLevel }
			};
		}

		public override void Load(TagCompound tag)
		{
			GetWaterLevel = tag.Get<float>("Water");
		}
	}

	public class WateringCanClass : PlayerProperty // Not an elegant solution, but it works. Solved Problem: Can UI not disappearing when held in the mouse slot
	{
		public override void PostLoad(PlayerProperty playerProperty)
		{
			DisarrayGlobalPlayer.GlobalProperties.Add(playerProperty);
		}

		public override void PostUpdateMiscEffects(Player player)
		{
			if (Main.mouseItem?.modItem is WateringCan can)
			{
				can.TimeSinceLastInteract++;
			}
		}
	}
}