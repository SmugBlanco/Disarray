using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System;
using Disarray.Core.UI;
using Disarray.Forge.Core.Items;

namespace Disarray.Forge.Core.UI
{
	public class ForgeItemSlot : UIItemSlot
	{
		public ForgeItemSlot(Texture2D background, Func<Item, Item, bool> preInsert, int drawSize = -1) : base(background, preInsert, drawSize) { }

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, GetDimensions().ToRectangle(), Color.White);

			if (!Item.IsAir)
			{
				Texture2D texture = Main.itemTexture[Item.type];
				if (Item.modItem is ForgeItem forgeItem)
				{
					if (forgeItem.GetTemplate != null)
					{
						texture = ForgeCore.ItemTextureData.TryGetValue(forgeItem.GetTemplate.item.type, out Texture2D actualTexture) ? actualTexture : Main.itemTexture[forgeItem.GetTemplate.item.type];
					}
				}

				DrawItem(spriteBatch, texture, (0f, 1f));
			}
		}
	}
}