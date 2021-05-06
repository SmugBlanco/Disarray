using Disarray.Core.Autoload;
using Disarray.Core.Globals;
using Disarray.Core.Map;
using Disarray.Forge.Core.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Disarray
{
	public class Disarray : Mod
	{
		public static Disarray GetMod { get; private set; }

		public Disarray() => GetMod = this;

		public static bool Loading { get; private set; }

		public ICollection<MapEntry> MapEntries;

		internal UserInterface ForgeUserInterface;
		internal UserInterface AlmanacUserInterface;
		internal UserInterface GardeningInterface;

		public override bool LoadResource(string path, int length, Func<Stream> getStream)
		{
			MapEntries = new Collection<MapEntry>();
			DisarrayGlobalNPC.Load();
			DisarrayGlobalPlayer.Load();
			ForgeCore.Load();
			AutoloadedClass.Load();
			return base.LoadResource(path, length, getStream);
		}

		public override void Load()
		{
			if (Code == null)
			{
				return;
			}

			Loading = true;

			On.Terraria.Main.DrawMap += Main_DrawMap;
			On.Terraria.Item.IsTheSameAs += Item_IsTheSameAs;

			foreach (Type item in Code.GetTypes())
			{
				if (!item.IsAbstract && item.GetConstructor(new Type[0]) != null)
				{
					if (item.IsSubclassOf(typeof(AutoloadedClass)))
					{
						AutoloadedClass.LoadType(item);
					}
				}
			}

			AutoloadedClass.PostLoadType(Code);

			if (!Main.dedServ)
			{
				Filters.Scene["Pestillence"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/Pestillence")), "Pestillence"), EffectPriority.VeryLow);
				Filters.Scene["Pestillence"].Load();

				Filters.Scene["ProtectiveSights"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/ProtectiveSights")), "ProtectiveSights"), EffectPriority.Medium);
				Filters.Scene["ProtectiveSights"].Load();

				Filters.Scene["Flashbang"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/Flashbang")), "Flashbang"), EffectPriority.VeryHigh);
				Filters.Scene["Flashbang"].Load();

				ForgeUserInterface = new UserInterface();
				AlmanacUserInterface = new UserInterface();
				GardeningInterface = new UserInterface();
			}

			Loading = false;
		}

		private bool Item_IsTheSameAs(On.Terraria.Item.orig_IsTheSameAs orig, Item self, Item compareItem)
		{
			if (self?.modItem is null || compareItem?.modItem is null)
			{
				return orig(self, compareItem);
			}

			if (self.modItem is ForgeItem forgeItem && compareItem.modItem is ForgeItem comparedForgeItem)
			{
				return forgeItem.Equals(comparedForgeItem);
			}

			return orig(self, compareItem);
		}

		public override void PostSetupContent()
		{
			foreach (AutoloadedClass autoloadedClass in AutoloadedClass.LoadedClasses)
			{
				foreach (AutoloadedClass autoloadedData in autoloadedClass.LoadedData)
				{
					autoloadedData.PostSetupContent();
				}
			}
		}

		public override void AddRecipeGroups() // OOPize later
		{
			RecipeGroup recipeGroup = new RecipeGroup(() =>  "Copper or Tin Bars", new int[] { ItemID.CopperBar, ItemID.TinBar });
			RecipeGroup.RegisterGroup("Disarray:CopperBar", recipeGroup);

			recipeGroup = new RecipeGroup(() => "Silver or Tungsten Bars", new int[] { ItemID.SilverBar, ItemID.TungstenBar });
			RecipeGroup.RegisterGroup("Disarray:SilverBar", recipeGroup);

			recipeGroup = new RecipeGroup(() => "Gold or Platinum Bars", new int[] { ItemID.GoldBar, ItemID.PlatinumBar });
			RecipeGroup.RegisterGroup("Disarray:GoldBar", recipeGroup);

			recipeGroup = new RecipeGroup(() => "Demonite or Crimtane Bars", new int[] { ItemID.DemoniteBar, ItemID.CrimtaneBar });
			RecipeGroup.RegisterGroup("Disarray:DemoniteBar", recipeGroup);
		}

		public override void Unload()
		{
			DisarrayGlobalNPC.Unload();
			DisarrayGlobalPlayer.Unload();
			AutoloadedClass.Unload();
			ForgeCore.Unload();
		}

		public override void UpdateUI(GameTime gameTime)
		{
			ForgeUserInterface?.Update(gameTime);
			AlmanacUserInterface?.Update(gameTime);
			GardeningInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
			if (inventoryIndex != -1)
			{
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer("Disarray: Forge", delegate { ForgeUserInterface.Draw(Main.spriteBatch, new GameTime()); return true; }, InterfaceScaleType.UI));
				layers.Insert(inventoryIndex + 1, new LegacyGameInterfaceLayer("Disarray: Almanac", delegate { AlmanacUserInterface.Draw(Main.spriteBatch, new GameTime()); return true; }, InterfaceScaleType.UI));
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer("Disarray: Gardening", delegate { GardeningInterface.Draw(Main.spriteBatch, new GameTime()); return true; }, InterfaceScaleType.UI));
			}
		}

		private void Main_DrawMap(On.Terraria.Main.orig_DrawMap orig, Main self)
		{
			orig.Invoke(self);

			float mapScale = Main.mapFullscreen ? Main.mapFullscreenScale : ((Main.mapStyle != 1) ? Main.mapOverlayScale : Main.mapMinimapScale);
			foreach (MapEntry mapEntry in MapEntries)
			{
				Vector2 drawPosition = mapEntry.WorldPosition.ToTileCoordinates().ToVector2() * mapScale;
				if (Main.mapFullscreen)
				{
					drawPosition += (Main.mapFullscreenPos * -mapScale) + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
				}
				else
				{
					if (Main.mapStyle == 1)
					{
						Rectangle miniMap = new Rectangle(Main.miniMapX, Main.miniMapY, Main.miniMapWidth, Main.miniMapHeight);
						Vector2 positionRelativeToMap = (mapEntry.WorldPosition - Main.LocalPlayer.Center).ToTileCoordinates().ToVector2() * Main.mapMinimapScale;
						drawPosition = miniMap.Center() + positionRelativeToMap;
						Vector2 sizeAdjustedForScale = (mapEntry.SourceRectangle.HasValue ? mapEntry.SourceRectangle.Value.Size() : mapEntry.Texture.Size()) * mapEntry.Scale;
						Rectangle entrySize = new Rectangle((int)(drawPosition.X - sizeAdjustedForScale.X / 2), (int)(drawPosition.Y - sizeAdjustedForScale.Y / 2), (int)sizeAdjustedForScale.X, (int)sizeAdjustedForScale.Y);
						miniMap.Contains(ref entrySize, out bool result);
						if (!result)
						{
							continue;
						}
					}

					if (Main.mapStyle == 2)
					{
						Vector2 screenCenter = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) / 2;
						Vector2 screenCenterTile = screenCenter.ToTileCoordinates().ToVector2() * Main.mapOverlayScale;
						Vector2 overlayOffset = new Vector2(0f - screenCenterTile.X + (Main.screenWidth / 2), 0f - screenCenterTile.Y + (Main.screenHeight / 2)) + new Vector2(10) * Main.mapOverlayScale;
						drawPosition += overlayOffset;
						drawPosition -= new Vector2(10) * Main.mapOverlayScale;
					}
				}
				Main.spriteBatch.Draw(mapEntry.Texture, drawPosition, mapEntry.SourceRectangle, mapEntry.DrawColor, mapEntry.Rotation, mapEntry.Origin, mapEntry.Scale, mapEntry.SpriteEffects, 0f);
			}

			if (Main.mapStyle == 1 && !Main.mapFullscreen)
			{
				Rectangle miniMapFrame = new Rectangle(Main.miniMapX - 6, Main.miniMapY - 6, Main.miniMapWidth + 12, Main.miniMapHeight + 12);
				Main.spriteBatch.Draw(Main.miniMapFrameTexture, miniMapFrame.Location.ToVector2(), new Rectangle(0, 0, Main.miniMapFrameTexture.Width, Main.miniMapFrameTexture.Height), Color.White, 0f, default, 1f, SpriteEffects.None, 0f);
				for (int miniMapButtonCount = 0; miniMapButtonCount < 3; miniMapButtonCount++)
				{
					float num133 = miniMapFrame.Location.X + 148f + miniMapButtonCount * 26;
					float num135 = miniMapFrame.Location.Y + 234f;
					if (!(Main.mouseX > num133) || !(Main.mouseX < num133 + 22f) || !(Main.mouseY > num135) || !(Main.mouseY < num135 + 22f))
					{
						continue;
					}
					Main.spriteBatch.Draw(Main.miniMapButtonTexture[miniMapButtonCount], new Vector2(num133, num135), new Rectangle(0, 0, Main.miniMapButtonTexture[miniMapButtonCount].Width, Main.miniMapButtonTexture[miniMapButtonCount].Height), Color.White, 0f, default, 1f, SpriteEffects.None, 0f);
				}
			}

			MapEntries.Clear();
		}
	}
}