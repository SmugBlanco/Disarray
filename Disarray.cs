using Disarray.Core.Autoload;
using Disarray.Core.Forge.Items;
using Disarray.Core.Gardening;
using Disarray.Core.Gardening.Tiles;
using Disarray.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
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

		public static Effect npcEffect;

		public override void Load()
		{
			if (Code == null)
			{
				return;
			}

			Loading = true;

			GardeningInformation.Load();
			DisarrayGlobalNPC.Load();
			AutoloadedClass.Load();

			foreach (Type item in Code.GetTypes())
			{
				if (!item.IsAbstract && item.GetConstructor(new Type[0]) != null)
				{
					if (item.IsSubclassOf(typeof(GardeningInformation)))
					{
						GardeningInformation.LoadType(item);
					}

					if (item.IsSubclassOf(typeof(AutoloadedClass)))
					{
						AutoloadedClass.LoadType(item);
					}
				}
			}

			AutoloadedClass.PostLoadType(Code);

			if (!Main.dedServ)
			{
				Ref<Effect> pestillenceRef = new Ref<Effect>(GetEffect("Effects/Pestillence"));
				Filters.Scene["Pestillence"] = new Filter(new ScreenShaderData(pestillenceRef, "Pestillence"), EffectPriority.VeryLow);
				Filters.Scene["Pestillence"].Load();

				ForgeUserInterface = new UserInterface();
				AlmanacUserInterface = new UserInterface();
				GardeningInterface = new UserInterface();
			}

			Loading = false;
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

		public override void Unload()
		{
			ForgeBase.Unload();
			GardeningInformation.Unload();
			DisarrayGlobalNPC.Unload();
			FloraBase.Unload();
			AutoloadedClass.Unload();
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