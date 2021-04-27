using Terraria;
using Terraria.ModLoader;
using Disarray.Almanac.Core;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Disarray.Forge.Core.Items
{
	public abstract partial class ForgeCore : ModItem, IAlmanacable
	{
		/// <summary>
		/// type, texture
		/// </summary>
		public static IDictionary<int, int> SlotData;

		/// <summary>
		/// type, texture
		/// </summary>
		public static IDictionary<int, Texture2D> WeaponTextureData;

		/// <summary>
		/// type, texture
		/// </summary>
		public static IDictionary<int, Texture2D> ItemTextureData;

		public static void Load()
		{
			SlotData = new Dictionary<int, int>();
			WeaponTextureData = new Dictionary<int, Texture2D>();
			ItemTextureData = new Dictionary<int, Texture2D>();
		}

		public static void Unload()
		{
			SlotData?.Clear();
			WeaponTextureData?.Clear();
			ItemTextureData?.Clear();
		}

		public static bool AutoloadArmor(string name, Item item, EquipType equipType, string altItemTexturePath = null)
		{
			Disarray.GetMod.AddItem(name, item.modItem);
			string texturePath = item.modItem.Texture;
			SlotData.Add(item.type, Disarray.GetMod.AddEquipTexture(item.modItem, equipType, item.Name, texturePath + "_" + equipType, texturePath + "_Arms", texturePath + "_FemaleBody"));
			if (altItemTexturePath != null)
			{
				AutoloadItem(name, item, altItemTexturePath, false);
			}
			return false;
		}

		public static bool AutoloadWeapon(string name, Item item, string weaponTexturePath = null, string altItemTexturePath = null)
		{
			Disarray.GetMod.AddItem(name, item.modItem);
			string texturePath = weaponTexturePath is null ? item.modItem.Texture + "_Weapon" : weaponTexturePath;
			Disarray.GetMod.Logger.Info("Autoloading weapon texture for: " + item.modItem.Name + ". Given path: " + texturePath);
			WeaponTextureData.Add(item.type, ModContent.GetTexture(texturePath));
			if (altItemTexturePath != null)
			{
				AutoloadItem(name, item, altItemTexturePath, false);
			}
			else
			{
				AutoloadItem(name, item, texturePath, false);
			}
			return false;
		}

		public static bool AutoloadItem(string name, Item item, string altItemTexturePath = null, bool addItem = true)
		{
			if (addItem)
			{
				Disarray.GetMod.AddItem(name, item.modItem);
			}

			string texture = altItemTexturePath is null ? item.modItem.Texture : altItemTexturePath;
			Disarray.GetMod.Logger.Info("Autoloading item texture for: " + item.modItem.Name + " | " + item.type + ". Given path: " + texture);
			ItemTextureData.Add(item.type, ModContent.GetTexture(texture));
			return false;
		}
	}
}