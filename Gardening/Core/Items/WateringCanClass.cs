using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using DustID = Disarray.ID.DustID;
using Disarray.Gardening.Core.UI.Watering;
using Disarray.Gardening.Core.GE;
using Disarray.Gardening.Core.Needs;

namespace Disarray.Gardening.Core.Items
{
	public abstract class WateringCanClass : GardeningUsableItem
	{
		public abstract int WateringSpeed { get; protected set; }

		public override bool AltFunctionUse(Player player) => true;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(ItemName);
			Tooltip.SetDefault("Allows you to water plants"
			+ "\nMax capacity: " + (MaxQuantity / 25f) + " liters");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) => tooltips.Add(new TooltipLine(mod, "GardeningWaterPercentage", "Contents: " + (GetQuantity / 25f) + " liters of water. ( " + (100f / MaxQuantity * GetQuantity) + "/100% )"));

		public override void UseDust(Player player) => Dust.NewDustPerfect(player.itemLocation + new Vector2(ItemDimensions.X * player.direction, ItemDimensions.Y / 3f), DustID.Water, new Vector2(1 * player.direction, 0));

		public sealed override void OnUse(GardenEntity gardenEntity, Player player)
		{
			for (int indexer = 0; indexer < 3; indexer++)
			{
				Dust.NewDustPerfect(Main.MouseWorld, DustID.Water, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2)));
			}

			PlantNeeds need = gardenEntity.Needs.FirstOrDefault(needs => needs is Thirst);

			if (need is Thirst thirst)
			{
				if (thirst.GetTimer > thirst.Sturdiness)
				{
					thirst.GetTimer = thirst.Sturdiness;
				}

				thirst.GetTimer -= 1200;
			}
		}

		public sealed override void HoldItem(Player player)
		{
			if (ModContent.GetInstance<Disarray>().GardeningInterface.CurrentState == null)
			{
				ModContent.GetInstance<Disarray>().GardeningInterface?.SetState(new WaterDisplay());
				GetTimeSinceLastInteraction = 0;
			}

			int offsetX = (int)(ItemDimensions.X / 2.75f);
			int offsetY = (int)(ItemDimensions.Y / 2f);
			player.itemLocation += new Vector2(player.direction == 1 ? -offsetX : offsetX, offsetY);

			if (player.altFunctionUse == 2)
			{
				GetTimeSinceLastInteraction = 0;

				if (GetQuantity < MaxQuantity)
				{
					Refill(player);
				}
			}
		}

		public sealed override void Refill(Player player)
		{
			Tile tile = Framing.GetTileSafely(player.Bottom.ToTileCoordinates16() + new Point16(0, -1));
			if (tile.liquidType() == 0 && tile.liquid >= 51)
			{
				GetQuantity += MaxQuantity / 100f;
			}
		}

		public sealed override bool CanUseItem(Player player)
		{
			if (GetQuantity <= 0)
			{
				item.UseSound = null;
			}
			else
			{
				item.UseSound = SoundID.LiquidsHoneyWater;
			}

			return player.altFunctionUse != 2;
		}

		public sealed override void UseStyle(Player player)
		{
			int offsetX = (int)(ItemDimensions.X / 8.5f);
			int offsetY = (int)(ItemDimensions.Y / 2f);
			player.itemLocation += new Vector2(player.direction == 1 ? offsetX : -offsetX, -offsetY);
			player.itemRotation = player.direction == 1 ? MathHelper.ToRadians(45) : MathHelper.ToRadians(-45);
		}
	}
}