using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Disarray.Core.UI
{
	public class UIItemSlot : UIElement
	{
		public Texture2D ImageBG;
		public Item item;
		public Type ValidItemType = typeof(ModItem);
		public IEnumerable<UIItemSlot> OthersToAccountFor = new List<UIItemSlot>();

		public Player Player => Main.LocalPlayer;

		public Item HeldItem => Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;

		public event Action ItemChanged;

		public UIItemSlot(Texture2D image, Type validItemType, IEnumerable<UIItemSlot> others = null)
		{
			ImageBG = image;
			item = new Item();
			item.SetDefaults();
			ValidItemType = validItemType;
			OthersToAccountFor = others;
		}

		public virtual void ReleaseItem()
		{
			Item newestItem = item.CloneWithModdedDataFrom(item); // DO NOT REMOVE. REMOVAL CAUSES EVERYTHING TO BREAK.

			if (Main.mouseItem.IsAir)
			{
				Main.mouseItem.SetDefaults(item.type);
				Item mouseSpawnedItem = Main.mouseItem;
				mouseSpawnedItem = mouseSpawnedItem.CloneWithModdedDataFrom(item);
				mouseSpawnedItem.modItem?.SetDefaults();
				return;
			}
			else if (Main.mouseItem.type == item.type && Main.mouseItem.stack < Main.mouseItem.maxStack)
			{
				Main.mouseItem.stack++;
				return;
			}

			Item spawnedItem = Main.item[Item.NewItem(Player.getRect(), item.type)];
			spawnedItem = spawnedItem.CloneWithModdedDataFrom(item);
			spawnedItem.modItem?.SetDefaults();
		}

		public bool HoldingValidItem()
        {
			if (!HeldItem.IsAir && HeldItem.modItem != null && HeldItem.modItem.GetType().IsSubclassOf(ValidItemType))
            {
				if (OthersToAccountFor != null && OthersToAccountFor.Count() > 0)
                {
					foreach (UIItemSlot slot in OthersToAccountFor)
                    {
						if (slot.item.type == HeldItem.type)
                        {
							return false;
                        }
                    }
                }

				return true;
            }

			return false;
		}

		public void ForceChange(Item refItem)
		{
			if (!item.IsAir)
			{
				Item spawnedItem = Main.item[Item.NewItem(Player.getRect(), item.type)];
				spawnedItem = spawnedItem.CloneWithModdedDataFrom(item);
				spawnedItem.modItem?.SetDefaults();
			}

			item.SetDefaults(refItem.type);
			item = item.CloneWithModdedDataFrom(refItem);
			item.modItem?.SetDefaults();
			ItemChanged?.Invoke();
		}

		public void InvokeItemChanged() => ItemChanged?.Invoke();

		public void HandleConsuming()
        {
			if (Main.mouseItem.IsAir)
			{
				Player.ConsumeItem(HeldItem.type, true);
			}
			else
			{
				Main.mouseItem.stack--;
				if (Main.mouseItem.stack <= 0)
				{
					Main.mouseItem.SetDefaults();
				}
			}
		}

		public override void Click(UIMouseEvent evt)
		{
			if (!item.IsAir)
			{
				ReleaseItem();
				item.SetDefaults();
				ItemChanged?.Invoke();
				return;
			}

			if (HoldingValidItem())
			{
				item.SetDefaults(HeldItem.type);
				item = item.CloneWithModdedDataFrom(HeldItem);
				item.modItem?.SetDefaults();

				HandleConsuming();

				ItemChanged?.Invoke();
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
				Texture2D texture = Main.itemTexture[item.type];
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