using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System.Linq;
using Disarray.Core.Forge.Items;
using Terraria.UI.Chat;

namespace Disarray.Core.Almanac.UI
{
	public partial class AlmanacUI : UIState
	{
		public const string AssetDirectory = "Disarray/Core/Almanac/UI/Textures/";

		public AlmanacUI()
		{
		}

		public UIElement MasterBackground; // Including this fixes many issues with the UI

		//----------------------------------------------------------------------------------------------

        public override void OnInitialize()
		{
			MasterBackground = new UIElement();
			MasterBackground.Left.Set(Main.screenWidth / 2 - 432, 0);
			MasterBackground.Top.Set(Main.screenHeight / 2 - 576 * 0.5f, 0f);
			MasterBackground.Width.Set(432 * 2, 0);
			MasterBackground.Height.Set(576, 0);
			MasterBackground.OnMouseDown += new MouseEvent(DragStart);
			MasterBackground.OnMouseUp += new MouseEvent(DragEnd);
			InitializeFirstPage();
			MasterBackground.Append(FirstPage);
			InitializeSecondPage();
			MasterBackground.Append(SecondPage);
			Append(MasterBackground);
		}

		private void ItemSlot_ItemChanged()
		{
			if (ItemSlot.item.IsAir || ItemSlot.item.modItem == null)
			{
				ItemName = string.Empty;
				DescriptionTextbox.CurrentText = "Description:\nInsert an item into the Item Slot to view it's description!";
				StatisticTextbox.CurrentText = "Statistics:\nInsert an item into the Item Slot to view it's statistics!";
				ObtainingTextbox.CurrentText = "Obtaining:\nInsert an item into the Item Slot to view it's obtained!";
				MiscellaneousTextbox.CurrentText = "Miscellaneous Information:\nInsert an item into the Item Slot to view it's miscellaneous information!";
				SlotItemData.Clear();
			}

			if (ItemSlot.item.modItem != null && ItemSlot.item.modItem is ForgeBase forgeBase)
			{
				ItemName = ItemSlot.item.Name;
				DescriptionTextbox.CurrentText = "Description:\n" + forgeBase.ItemDescription();
				StatisticTextbox.CurrentText = "Statistics:\n" + forgeBase.ItemStatistics();
				ObtainingTextbox.CurrentText = "Obtaining:\n" + forgeBase.ObtainingDetails();
				MiscellaneousTextbox.CurrentText = "Miscellaneous Information:\n" + forgeBase.MiscDetails();
				SlotItemData.Clear();

				if (forgeBase is ForgeItem forgeItem)
                {
					ItemSlot.item.modItem?.SetDefaults();
					SlotItemData = forgeItem.UniqueBases.ToList();
					SlotItemData.Insert(0, ItemSlot.item.modItem as ForgeBase);
				}
			}
		}

		private void ItemSlot_ExpressedItemChanged()
		{
			if (ItemSlot.expressedItem.modItem != null && ItemSlot.expressedItem.modItem is ForgeBase forgeBase)
			{
				ItemName = forgeBase.item.Name + (forgeBase is ForgeItem ? string.Empty : " - x" + (from bases in (ItemSlot.item.modItem as ForgeItem).AllBases where bases.Name == forgeBase.Name select bases).Count());
				DescriptionTextbox.CurrentText = "Description:\n" + forgeBase.ItemDescription();
				StatisticTextbox.CurrentText = "Statistics:\n" + forgeBase.ItemStatistics();
				ObtainingTextbox.CurrentText = "Obtaining:\n" + forgeBase.ObtainingDetails();
				MiscellaneousTextbox.CurrentText = "Miscellaneous Information:\n" + forgeBase.MiscDetails();
			}
		}

		private void SlotCycleLeft_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (SlotItemData.Count == 0)
            {
				return;
            }

			CurrentSlot = (CurrentSlot - 1) < 0 ? SlotItemData.Count - 1 : (CurrentSlot - 1) % SlotItemData.Count;
			ItemSlot.ChangeExpressed(SlotItemData[CurrentSlot].item);
		}

		private void SlotCycleRight_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (SlotItemData.Count == 0)
			{
				return;
			}

			CurrentSlot = (CurrentSlot + 1) % SlotItemData.Count;
			ItemSlot.ChangeExpressed(SlotItemData[CurrentSlot].item);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			CalculatedStyle space = GetDimensions();

			Vector2 ItemNameDrawPosition = space.Position() + new Vector2(MasterBackground.Left.Pixels, MasterBackground.Top.Pixels) + new Vector2(SecondPage.Left.Pixels, SecondPage.Top.Pixels) + new Vector2(SecondPage.Width.Pixels / 2, 28);
			Vector2 ItemNameStringSize = Main.fontDeathText.MeasureString(ItemName);
			float ItemNameScale = 0.75f;
			Vector2 ItemNameOrigin = new Vector2(ItemNameStringSize.X, ItemNameStringSize.Y * ItemNameScale);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontDeathText, ItemName, ItemNameDrawPosition, Color.White, 0f, ItemNameOrigin / 2, new Vector2(ItemNameScale, ItemNameScale), -1, 2);
		}

        public override void OnDeactivate()
		{
			SecondPage = null;
			ItemSlot.ReleaseItem();
			ItemSlot = null;
		}

		public override void Update(GameTime gameTime)
		{
			if (!Main.playerInventory)
			{
				ModContent.GetInstance<Disarray>().AlmanacUserInterface?.SetState(null);
				return;
			}

			UpdateFirstPage();
		}

		Vector2 offset;
		public bool dragging = false;
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!Lock && !LockedMovement.ContainsPoint(evt.MousePosition))
			{
				offset = new Vector2(evt.MousePosition.X - MasterBackground.Left.Pixels, evt.MousePosition.Y - MasterBackground.Top.Pixels);
				dragging = true;
			}
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!Lock && !LockedMovement.ContainsPoint(evt.MousePosition))
			{
				Vector2 end = evt.MousePosition;
				dragging = false;
				MasterBackground.Left.Set(end.X - offset.X, 0f);
				MasterBackground.Top.Set(end.Y - offset.Y, 0f);
				Recalculate();
			}
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (MasterBackground.ContainsPoint(MousePosition))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			if (dragging)
			{
				MasterBackground.Left.Set(MousePosition.X - offset.X, 0f);
				MasterBackground.Top.Set(MousePosition.Y - offset.Y, 0f);
				Recalculate();
			}
		}
	}
}