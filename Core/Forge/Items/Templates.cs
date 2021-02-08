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
using ReLogic.Utilities;

namespace Disarray.Core.Forge.Items
{
	public enum TemplateType
	{
		Mold,
		Artifact
	}

	public abstract class Templates : ForgeBase
	{
		public virtual void SafeDefaults(Item item)
		{

		}

		public sealed override void SetDefaults()
		{
			SafeDefaults(item);

			NonProductDefaults();
		}

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
			if (!Main.dedServ)
			{
				Texture2D actualItemTexture = WeaponTextureData.TryGetValue(item.type, out Texture2D WeaponTexture) ? WeaponTexture : ItemTextureData.TryGetValue(item.type, out Texture2D ItemTexture) ? ItemTexture : Main.itemTexture[item.type];
				hitbox.Width = (int)((float)hitbox.Width * ((float)actualItemTexture.Width / (float)Main.itemTexture[item.type].Width));
				hitbox.Height = (int)((float)hitbox.Height * ((float)actualItemTexture.Height / (float)Main.itemTexture[item.type].Height));
			}
		}
	}
}