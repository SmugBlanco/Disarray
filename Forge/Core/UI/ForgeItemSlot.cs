using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using Disarray.Core.UI;
using Disarray.Forge.Core.Items;

namespace Disarray.Forge.Core.UI
{
	public class ForgeItemSlot : UIItemSlot
	{
		public ForgeItemSlot(Texture2D image, Type validItemType, IEnumerable<ForgeItemSlot> others = null) : base(image, validItemType, others?.Select(itemSlot => itemSlot as UIItemSlot)) { }

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float ReturnBigger(int MaximumSize, int GivenWidth, int GivenHeight)
			{
				if (GivenWidth > GivenHeight)
				{
					return MaximumSize / GivenWidth;
				}
				else
				{
					return MaximumSize / GivenHeight;
				}
			}

			CalculatedStyle dimensions = GetDimensions();
			Point DrawPos = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(ImageBG, new Rectangle(DrawPos.X, DrawPos.Y, width, height), Color.White);

			if (!item.IsAir)
			{
				Texture2D texture = Main.itemTexture[item.type];
				if (item.modItem is ForgeItem forgeItem)
				{
					if (forgeItem.ForgedTemplate != null)
					{
						texture = ForgeBase.ItemTextureData.TryGetValue(forgeItem.ForgedTemplate.item.type, out Texture2D actualTexture) ? actualTexture : Main.itemTexture[forgeItem.ForgedTemplate.item.type];
					}
				}

				int DrawSize = 60;
				float Scale = ReturnBigger(DrawSize, texture.Width, texture.Height);
				Rectangle sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
				Vector2 origin = sourceRect.Size() / 2f;
				Vector2 DrawOffset = new Vector2(DrawSize / 2 - (texture.Width * Scale) / 2, DrawSize / 2 - (texture.Height * Scale) / 2);
				Rectangle DestinationRectangle = new Rectangle((int)(DrawPos.X + DrawOffset.X), (int)(DrawPos.Y + DrawOffset.Y), (int)(texture.Width * Scale), (int)(texture.Height * Scale));
				spriteBatch.Draw(texture, new Vector2(DrawPos.X + (ImageBG.Width / 2 - (sourceRect.Width / 2)), DrawPos.Y + (ImageBG.Height / 2 - (sourceRect.Height / 2))), sourceRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
			}
		}
	}
}