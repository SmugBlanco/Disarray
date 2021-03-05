using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Disarray.Core.Gardening;
using Disarray.Core.Gardening.Tiles;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Disarray
{
	public class Disarray : Mod
	{
		public static bool Loading { get; private set; }

		internal UserInterface ForgeUserInterface;
		internal UserInterface AlmanacUserInterface;
		internal UserInterface GardeningInterface;

		public static Disarray GetMod { get; private set; }

		public Disarray()
		{
			GetMod = this;
		}

		public override void Load()
		{
			if (Code == null)
			{
				return;
			}

			Loading = true;

			GardeningInformation.Load();
			PlayerProperty.Load();
			DisarrayGlobalNPC.Load();
			NPCProperty.Load();
			ProjectileProperty.Load();
			TileData.Load();
			PlantNeeds.Load();

			foreach (Type item in Code.GetTypes())
			{
				if (!item.IsAbstract && item.GetConstructor(new Type[0]) != null)
				{
					if (item.IsSubclassOf(typeof(GardeningInformation)))
					{
						GardeningInformation.LoadType(item);
					}

					if (item.IsSubclassOf(typeof(NPCProperty)))
					{
						NPCProperty.LoadType(item);
					}

					if (item.IsSubclassOf(typeof(PlayerProperty)))
					{
						PlayerProperty.LoadType(item);
					}

					if (item.IsSubclassOf(typeof(ProjectileProperty)))
					{
						ProjectileProperty.LoadType(item);
					}

					if (item.IsSubclassOf(typeof(TileData)))
					{
						TileData.LoadType(item);
					}

					if (item.IsSubclassOf(typeof(PlantNeeds)))
					{
						PlantNeeds.LoadType(item);
					}
				}
			}

			if (!Main.dedServ)
			{
				ForgeUserInterface = new UserInterface();
				AlmanacUserInterface = new UserInterface();
				GardeningInterface = new UserInterface();
			}

			Loading = false;
		}

		public override void Unload()
		{
			ForgeBase.Unload();
			GardeningInformation.Unload();
			PlayerProperty.Unload();
			DisarrayGlobalNPC.Unload();
			NPCProperty.Unload();
			ProjectileProperty.Unload();
			FloraBase.Unload();
			TileData.Unload();
			PlantNeeds.Unload();
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
	}
}