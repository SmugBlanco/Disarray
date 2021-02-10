using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using Terraria.GameInput;
using System.Collections.Generic;
using Disarray.Core.UI;
using Terraria.GameContent.Events;
using System.Reflection;
using Disarray.Core.Forge.Items;
using ReLogic.Graphics;
using Disarray.Core.Data;
using Disarray.Core.Extensions;
using static Disarray.Core.Data.Moonphase;
using static Disarray.Core.Data.SeasonData;
using Terraria.UI.Chat;

namespace Disarray.Core.Almanac.UI
{
	public class AlmanacUI : UIState
	{
		public const string AssetDirectory = "Disarray/Core/Almanac/UI/Textures/Almanac";

		public AlmanacUI()
		{
		}

		public UIElement MasterBackground;

		public UIImage FirstPage;
		public UIText Title;

		public UIImageButton LockedMovement;
		public bool Lock = true;

		public string DateTime => System.DateTime.Today.DayOfWeek + ", " + System.DateTime.Now.ToString();
		public string DaysIntoNewYear => System.DateTime.Today.Subtract(new DateTime(System.DateTime.Today.Year, 1, 1)).Days + " days into the New Year";
		public Seasons CurrentSeason => GetSeasonOnDate(System.DateTime.Today);
		public PhasesOfMoon MoonPhaseToday => System.DateTime.Today.GetMoonphase();
		public PhasesOfMoon MoonPhaseTommorow => System.DateTime.Today.AddDays(1).GetMoonphase();
		public PhasesOfMoon MoonPhaseOvermorrow => System.DateTime.Today.AddDays(2).GetMoonphase();

		public MoonphaseDisplay[] moonphaseDisplays = new MoonphaseDisplay[3];

		public CropDisplay cropDisplay;
		public UIDisplayTextbox cropDisplayTextbox;
		public FixedUIScrollbar cropDisplayTextboxScrollbar;
		public UIImageButton cropDisplayButtonLeft;
		public UIImageButton cropDisplayButtonRight;

		public UIDisplayTextbox DailyTriviaTextbox;
		public FixedUIScrollbar DailyTriviaTextboxScrollbar;

		//----------------------------------------------------------------------------------------------

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

		public void InitializeFirstPage()
        {
			Texture2D bgTexture = ModContent.GetTexture(AssetDirectory + "_Background");
			FirstPage = new UIImage(bgTexture);
			FirstPage.Left.Set(0, 0f);
			FirstPage.Top.Set(0, 0f);
			FirstPage.Width.Set(bgTexture.Width, 0);
			FirstPage.Height.Set(bgTexture.Height, 0);

			Title = new UIText("The Almanac", 0.8f, true);
			Title.Left.Set(120, 0f);
			Title.Top.Set(18, 0f);
			FirstPage.Append(Title);

			Texture2D lockTexture = ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/Locked");
			LockedMovement = new UIImageButton(lockTexture);
			LockedMovement.Left.Set(bgTexture.Width - lockTexture.Width - 18, 0f);
			LockedMovement.Top.Set(14, 0f);
			LockedMovement.Width.Set(lockTexture.Width, 0);
			LockedMovement.Height.Set(lockTexture.Height, 0);
            LockedMovement.OnClick += LockedMovement_OnClick;
			FirstPage.Append(LockedMovement);

			Texture2D MoonDisplayBG = ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/MoonDisplay_Background");
			for (int Indexer = 0; Indexer < moonphaseDisplays.Length; Indexer++)
            {
				PhasesOfMoon phase = PhasesOfMoon.NewMoon;
				string headerText = "Today";
				int Left = 14;
				switch (Indexer)
                {
					default:
					case 0:
						phase = MoonPhaseToday;
						headerText = "Today";
						Left = 14;
						break;

					case 1:
						phase = MoonPhaseTommorow;
						headerText = "Tommorow";
						Left = 151;
						break;

					case 2:
						phase = MoonPhaseOvermorrow;
						headerText = "Overmorrow";
						Left = 288;
						break;
                }
				
				moonphaseDisplays[Indexer] = new MoonphaseDisplay(phase, headerText);
				moonphaseDisplays[Indexer].Left.Set(Left, 0);
				moonphaseDisplays[Indexer].Top.Set(147, 0);
				moonphaseDisplays[Indexer].Width.Set(MoonDisplayBG.Width, 0);
				moonphaseDisplays[Indexer].Height.Set(MoonDisplayBG.Height, 0);
				FirstPage.Append(moonphaseDisplays[Indexer]);
			}

			Texture2D CropDisplayBG = ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/CropDisplay_Background");
			cropDisplay = new CropDisplay();
			cropDisplay.Left.Set(38, 0);
			cropDisplay.Top.Set(323, 0);
			cropDisplay.Width.Set(CropDisplayBG.Width, 0);
			cropDisplay.Height.Set(CropDisplayBG.Height, 0);
            cropDisplay.CropChanged += CropDisplay_CropChanged;
			FirstPage.Append(cropDisplay);

			Color BackgroundColour = new Color(142, 121, 114);
			Color BorderColour = new Color(96, 72, 60);

			Crop crop = Crop.GetCrop(cropDisplay.currentCrop);
			cropDisplayTextbox = new UIDisplayTextbox("Description: " + crop.Description + "\n \nOrigin: " + crop.Origin + "\n \nPrice Per LBs: " + crop.PricePerPound + "\n \nPlanting Season: " + crop.PlantingMonths + "\n \nHarvest Season: " + crop.HarvestMonths);
			cropDisplayTextbox.Left.Set(38 + CropDisplayBG.Width + 4, 0f);
			cropDisplayTextbox.Top.Set(323, 0f);
			cropDisplayTextbox.Width.Set(254, 0);
			cropDisplayTextbox.Height.Set(CropDisplayBG.Height, 0);
			FirstPage.Append(cropDisplayTextbox);
			cropDisplayTextboxScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
			cropDisplayTextboxScrollbar.Left.Set(4, 0f);
			cropDisplayTextboxScrollbar.Height.Set(0, 1f);
			cropDisplayTextboxScrollbar.HAlign = 1f;
			cropDisplayTextbox.Append(cropDisplayTextboxScrollbar);
			cropDisplayTextbox.SetScrollbar(cropDisplayTextboxScrollbar);
			cropDisplayTextbox.BackgroundColor = BackgroundColour;
			cropDisplayTextbox.BorderColor = BorderColour;

			Texture2D CropDisplayButtonLeft = ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/CropDisplay_ButtonLeft");
			cropDisplayButtonLeft = new UIImageButton(CropDisplayButtonLeft);
			cropDisplayButtonLeft.Left.Set(14, 0f);
			cropDisplayButtonLeft.Top.Set(365, 0f);
			cropDisplayButtonLeft.Width.Set(CropDisplayButtonLeft.Width, 0);
			cropDisplayButtonLeft.Height.Set(CropDisplayButtonLeft.Height, 0);
			cropDisplayButtonLeft.OnClick += CropDisplayButtonLeft_OnClick;
			FirstPage.Append(cropDisplayButtonLeft);

			Texture2D CropDisplayButtonRight = ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/CropDisplay_ButtonRight");
			cropDisplayButtonRight = new UIImageButton(CropDisplayButtonRight);
			cropDisplayButtonRight.Left.Set(FirstPage.Width.Pixels - 14 - CropDisplayButtonLeft.Width, 0f);
			cropDisplayButtonRight.Top.Set(365, 0f);
			cropDisplayButtonRight.Width.Set(CropDisplayButtonLeft.Width, 0);
			cropDisplayButtonRight.Height.Set(CropDisplayButtonLeft.Height, 0);
            cropDisplayButtonRight.OnClick += CropDisplayButtonRight_OnClick;
			FirstPage.Append(cropDisplayButtonRight);

			DailyTriviaTextbox = new UIDisplayTextbox("Trivia for today:\nhaha hi");
			DailyTriviaTextbox.Left.Set(14, 0f);
			DailyTriviaTextbox.Top.Set(442, 0f);
			DailyTriviaTextbox.Width.Set(404, 0);
			DailyTriviaTextbox.Height.Set(120, 0);
			FirstPage.Append(DailyTriviaTextbox);
			DailyTriviaTextboxScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
			DailyTriviaTextboxScrollbar.Left.Set(4, 0f);
			DailyTriviaTextboxScrollbar.Height.Set(0, 1f);
			DailyTriviaTextboxScrollbar.HAlign = 1f;
			DailyTriviaTextbox.Append(DailyTriviaTextboxScrollbar);
			DailyTriviaTextbox.SetScrollbar(DailyTriviaTextboxScrollbar);
			DailyTriviaTextbox.BackgroundColor = BackgroundColour;
			DailyTriviaTextbox.BorderColor = BorderColour;
		}

        public void InitializeSecondPage()
        {
			Texture2D bgTexture = ModContent.GetTexture(AssetDirectory + "_Background");
			SecondPage = new UIImage(bgTexture);
			SecondPage.Left.Set(bgTexture.Width, 0f);
			SecondPage.Top.Set(0, 0f);
			SecondPage.Width.Set(bgTexture.Width, 0);
			SecondPage.Height.Set(bgTexture.Height, 0);

			Texture2D itemSlotTexture = ModContent.GetTexture(AssetDirectory + "_ItemSlot");
			ItemSlot = new ExpressableItemSlot(itemSlotTexture, typeof(ForgeBase), null);
			ItemSlot.Left.Set(318, 0f);
			ItemSlot.Top.Set(50, 0f);
			ItemSlot.Width.Set(itemSlotTexture.Width, 0);
			ItemSlot.Height.Set(itemSlotTexture.Height, 0);
            ItemSlot.ItemChanged += ItemSlot_ItemChanged;
            ItemSlot.ExpressedItemChanged += ItemSlot_ExpressedItemChanged;
			SecondPage.Append(ItemSlot);

			Texture2D cycleLeft = ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/ArrowLeft");
			SlotCycleLeft = new UIImageButton(cycleLeft);
			SlotCycleLeft.Left.Set(14, 0f);
			SlotCycleLeft.Top.Set(21, 0f);
			SlotCycleLeft.Width.Set(cycleLeft.Width, 0f);
			SlotCycleLeft.Height.Set(cycleLeft.Height, 0f);
            SlotCycleLeft.OnClick += SlotCycleLeft_OnClick;
			SecondPage.Append(SlotCycleLeft);

			Texture2D cycleRight = ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/ArrowRight");
			SlotCycleRight = new UIImageButton(cycleRight);
			SlotCycleRight.Left.Set(bgTexture.Width - 14 - cycleRight.Width, 0f);
			SlotCycleRight.Top.Set(21, 0f);
			SlotCycleRight.Width.Set(cycleRight.Width, 0f);
			SlotCycleRight.Height.Set(cycleRight.Height, 0f);
            SlotCycleRight.OnClick += SlotCycleRight_OnClick;
			SecondPage.Append(SlotCycleRight);

			Color BackgroundColour = new Color(142, 121, 114);
			Color BorderColour = new Color(96, 72, 60);

			DescriptionTextbox = new UIDisplayTextbox("Description:\nInsert an item into the Item Slot to view it's description!");
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

			StatisticTextbox = new UIDisplayTextbox("Statistics:\nInsert an item into the Item Slot to view it's statistics!");
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

			ObtainingTextbox = new UIDisplayTextbox("Obtaining:\nInsert an item into the Item Slot to view it's obtained!");
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


			MiscellaneousTextbox = new UIDisplayTextbox("Miscellaneous Information:\nInsert an item into the Item Slot to view it's miscellaneous information!");
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

		private void LockedMovement_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Lock = !Lock;
			string LockTexture = Lock ? "Locked" : "Unlocked";
			LockedMovement.SetImage(ModContent.GetTexture("Disarray/Core/Almanac/UI/Textures/" + LockTexture));
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

		private void CropDisplay_CropChanged()
		{
			Crop crop = Crop.GetCrop(cropDisplay.currentCrop);
			cropDisplayTextbox.CurrentText = "Description: " + crop.Description + "\n \nOrigin: " + crop.Origin + "\n \nPrice Per LBs: " + crop.PricePerPound + "\n \nPlanting Season: " + crop.PlantingMonths + "\n \nHarvest Season: " + crop.HarvestMonths;
		}

		private void CropDisplayButtonLeft_OnClick(UIMouseEvent evt, UIElement listeningElement) => cropDisplay.ForceChange(((CropDisplay.HandleCropDisplay.CurrentCrop - 1) < 0 ? Crop.LoadedCrops.Count - 1 : CropDisplay.HandleCropDisplay.CurrentCrop - 1) % Crop.LoadedCrops.Count);

		private void CropDisplayButtonRight_OnClick(UIMouseEvent evt, UIElement listeningElement) => cropDisplay.ForceChange((CropDisplay.HandleCropDisplay.CurrentCrop + 1) % Crop.LoadedCrops.Count);

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			CalculatedStyle space = GetDimensions();

			Vector2 ItemNameDrawPosition = space.Position() + new Vector2(MasterBackground.Left.Pixels, MasterBackground.Top.Pixels) + new Vector2(SecondPage.Left.Pixels, SecondPage.Top.Pixels) + new Vector2(SecondPage.Width.Pixels / 2, 28);
			Vector2 ItemNameStringSize = Main.fontDeathText.MeasureString(ItemName);
			float ItemNameScale = 0.75f;
			Vector2 ItemNameOrigin = new Vector2(ItemNameStringSize.X, ItemNameStringSize.Y * ItemNameScale);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontDeathText, ItemName, ItemNameDrawPosition, Color.White, 0f, ItemNameOrigin / 2, new Vector2(ItemNameScale, ItemNameScale), -1, 2);

			Vector2 DateTimeDrawPosition = space.Position() + new Vector2(MasterBackground.Left.Pixels, MasterBackground.Top.Pixels) + new Vector2(FirstPage.Width.Pixels / 2, 50);
			Vector2 DateTimeSize = Main.fontDeathText.MeasureString(DateTime);
			float DateTimeScale = 0.5f;
			Vector2 DateTimeOrigin = new Vector2(DateTimeSize.X, 0);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontDeathText, DateTime, DateTimeDrawPosition, Color.White, 0f, DateTimeOrigin / 2, new Vector2(DateTimeScale, DateTimeScale), -1, 2);

			Vector2 DaysNewYearDrawPosition = space.Position() + new Vector2(MasterBackground.Left.Pixels, MasterBackground.Top.Pixels) + new Vector2(FirstPage.Width.Pixels / 2, 87);
			Vector2 DaysNewYearSize = Main.fontDeathText.MeasureString(DaysIntoNewYear);
			float DaysNewYearScale = 0.4f;
			Vector2 DaysNewYearOrigin = new Vector2(DaysNewYearSize.X, 0);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontDeathText, DaysIntoNewYear, DaysNewYearDrawPosition, Color.White, 0f, DaysNewYearOrigin / 2, new Vector2(DaysNewYearScale, DaysNewYearScale), -1, 2);

			int DaysUntilNextSeason = GetSeasonDate((int)CurrentSeason + 1, System.DateTime.Today.Year).Subtract(System.DateTime.Today).Days;
			string DaysUntilNextSeasonPlurality = DaysUntilNextSeason == 1 ? string.Empty : "s";
			Seasons nextSeason = (Seasons)(((int)CurrentSeason + 1) % 4);
			string SeasonInformation = "Currently in " + CurrentSeason.ToString() + ", " + DaysUntilNextSeason + " day" + DaysUntilNextSeasonPlurality + " until " + nextSeason.ToString();
			Vector2 SeasonInformationDrawPosition = space.Position() + new Vector2(MasterBackground.Left.Pixels, MasterBackground.Top.Pixels) + new Vector2(FirstPage.Width.Pixels / 2, 115);
			Vector2 SeasonInformationSize = Main.fontDeathText.MeasureString(SeasonInformation);
			float SeasonInformationScale = 0.4f;
			Vector2 SeasonInformationOrigin = new Vector2(SeasonInformationSize.X, 0);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontDeathText, SeasonInformation, SeasonInformationDrawPosition, Color.White, 0f, SeasonInformationOrigin / 2, new Vector2(SeasonInformationScale, SeasonInformationScale), -1, 2);
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