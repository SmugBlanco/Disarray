using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System.Collections.Generic;
using Disarray.Core.UI;
using System.Linq;
using Disarray.Forge.Core.Items;

namespace Disarray.Almanac.Core.UI
{
	public partial class AlmanacUI : UIState
	{
		public UIImage SecondPage;
		public string ItemName = string.Empty;
		public ExpressableItemSlot ItemSlot;
		public int CurrentSlot;
		public IList<ForgeBase> SlotItemData = new List<ForgeBase>();
		public UIImageButton SlotCycleLeft;
		public UIImageButton SlotCycleRight;

		public UIDisplayTextbox DescriptionTextbox;
		public FixedUIScrollbar DescriptionScrollbar;

		public UIDisplayTextbox StatisticTextbox;
		public FixedUIScrollbar StatisticScrollbar;

		public UIDisplayTextbox ObtainingTextbox;
		public FixedUIScrollbar ObtainingScrollbar;

		public UIDisplayTextbox MiscellaneousTextbox;
		public FixedUIScrollbar MiscellaneousScrollbar;

		public void InitializeSecondPage()
		{
			Texture2D bgTexture = ModContent.GetTexture(AssetDirectory + "Almanac_Background");
			SecondPage = new UIImage(bgTexture);
			SecondPage.Left.Set(bgTexture.Width, 0f);
			SecondPage.Top.Set(0, 0f);
			SecondPage.Width.Set(bgTexture.Width, 0);
			SecondPage.Height.Set(bgTexture.Height, 0);

			Texture2D itemSlotTexture = ModContent.GetTexture(AssetDirectory + "Almanac_ItemSlot");
			ItemSlot = new ExpressableItemSlot(itemSlotTexture, typeof(ForgeBase), null);
			ItemSlot.Left.Set(318, 0f);
			ItemSlot.Top.Set(50, 0f);
			ItemSlot.Width.Set(itemSlotTexture.Width, 0);
			ItemSlot.Height.Set(itemSlotTexture.Height, 0);
			ItemSlot.ItemChanged += ItemSlot_ItemChanged;
			ItemSlot.ExpressedItemChanged += ItemSlot_ExpressedItemChanged;
			SecondPage.Append(ItemSlot);

			Texture2D cycleLeft = ModContent.GetTexture(AssetDirectory + "ArrowLeft");
			SlotCycleLeft = new UIImageButton(cycleLeft);
			SlotCycleLeft.Left.Set(14, 0f);
			SlotCycleLeft.Top.Set(21, 0f);
			SlotCycleLeft.Width.Set(cycleLeft.Width, 0f);
			SlotCycleLeft.Height.Set(cycleLeft.Height, 0f);
			SlotCycleLeft.OnClick += SlotCycleLeft_OnClick;
			SecondPage.Append(SlotCycleLeft);

			Texture2D cycleRight = ModContent.GetTexture(AssetDirectory + "ArrowRight");
			SlotCycleRight = new UIImageButton(cycleRight);
			SlotCycleRight.Left.Set(bgTexture.Width - 14 - cycleRight.Width, 0f);
			SlotCycleRight.Top.Set(21, 0f);
			SlotCycleRight.Width.Set(cycleRight.Width, 0f);
			SlotCycleRight.Height.Set(cycleRight.Height, 0f);
			SlotCycleRight.OnClick += SlotCycleRight_OnClick;
			SecondPage.Append(SlotCycleRight);

			Color BackgroundColour = new Color(142, 121, 114);
			Color BorderColour = new Color(96, 72, 60);

			DescriptionTextbox = new UIDisplayTextbox(string.Empty);
			DescriptionTextbox.Left.Set(14, 0f);
			DescriptionTextbox.Top.Set(50, 0f);
			DescriptionTextbox.Width.Set(296, 0);
			DescriptionTextbox.Height.Set(100, 0);
			SecondPage.Append(DescriptionTextbox);
			DescriptionScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
			DescriptionScrollbar.Left.Set(4, 0f);
			DescriptionScrollbar.Height.Set(0, 1f);
			DescriptionScrollbar.HAlign = 1f;
			DescriptionTextbox.Append(DescriptionScrollbar);
			DescriptionTextbox.SetScrollbar(DescriptionScrollbar);
			DescriptionTextbox.BackgroundColor = BackgroundColour;
			DescriptionTextbox.BorderColor = BorderColour;

			StatisticTextbox = new UIDisplayTextbox(string.Empty);
			StatisticTextbox.Left.Set(14, 0f);
			StatisticTextbox.Top.Set(158, 0f);
			StatisticTextbox.Width.Set(SecondPage.Width.Pixels - 28, 0);
			StatisticTextbox.Height.Set(170, 0);
			SecondPage.Append(StatisticTextbox);
			StatisticScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
			StatisticScrollbar.Left.Set(4, 0f);
			StatisticScrollbar.Height.Set(0, 1f);
			StatisticScrollbar.HAlign = 1f;
			StatisticTextbox.Append(StatisticScrollbar);
			StatisticTextbox.SetScrollbar(StatisticScrollbar);
			StatisticTextbox.BackgroundColor = BackgroundColour;
			StatisticTextbox.BorderColor = BorderColour;

			ObtainingTextbox = new UIDisplayTextbox(string.Empty);
			ObtainingTextbox.Left.Set(14, 0f);
			ObtainingTextbox.Top.Set(336, 0f);
			ObtainingTextbox.Width.Set(SecondPage.Width.Pixels / 2 - 18, 0);
			ObtainingTextbox.Height.Set(226, 0);
			SecondPage.Append(ObtainingTextbox);
			ObtainingScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
			ObtainingScrollbar.Left.Set(4, 0f);
			ObtainingScrollbar.Height.Set(0, 1f);
			ObtainingScrollbar.HAlign = 1f;
			ObtainingTextbox.Append(ObtainingScrollbar);
			ObtainingTextbox.SetScrollbar(ObtainingScrollbar);
			ObtainingTextbox.BackgroundColor = BackgroundColour;
			ObtainingTextbox.BorderColor = BorderColour;


			MiscellaneousTextbox = new UIDisplayTextbox(string.Empty);
			MiscellaneousTextbox.Left.Set(SecondPage.Width.Pixels / 2 + 4, 0f);
			MiscellaneousTextbox.Top.Set(336, 0f);
			MiscellaneousTextbox.Width.Set(SecondPage.Width.Pixels / 2 - 18, 0);
			MiscellaneousTextbox.Height.Set(226, 0);
			SecondPage.Append(MiscellaneousTextbox);
			MiscellaneousScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
			MiscellaneousScrollbar.Left.Set(4, 0f);
			MiscellaneousScrollbar.Height.Set(0, 1f);
			MiscellaneousScrollbar.HAlign = 1f;
			MiscellaneousTextbox.Append(MiscellaneousScrollbar);
			MiscellaneousTextbox.SetScrollbar(MiscellaneousScrollbar);
			MiscellaneousTextbox.BackgroundColor = BackgroundColour;
			MiscellaneousTextbox.BorderColor = BorderColour;
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

			if (ItemSlot.item.modItem != null && ItemSlot.item.modItem is IAlmanacable almanacable)
			{
				ItemName = ItemSlot.item.Name;
				DescriptionTextbox.CurrentText = "Description:\n" + almanacable.GeneralDescription;
				StatisticTextbox.CurrentText = "Statistics:\n" + almanacable.ItemStatistics;
				ObtainingTextbox.CurrentText = "Obtaining:\n" + almanacable.ObtainingGuide;
				MiscellaneousTextbox.CurrentText = "Miscellaneous Information:\n" + almanacable.Miscellaneous;
				SlotItemData.Clear();

				if (almanacable is ForgeItem forgeItem)
				{
					ItemSlot.item.modItem?.SetDefaults();
					SlotItemData = forgeItem.UniqueBases.ToList();
					SlotItemData.Insert(0, ItemSlot.item.modItem as ForgeBase);
				}
			}
		}

		private void ItemSlot_ExpressedItemChanged()
		{
			if (ItemSlot.expressedItem.modItem != null && ItemSlot.expressedItem.modItem is IAlmanacable almanacable)
			{
				ItemName = ItemSlot.expressedItem.Name + (almanacable is ForgeItem ? string.Empty : " - x" + (from bases in (ItemSlot.item.modItem as ForgeItem).AllBases where bases.Name.Equals(ItemSlot.expressedItem.Name) select bases).Count());
				DescriptionTextbox.CurrentText = "Description:\n" + almanacable.GeneralDescription;
				StatisticTextbox.CurrentText = "Statistics:\n" + almanacable.ItemStatistics;
				ObtainingTextbox.CurrentText = "Obtaining:\n" + almanacable.ObtainingGuide;
				MiscellaneousTextbox.CurrentText = "Miscellaneous Information:\n" + almanacable.Miscellaneous;
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
	}
}