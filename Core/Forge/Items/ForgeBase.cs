using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.ModLoader.IO;

namespace Disarray.Core.Forge.Items
{
	public abstract partial class ForgeBase : ModItem
	{
		/// <summary>
		/// Item type, Slot data
		/// </summary>
		public static IDictionary<int, int> SlotData = new Dictionary<int, int>();

		/// <summary>
		/// Item type, Texture data
		/// </summary>
		public static IDictionary<int, Texture2D> WeaponTextureData = new Dictionary<int, Texture2D>();

		/// <summary>
		/// Item type, Texture data
		/// </summary>
		public static IDictionary<int, Texture2D> ItemTextureData = new Dictionary<int, Texture2D>();

		public static void Unload()
        {
			SlotData.Clear();
			WeaponTextureData.Clear();
			ItemTextureData.Clear();
        }

		public bool AutoloadArmor(string name, Item item, EquipType equipType, string AltItemTexturePath = "")
		{
			mod.AddItem(name, item.modItem);
			string TexturePath = item.modItem.Texture;
			SlotData.Add(item.type, mod.AddEquipTexture(item.modItem, equipType, item.Name, TexturePath + "_" + equipType, TexturePath + "_Arms", TexturePath + "_FemaleBody"));
			if (AltItemTexturePath != string.Empty)
            {
				AutoloadItem(item, AltItemTexturePath);
            }
			return false;
		}

		public bool AutoloadWeapon(string name, Item item, string WeaponTexturePath = "", string AltItemTexturePath = "")
		{
			mod.AddItem(name, item.modItem);
			WeaponTextureData.Add(item.type, ModContent.GetTexture(WeaponTexturePath == string.Empty ? item.modItem.Texture + "_Weapon" : WeaponTexturePath));
			if (AltItemTexturePath != string.Empty)
			{
				AutoloadItem(item, AltItemTexturePath);
			}
			return false;
		}

		public void PreDrawWeaponAnimation(ref Texture2D texture, ref Vector2 drawPosition, ref Rectangle sourceRectangle, ref Color drawColor, ref float rotation, ref Vector2 drawOrigin, ref float scale, ref SpriteEffects spriteEffects)
        {

        }

		public bool AutoloadItem(Item item, string AltItemTexturePath = "")
        {
			ItemTextureData.Add(item.type, ModContent.GetTexture(AltItemTexturePath));
			return false;
        }

		/// <summary>
		/// Not implemented anywhere, exists for your comfort and standardization.
		/// </summary>
		public virtual void NonProductDefaults()
		{

		}

		public const string DefaultInformation = "No Available Information :(";

		/// <summary>
		/// Where worldbuilding takes place and lore is placed
		/// </summary>
		public virtual string ItemDescription() => DefaultInformation;

		/// <summary>
		/// Raw numerical statistics only
		/// </summary>
		public virtual string ItemStatistics() => DefaultInformation;

		/// <summary>
		/// Data on how to obtain said item, including chances and all
		/// </summary>
		public virtual string ObtainingDetails() => DefaultInformation;

		/// <summary>
		/// Other details.
		/// </summary>
		public virtual string MiscDetails() => DefaultInformation;

		/*private void ImplementStats(Player play)
        {
			ForgePlayer player = play.GetModPlayer<ForgePlayer>();
			player.MaxHealthPercent = MaxHealthPercent;
			player.MaxHealthFlat = MaxHealthFlat;
			player.ResistPercent = ResistPercent;
			player.ResistFlat = ResistFlat;
			player.CritRating = CritRating;
			player.BlockRating = BlockRating;
			player.PiercePercent = PiercePercent;
			player.PierceFlat = PierceFlat;
        }*/
    }
}