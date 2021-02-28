using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Disarray.Core.Gardening;
using Disarray.Core.Gardening.Tiles;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Disarray
{
	public class Disarray : Mod
	{
		internal UserInterface ForgeUserInterface;
		internal UserInterface AlmanacUserInterface;

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

			GardeningInformation.Autoload(Code);
			PlayerProperty.Load(Code);
			DisarrayGlobalNPC.Load();
			NPCProperty.Load(Code);
			ProjectileProperty.Load(Code);

			if (!Main.dedServ)
			{
				ForgeUserInterface = new UserInterface();
				AlmanacUserInterface = new UserInterface();
			}
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
		}

		public override void UpdateUI(GameTime gameTime)
		{
			ForgeUserInterface?.Update(gameTime);
			AlmanacUserInterface?.Update(gameTime);
		}

		public override void PostUpdateEverything()
		{
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
			if (inventoryIndex != -1)
			{
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer("Disarray: Forge", delegate { ForgeUserInterface.Draw(Main.spriteBatch, new GameTime()); return true; }, InterfaceScaleType.UI));
				layers.Insert(inventoryIndex + 1, new LegacyGameInterfaceLayer("Disarray: Almanac", delegate { AlmanacUserInterface.Draw(Main.spriteBatch, new GameTime()); return true; }, InterfaceScaleType.UI));
			}
		}
	}
}