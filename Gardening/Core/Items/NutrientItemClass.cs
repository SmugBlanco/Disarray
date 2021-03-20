using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Linq;
using DustID = Disarray.ID.DustID;
using Terraria.DataStructures;
using Terraria.ObjectData;
using System;
using Disarray.Core.Globals;
using Disarray.Gardening.Core.Tiles;
using Disarray.Gardening.Core.GE;
using Disarray.Gardening.Core.Needs;

namespace Disarray.Gardening.Core.Items
{
	public abstract class NutrientItemClass : ModItem
	{
		public abstract string ItemName { get; protected set; }

		public abstract int MaxReach { get; protected set; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(ItemName);
			Tooltip.SetDefault("Allows you to feed plants");
		}

		public override bool UseItem(Player player)
		{
			for (int indexer = 0; indexer < 2; indexer++)
			{
				Dust.NewDustPerfect(Main.MouseWorld, DustID.Ambient_DarkBrown, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-5, 0)), Scale: 1.25f);
			}

			Point16 mousePosition = Main.MouseWorld.ToTileCoordinates16();

			if (player.itemAnimation == 1)
			{
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

							PlantNeeds need = gardenEntity.Needs.FirstOrDefault(needs => needs is Hunger);

							if (need is Hunger hunger)
							{
								hunger.GetTimer = 0;
							}
						}
					}
				}

				return true;
			}

			return false;
		}
	}
}