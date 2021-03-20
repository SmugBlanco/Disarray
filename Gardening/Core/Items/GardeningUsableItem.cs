using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Disarray.Core.Globals;
using System;
using Terraria.ModLoader.IO;
using Disarray.Gardening.Core.GE;
using Disarray.Gardening.Core.Tiles;
using Disarray.Core.Properties;

namespace Disarray.Gardening.Core.Items
{
	public abstract class GardeningUsableItem : ModItem
	{
		public sealed override bool CloneNewInstances => true;

		public abstract string ItemName { get; protected set; }

		public Point ItemDimensions;

		public abstract int MaxQuantity { get; protected set; }

		public abstract int MaxReach { get; protected set; }

		public float GetQuantity { get => Quantity; set => Quantity = Utils.Clamp(value, 0, MaxQuantity); }

		private float Quantity;

		public int GetTimeSinceLastInteraction { get => TimeSinceLastInteraction; set => TimeSinceLastInteraction = Utils.Clamp(value, 0, 999); }

		private int TimeSinceLastInteraction;

		public sealed override void SetDefaults()
		{
			Defaults();

			item.width = ItemDimensions.X;
			item.height = ItemDimensions.Y;
		}

		public virtual void Defaults() { }

		public virtual void UseDust(Player player) { }

		public virtual void OnUse(GardenEntity gardenEntity, Player player) { }

		public override void UpdateInventory(Player player) => GetTimeSinceLastInteraction--;

		public sealed override bool UseItem(Player player)
		{
			GetTimeSinceLastInteraction = 0;

			if (GetQuantity <= 0)
			{
				return false;
			}

			UseDust(player);

			Point16 mousePosition = Main.MouseWorld.ToTileCoordinates16();

			if (player.itemAnimation == 1)
			{
				GetQuantity--;

				if (Vector2.DistanceSquared(mousePosition.ToVector2(), player.Center.ToTileCoordinates().ToVector2()) < Math.Pow(MaxReach, 2))
				{
					Tile tile = Framing.GetTileSafely(mousePosition);

					if (ModContent.GetModTile(tile.type) is FloraBase flora)
					{
						Point16 OriginTile = mousePosition - new Point16(tile.frameX % flora.Width / 18, tile.frameY % flora.Height / 18);
						OriginTile += TileObjectData.GetTileData(tile).Origin;

						if (DisarrayWorld.GardenEntitiesByPosition.TryGetValue(OriginTile, out TileData tileData))
						{
							GardenEntity gardenEntity = tileData as GardenEntity;
							OnUse(gardenEntity, player);
						}
					}
				}
			}

			return false;
		}

		public virtual void Refill(Player player) { }

		public override TagCompound Save() => new TagCompound() { { "Quantity", GetQuantity } };

		public override void Load(TagCompound tag) => GetQuantity = tag.Get<float>("Quantity");
	}

	public class GardeningItem : PlayerProperty // Not an elegant solution, but it works. Solved Problem: Can UI not disappearing when held in the mouse slot
	{
		public override void PostLoadType() => DisarrayGlobalPlayer.GlobalProperties.Add(this);

		public override void PostUpdateMiscEffects(Player player)
		{
			Item targettedItem = Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;
			if (targettedItem?.modItem is GardeningUsableItem gardeningItem)
			{
				gardeningItem.GetTimeSinceLastInteraction += 2;
			}
		}
	}
}