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
using System.Linq;
using Disarray.Content.Gardening.Needs;

namespace Disarray.Content.Gardening.Items.Watering
{
	public abstract class WateringCanClass : ModItem
	{
		public const int MaxDistance = 4;

		public const int WateringEffectiveness = 250;

		public abstract string ItemName { get; protected set; }

		public abstract Point Dimensions { get; protected set; }

		public abstract int WateringSpeed { get; protected set; }

		public abstract int MaxCapacity { get; protected set; }

		public float GetWaterLevel { get => WaterLevel; set => WaterLevel = (float)Math.Round(Utils.Clamp(value, 0, MaxCapacity), 2); }

		private float WaterLevel;

		public int GetTimeSinceLastInteraction { get => TimeSinceLastInteraction; set => TimeSinceLastInteraction = Utils.Clamp(value, 0, 999); }

		private int TimeSinceLastInteraction;

		public override bool CloneNewInstances => true;

		public override bool AltFunctionUse(Player player) => true;

		public override void UpdateInventory(Player player) => GetTimeSinceLastInteraction--;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(ItemName);
			Tooltip.SetDefault("Allows you to water plants"
			+ "\nMax capacity: " + (MaxCapacity / 100f) + " liters");
		}

		public override void SetDefaults()
		{
			item.width = Dimensions.X;
			item.height = Dimensions.Y;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.LiquidsHoneyWater;
			item.useTime = WateringSpeed;
			item.useAnimation = WateringSpeed;
			item.holdStyle = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) => tooltips.Add(new TooltipLine(mod, "GardeningWaterPercentage", "Contents: " + (GetWaterLevel / 100f) + " liters of water. ( " + (100f / MaxCapacity * GetWaterLevel) + "/100% )"));

		public override bool UseItem(Player player)
		{
			GetTimeSinceLastInteraction = 0;

			if (GetWaterLevel <= 0)
			{
				return false;
			}

			int offsetY = (int)(Dimensions.Y / 3f);
			Dust.NewDustPerfect(player.itemLocation + new Vector2(Dimensions.X * player.direction, offsetY), Core.Data.DustID.Water, new Vector2(1 * player.direction, 0));

			GetWaterLevel--;

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
							PlantNeeds need = gardenEntity.Needs.FirstOrDefault(prop => prop is Thirst);
							if (need is Thirst thirst)
							{
								if (thirst.GetTimer > thirst.Sturdiness)
								{
									thirst.GetTimer = thirst.Sturdiness;
								}

								thirst.GetTimer -= WateringEffectiveness;
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
				GetTimeSinceLastInteraction = 0;
			}

			int offsetX = (int)(Dimensions.X / 2.75f);
			int offsetY = (int)(Dimensions.Y / 2f);
			player.itemLocation += new Vector2(player.direction == 1 ? -offsetX : offsetX, offsetY);

			if (player.altFunctionUse == 2)
			{
				GetTimeSinceLastInteraction = 0;

				if (GetWaterLevel < MaxCapacity)
				{
					Refill(player);
				}
			}
		}

		public void Refill(Player player)
		{
			Tile tile = Framing.GetTileSafely(player.Bottom.ToTileCoordinates16() + new Point16(0, -1));
			if (tile.liquidType() == 0 && tile.liquid >= 51)
			{
				GetWaterLevel += MaxCapacity / 100f;
			}
		}

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

		public override void UseStyle(Player player)
		{
			int offsetX = (int)(Dimensions.X / 8.5f);
			int offsetY = (int)(Dimensions.Y / 2f);
			player.itemLocation += new Vector2(player.direction == 1 ? offsetX : -offsetX, -offsetY);
			player.itemRotation = player.direction == 1 ? MathHelper.ToRadians(45) : MathHelper.ToRadians(-45);
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

	public class WateringCanProperty : PlayerProperty // Not an elegant solution, but it works. Solved Problem: Can UI not disappearing when held in the mouse slot
	{
		public override void PostLoadType()
		{
			DisarrayGlobalPlayer.GlobalProperties.Add(this);
		}

		public override void PostUpdateMiscEffects(Player player)
		{
			Item targettedItem = Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;
			if (targettedItem?.modItem is WateringCanClass can)
			{
				can.GetTimeSinceLastInteraction += 2;
			}
		}
	}
}