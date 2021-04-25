using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using System;
using Disarray.Core.UI;
using Disarray.Forge.Core.Items;

namespace Disarray.Almanac.Core.UI
{
	public class ExpressableItemSlot : UIItemSlot
	{
		public Item ExpressedItem { get; private set; }

		public event Action OnExpressedItemChange;

		public ExpressableItemSlot(Texture2D image, Func<Item, Item, bool> preInsert, int drawSize = -1) : base(image, preInsert, drawSize)
		{
			ExpressedItem = new Item();
			ExpressedItem.SetDefaults();
		}

		public void ChangeExpressed(Item refItem)
        {
			ExpressedItem.SetDefaults(refItem.type);
			ExpressedItem = ExpressedItem.CloneWithModdedDataFrom(refItem);
			ExpressedItem.modItem?.SetDefaults();
			OnExpressedItemChange?.Invoke();
		}

		public override void Click(UIMouseEvent evt)
		{
			if (!Item.IsAir)
			{
				ReleaseItem();
				Item.SetDefaults();
				ExpressedItem.SetDefaults();
				InvokeItemChange();
				return;
			}

			if (PreInsert == null || PreInsert(Item, HeldItem))
			{
				Item.SetDefaults(HeldItem.type);
				Item = Item.CloneWithModdedDataFrom(HeldItem);
				Item.modItem?.SetDefaults();

				ExpressedItem.SetDefaults(HeldItem.type);
				ExpressedItem = ExpressedItem.CloneWithModdedDataFrom(HeldItem);
				ExpressedItem.modItem?.SetDefaults();

				HandleConsuming();

				InvokeItemChange();
			}
		}

        protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, GetDimensions().ToRectangle(), Color.White);

			if (!Item.IsAir)
			{
				Item refItem = ExpressedItem.IsAir ? Item : ExpressedItem;
				Texture2D texture = Main.itemTexture[refItem.type];
				if (refItem.modItem is ForgeItem forgeItem)
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