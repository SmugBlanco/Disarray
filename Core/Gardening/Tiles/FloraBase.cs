using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Disarray.Core.Globals;
using Disarray.Core.Data;
using Terraria.ObjectData;
using System.Linq;

namespace Disarray.Core.Gardening.Tiles
{
	public abstract class FloraBase : ModTile
	{
		public static ICollection<FloraBase> LoadedBases;

		public sealed override bool Autoload(ref string name, ref string texture)
		{
			LoadedBases = new Collection<FloraBase>();
			LoadedBases.Add(this);
			return true;
		}

		public static void Unload()
		{
			LoadedBases?.Clear();
		}

		public virtual short Height => (short)(TileObjectData.GetTileData(Type, 0).Height * 18);

		public virtual short Width => (short)(TileObjectData.GetTileData(Type, 0).Width * 18);

		public override bool NewRightClick(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			Point16 OriginTile = new Point16(i, j) - new Point16(tile.frameX % Width / 18, tile.frameY % Height / 18);
			OriginTile += TileObjectData.GetTileData(tile).Origin;
			if (DisarrayWorld.GardenEntitiesByPosition.TryGetValue(OriginTile, out TileData tileData))
			{
				GardenEntity gardenEntity = tileData as GardenEntity;
				gardenEntity.Harvest();
				return true;
			}
			Main.NewText("Pain");
			return false;
		}

		public static Color TaintColor(Color oldColor, Color desiredColor, float amount)
		{
			amount = Utils.Clamp(amount, 0, 1);
			(float Red, float Green, float Blue, float Alpha) colorValues = (desiredColor.R - oldColor.R, desiredColor.G - oldColor.G, desiredColor.B - oldColor.G, desiredColor.A - oldColor.A);
			colorValues.Alpha *= amount;
			colorValues.Red *= amount;
			colorValues.Green *= amount;
			colorValues.Blue *= amount;
			return Color.FromNonPremultiplied((int)(oldColor.R + colorValues.Red), (int)(oldColor.G + colorValues.Green), (int)(oldColor.B + colorValues.Blue), (int)(oldColor.A + colorValues.Alpha));
		}

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (DisarrayWorld.GardenEntitiesByPosition.TryGetValue(new Point16(i, j), out TileData tileData))
			{
				Tile tile = Framing.GetTileSafely(i, j);
				GardenEntity entity = tileData as GardenEntity;

				int tileWidth = Width / 18;
				int tileHeight = Height / 18;

				Point16 OriginTile = new Point16(i, j) - new Point16(tile.frameX % Width / 16, tile.frameY % Height / 16);

				Vector2 offset = new Vector2(Main.offScreenRange, Main.offScreenRange);
				if (Main.drawToScreen)
				{
					offset = Vector2.Zero;
				}

				for (int X = OriginTile.X; X < OriginTile.X + tileWidth; X++)
				{
					for (int Y = OriginTile.Y; Y < OriginTile.Y + tileHeight; Y++)
					{
						Vector2 drawPosition = new Vector2(X, Y).ToWorldCoordinates(Vector2.Zero) - Main.screenPosition + offset;
						Tile drawnTile = Framing.GetTileSafely(X, Y);
						Rectangle sourceRectangle = new Rectangle(drawnTile.frameX, drawnTile.frameY, 16, 16);

						Color drawnColor = Lighting.GetColor(X, Y);
						if (entity.GetGrowth >= 100)
						{
							drawnColor = TaintColor(drawnColor, new Color(150, 225, 250, 100), 1);
						}
						else
						{
							drawnColor = TaintColor(drawnColor, new Color(25, 25, 25, 200), 1 - entity.GetHealth / 100);
						}

						spriteBatch.Draw(Main.tileTexture[Type], drawPosition, sourceRectangle, drawnColor);
					}
				}

				HandleDrawingNeeds(spriteBatch, entity, OriginTile.ToWorldCoordinates(Vector2.Zero) - Main.screenPosition + offset);
			}

			return false;
		}

		public void HandleDrawingNeeds(SpriteBatch spriteBatch, GardenEntity entity, Vector2 originDrawPosition)
		{
			ICollection<Texture2D> drawnNeedsTextures = new Collection<Texture2D>();
			if (entity.TimeSinceLastWatering > entity.WateringTimerInfo.Sturdiness)
			{
				drawnNeedsTextures.Add(ModContent.GetTexture("Disarray/Core/Gardening/Tiles/NeedsIndicator_Water"));
			}

			if (entity.TimeSinceLightNeedsMet > entity.LightingTimerInfo.Sturdiness)
			{
				drawnNeedsTextures.Add(ModContent.GetTexture("Disarray/Core/Gardening/Tiles/NeedsIndicator_Light"));
			}

			if (!entity.FulfilledExtraNeeds())
			{
				drawnNeedsTextures.Add(ModContent.GetTexture("Disarray/Core/Gardening/Tiles/NeedsIndicator_Extra"));
			}

			DrawNeeds(spriteBatch, new Rectangle((int)originDrawPosition.X, (int)originDrawPosition.Y, Width, 2), drawnNeedsTextures.ToArray());
		}

		public void DrawNeeds(SpriteBatch spriteBatch, Rectangle drawBounds, params Texture2D[] needsToDraw)
		{
			for (int indexer = 0; indexer < needsToDraw.Length; indexer++)
			{
				Texture2D texture = needsToDraw[indexer];
				Vector2 drawPosition = drawBounds.TopLeft() + new Vector2((TileObjectData.GetTileData(Type, 0).Width * 16) / (needsToDraw.Length + 1) * (indexer + 1), 0);
				spriteBatch.Draw(texture, drawPosition, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
			}
		}

		public void SyncTile(int i, int j)
		{
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendTileSquare(-1, i, j, Width > Height ? Width / 18 : Height / 18);
			}
		}
	}
}