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
	public class UIItemSlot : UIElement
	{
		public Texture2D ImageBG;
		public Item item = new Item();
		public Item expressedItem = new Item();
		public Type ValidItemType = typeof(ModItem);
		public List<UIItemSlot> OthersToAccountFor = new List<UIItemSlot>();

		private Player player => Main.LocalPlayer;

		private Item heldItem => Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;

		public event Action ItemChanged;
		public event Action ExpressedItemChanged;

		public UIItemSlot(Texture2D image, Type validItemType, List<UIItemSlot> Others = null)
		{
			ImageBG = image;
			item.SetDefaults();
			expressedItem.SetDefaults();
			ValidItemType = validItemType;
			OthersToAccountFor = Others;
		}

		public void ReleaseItem()
		{
			//Nothing appears in logs or in game chat when this breaks. Somehow one variant works perfectly normal while another just breaks?
			Item newestItem = item.CloneWithModdedDataFrom(item); // DO NOT REMOVE. REMOVAL CAUSES EVERYTHING TO BREAK.

			//Main.mouseItem = ModContent.GetModItem(ModContent.ItemType<RevolverMold>()).item.Clone();
			//Main.mouseItem.SetDefaults(); //Does nothing

			//Main.NewText("Check for sanity"); // Gets called whenever the first condition is not met

			//if (Main.mouseItem == null) Main.NewText("Mouse item is null"); // Does not get called 

			//if (Main.mouseItem.modItem == null) Main.NewText("Mouse item's moditem is null"); // Does not get called 

			//string NewString = Main.mouseItem?.ToString();
			//Main.NewText("string" + (string.IsNullOrEmpty(NewString) ? "Is NullOrEmpty" : "Perfectly Normal :)") + " | " + (string.IsNullOrWhiteSpace(NewString) ? "Is NullOrWhiteSpace" : "Perfectly Normal :)")); // Nothing gets printed when the first condition is met

			if (Main.mouseItem.IsAir) // If this condition is satisfied, nothing happens
			{
				//Main.NewText("Testing");
				Main.mouseItem.SetDefaults(item.type);
				Item mouseSpawnedItem = Main.mouseItem;
				mouseSpawnedItem = mouseSpawnedItem.CloneWithModdedDataFrom(item);
				mouseSpawnedItem.modItem?.SetDefaults();
				return;
			}
			else if (Main.mouseItem.type == item.type && Main.mouseItem.stack < Main.mouseItem.maxStack && !(Main.mouseItem.modItem is ForgeItem)) // Works as intended
			{
				//Main.NewText("Test 2");
				Main.mouseItem.stack++;
				return;
			}

			//Main.NewText("Test 3"); //Works as normal 
			Item spawnedItem = Main.item[Item.NewItem(player.getRect(), item.type)];
			spawnedItem = spawnedItem.CloneWithModdedDataFrom(item);
			spawnedItem.modItem?.SetDefaults();
		}

		public bool HoldingValidItem()
        {
			if (!heldItem.IsAir && heldItem.modItem != null && heldItem.modItem.GetType().IsSubclassOf(ValidItemType))
            {
				if (OthersToAccountFor != null && OthersToAccountFor.Count > 0)
                {
					foreach (UIItemSlot slot in OthersToAccountFor)
                    {
						if (slot.item.type == heldItem.type)
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
				Item spawnedItem = Main.item[Item.NewItem(player.getRect(), item.type)];
				spawnedItem = spawnedItem.CloneWithModdedDataFrom(item);
				spawnedItem.modItem?.SetDefaults();
			}

			item.SetDefaults(refItem.type);
			item = item.CloneWithModdedDataFrom(refItem);
			item.modItem?.SetDefaults();
			ItemChanged?.Invoke();
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
				ItemChanged?.Invoke();
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

				if (Main.mouseItem.IsAir)
				{
					player.ConsumeItem(heldItem.type, true);
				}
				else
                {
					Main.mouseItem.stack--;
					if (Main.mouseItem.stack <= 0)
					{
						Main.mouseItem.SetDefaults();
					}
				}

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