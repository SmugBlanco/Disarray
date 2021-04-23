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
		public static IDictionary<int, int> SlotData = new Dictionary<int, int>();

		/// <summary>
		/// type, texture
		/// </summary>
		public static IDictionary<int, Texture2D> WeaponTextureData = new Dictionary<int, Texture2D>();

		/// <summary>
		/// type, texture
		/// </summary>
		public static IDictionary<int, Texture2D> ItemTextureData = new Dictionary<int, Texture2D>();

		public static void Unload()
		{
			SlotData.Clear();
			WeaponTextureData.Clear();
			ItemTextureData.Clear();
		}

		public static bool AutoloadArmor(string name, Item item, EquipType equipType, string altItemTexturePath = null)
		{
			Disarray.GetMod.AddItem(name, item.modItem);
			string texturePath = item.modItem.Texture;
			SlotData.Add(item.type, Disarray.GetMod.AddEquipTexture(item.modItem, equipType, item.Name, texturePath + "_" + equipType, texturePath + "_Arms", texturePath + "_FemaleBody"));
			if (altItemTexturePath != null)
			{
				AutoloadItem(item, altItemTexturePath);
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
				AutoloadItem(item, altItemTexturePath);
			}
			return false;
		}

		public static bool AutoloadItem(Item item, string altItemTexturePath = "")
		{
			ItemTextureData.Add(item.type, ModContent.GetTexture(altItemTexturePath));
			return false;
		}
	}
}