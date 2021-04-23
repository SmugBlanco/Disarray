using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Disarray.Core.UI
{
	public class UIItemSlot : UIElement
	{
		public Texture2D Background { get; protected set; }

		public Func<Item, Item, bool> PreInsert { get; protected set; }

		public Item Item { get; internal set; }

		public event Action OnItemChange;

		public int DrawSize { get; }

		public Item HeldItem => Main.mouseItem.IsAir ? Main.LocalPlayer.HeldItem : Main.mouseItem;

		public UIItemSlot(Texture2D background, Func<Item, Item, bool> preInsert, int drawSize = -1)
		{
			Background = background;
			PreInsert = preInsert;
			Item = new Item();
			Item.SetDefaults();
			DrawSize = drawSize <= 0 ? (background.Width > background.Height ? background.Height : background.Width) - 10 : drawSize;
		}

		public virtual void ReleaseItem(bool disregardMouse = false)
		{
			if (Item.IsAir)
			{
				return;
			}

			if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift) || disregardMouse)
			{
				goto SpawnInWorld;
			}

			if (Main.mouseItem.IsAir)
			{
				Main.mouseItem.SetDefaults(Item.type);
				Item mouseSpawnedItem = Main.mouseItem;
				mouseSpawnedItem = mouseSpawnedItem.CloneWithModdedDataFrom(Item);
				mouseSpawnedItem.modItem?.SetDefaults();
				return;
			}
			else if (Main.mouseItem.type == Item.type && Main.mouseItem.stack < Main.mouseItem.maxStack)
			{
				Main.mouseItem.stack++;
				return;
			}

			SpawnInWorld:

			int newItem = Item.NewItem(Main.LocalPlayer.getRect(), Item.type);

			Item spawnedItem = Main.item[newItem];
			spawnedItem = spawnedItem.CloneWithModdedDataFrom(Item);
			spawnedItem.modItem?.SetDefaults();

			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem);
				Main.item[newItem].FindOwner(newItem);
			}
		}

		public void HandleConsuming()
		{
			if (Main.mouseItem.IsAir)
			{
				Main.LocalPlayer.ConsumeItem(HeldItem.type, true);
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

		protected void InvokeItemChange() => OnItemChange?.Invoke();

		public override void Click(UIMouseEvent evt)
		{
			if (!Item.IsAir)
			{
				ReleaseItem();
				Item.SetDefaults();
				InvokeItemChange();
				return;
			}

			if (PreInsert == null || PreInsert(Item, HeldItem))
			{
				Item.SetDefaults(HeldItem.type);
				Item = Item.CloneWithModdedDataFrom(HeldItem);
				Item.modItem?.SetDefaults();

				HandleConsuming();

				InvokeItemChange();
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, GetDimensions().ToRectangle(), Color.White);

			if (!Item.IsAir)
			{
				Texture2D texture = Main.itemTexture[Item.type];
				DrawItem(spriteBatch, texture);
			}
		}

		public void DrawItem(SpriteBatch spriteBatch, Texture2D itemTexture, (float lower, float upper)? scaleClamp = null)
		{
			float scale = DrawSize / (float)(itemTexture.Width >= itemTexture.Height ? itemTexture.Width : itemTexture.Height);
			if (scaleClamp != null)
			{
				scale = Utils.Clamp(scale, scaleClamp.Value.lower, scaleClamp.Value.upper);
			}
			Vector2 origin = itemTexture.Size() / 2f;
			spriteBatch.Draw(itemTexture, GetDimensions().Center(), null, Color.White, 0f, origin, scale, SpriteEffects.None, 0);
		}
	}
}