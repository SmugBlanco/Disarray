using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System;
using System.Collections.Generic;
using Disarray.Core.Forge.Items;
using Disarray.Content.Forge.Items.Blacksmith;
using Terraria.ID;

namespace Disarray.Core.UI
{
	public class ExpressableItemSlot : UIItemSlot
	{
		public Item expressedItem = new Item();

		public event Action ExpressedItemChanged;

		public ExpressableItemSlot(Texture2D image, Type validItemType, List<UIItemSlot> Others = null) : base(image, validItemType, Others)
		{
			expressedItem.SetDefaults();
		}

		public void ChangeExpressed(Item refItem)
        {
			expressedItem.SetDefaults(refItem.type);
			expressedItem = expressedItem.CloneWithModdedDataFrom(refItem);
			expressedItem.modItem?.SetDefaults();
			ExpressedItemChanged?.Invoke();
		}

		public override void Click(UIMouseEvent evt)
		{
			if (!item.IsAir)
			{
				ReleaseItem();
				item.SetDefaults();
				expressedItem.SetDefaults();
				InvokeItemChanged();
				return;
			}

			if (HoldingValidItem())
			{
				item.SetDefaults(heldItem.type);
				item = item.CloneWithModdedDataFrom(heldItem);
				item.modItem?.SetDefaults();

				expressedItem.SetDefaults(heldItem.type);
				expressedItem = expressedItem.CloneWithModdedDataFrom(heldItem);
				expressedItem.modItem?.SetDefaults();

				HandleConsuming();

				InvokeItemChanged();
			}
		}

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
				Item refItem = expressedItem.IsAir ? item : expressedItem;
				Texture2D texture = Main.itemTexture[refItem.type];
				if (refItem.modItem is ForgeItem forgeItem)
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