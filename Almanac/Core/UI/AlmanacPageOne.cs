using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Microsoft.Xna.Framework;
using Disarray.Core.UI;
using static Disarray.Almanac.Core.Data.SeasonData;
using static Disarray.Almanac.Core.Data.Moonphase;
using Disarray.Gardening.Core.Items;
using Disarray.Extensions;
using Disarray.Gardening.Core;

namespace Disarray.Almanac.Core.UI
{
	public partial class AlmanacUI : UIState
	{
		public UIImage FirstPage;

		public UIText Title;

		public bool Lock = true;
		public UIImageButton LockedMovement;

		public UIText DisplayCurrentTime;

		public int DaysIntoNewYear => DateTime.Today.Subtract(new DateTime(DateTime.Today.Year, 1, 1)).Days;
		public UIText DisplayDayIntoNewYear;

		public UIText DisplaySeasonalInformation;

		public MoonphaseDisplay[] moonphaseDisplays;

		public PlantImageDisplay PlantImage;

		public UIItemSlot PlantSlot;

		public PlantInformationDisplay DifficultyDisplay;

		public PlantInformationDisplay LightNeededDisplay;

		public PlantInformationDisplay ThirstinessDisplay;

		public UIDisplayTextbox PlantInformationTextbox;
		public FixedUIScrollbar PlantInformationTextboxScrollbar;

		public void InitializeFirstPage()
		{
			Texture2D bgTexture = ModContent.GetTexture(AssetDirectory + "Almanac_Background");
			FirstPage = new UIImage(bgTexture);
			FirstPage.Left.Set(0, 0f);
			FirstPage.Top.Set(0, 0f);
			FirstPage.Width.Set(bgTexture.Width, 0);
			FirstPage.Height.Set(bgTexture.Height, 0);

			Title = new UIText("The Almanac", 0.8f, true);
			Title.Top.Set(18, 0f);
			Title.HAlign = 0.5f;
			FirstPage.Append(Title);

			Texture2D lockTexture = ModContent.GetTexture("Disarray/Assets/UI/Locked");
			LockedMovement = new UIImageButton(lockTexture);
			LockedMovement.Left.Set(bgTexture.Width - lockTexture.Width - 18, 0f);
			LockedMovement.Top.Set(14, 0f);
			LockedMovement.Width.Set(lockTexture.Width, 0);
			LockedMovement.Height.Set(lockTexture.Height, 0);
			LockedMovement.OnClick += LockedMovement_OnClick;
			FirstPage.Append(LockedMovement);

			DisplayCurrentTime = new UIText(string.Empty, 0.5f, true);
			DisplayCurrentTime.Top.Set(54, 0f);
			DisplayCurrentTime.HAlign = 0.5f;
			FirstPage.Append(DisplayCurrentTime);

			string DaysIntoTheNewYear = DaysIntoNewYear + " days into the New Year";
			DisplayDayIntoNewYear = new UIText(DaysIntoTheNewYear, 0.4f, true);
			DisplayDayIntoNewYear.Top.Set(80, 0f);
			DisplayDayIntoNewYear.HAlign = 0.5f;
			FirstPage.Append(DisplayDayIntoNewYear);

			int DaysUntilNextSeason = GetSeasonDate((int)GetSeasonOnDate(DateTime.Today) + 1, DateTime.Today.Year).Subtract(DateTime.Today).Days;
			string DaysUntilNextSeasonPlurality = DaysUntilNextSeason == 1 ? string.Empty : "s";
			Seasons nextSeason = (Seasons)(((int)GetSeasonOnDate(DateTime.Today) + 1) % 4);
			string SeasonInformation = "Currently in " + GetSeasonOnDate(DateTime.Today).ToString() + ", " + DaysUntilNextSeason + " day" + DaysUntilNextSeasonPlurality + " until " + nextSeason.ToString();
			DisplaySeasonalInformation = new UIText(SeasonInformation, 0.4f, true);
			DisplaySeasonalInformation.Top.Set(104, 0f);
			DisplaySeasonalInformation.HAlign = 0.5f;
			FirstPage.Append(DisplaySeasonalInformation);

			moonphaseDisplays = new MoonphaseDisplay[3];
			Texture2D MoonDisplayBG = ModContent.GetTexture(AssetDirectory + "MoonDisplay_Background");
			for (int Indexer = 0; Indexer < moonphaseDisplays.Length; Indexer++) // Probably should find a way to clean this up
			{
				PhasesOfMoon phase = DateTime.Today.GetMoonphase();
				string headerText = "Today";
				int Left = 14;
				switch (Indexer)
				{
					case 1:
						phase = DateTime.Today.AddDays(1).GetMoonphase();
						headerText = "Tommorow";
						Left = 151;
						break;

					case 2:
						phase = DateTime.Today.AddDays(2).GetMoonphase();
						headerText = "Overmorrow";
						Left = 288;
						break;
				}

				moonphaseDisplays[Indexer] = new MoonphaseDisplay(phase, headerText);
				moonphaseDisplays[Indexer].Left.Set(Left, 0);
				moonphaseDisplays[Indexer].Top.Set(136, 0);
				moonphaseDisplays[Indexer].Width.Set(MoonDisplayBG.Width, 0);
				moonphaseDisplays[Indexer].Height.Set(MoonDisplayBG.Height, 0);
				FirstPage.Append(moonphaseDisplays[Indexer]);
			}

			Texture2D PlantImageTexture = ModContent.GetTexture(AssetDirectory + "PlantImageDisplay");
			PlantImage = new PlantImageDisplay(PlantImageTexture, string.Empty);
			PlantImage.Left.Set(14, 0);
			PlantImage.Top.Set(312, 0);
			FirstPage.Append(PlantImage);

			Texture2D itemSlotTexture = ModContent.GetTexture(AssetDirectory + "Almanac_SeedSlot");
			PlantSlot = new UIItemSlot(itemSlotTexture, (oldItem, item) => item.modItem is SeedItem);
			PlantSlot.Left.Set(300, 0f);
			PlantSlot.Top.Set(312, 0f);
			PlantSlot.Width.Set(itemSlotTexture.Width, 0);
			PlantSlot.Height.Set(itemSlotTexture.Height, 0);
            PlantSlot.OnItemChange += PlantSlot_ItemChanged;
			FirstPage.Append(PlantSlot);

			DifficultyDisplay = new PlantInformationDisplay("Difficulty:", 0.25f, 1, Color.Red);
			DifficultyDisplay.Left.Set(126, 0f);
			DifficultyDisplay.Top.Set(312, 0);
			DifficultyDisplay.Width.Set(166, 0f);
			DifficultyDisplay.Height.Set(38, 0f);
			FirstPage.Append(DifficultyDisplay);

			LightNeededDisplay = new PlantInformationDisplay("Lighting Requirements:", 0.525f, 1, Color.White);
			LightNeededDisplay.Left.Set(126, 0f);
			LightNeededDisplay.Top.Set(352, 0);
			LightNeededDisplay.Width.Set(166, 0f);
			LightNeededDisplay.Height.Set(38, 0f);
			FirstPage.Append(LightNeededDisplay);

			ThirstinessDisplay = new PlantInformationDisplay("Thirstiness:", 0.3125f, 1, Color.SkyBlue);
			ThirstinessDisplay.Left.Set(126, 0f);
			ThirstinessDisplay.Top.Set(392, 0);
			ThirstinessDisplay.Width.Set(166, 0f);
			ThirstinessDisplay.Height.Set(38, 0f);
			FirstPage.Append(ThirstinessDisplay);

			Color BackgroundColour = new Color(142, 121, 114);
			Color BorderColour = new Color(96, 72, 60);

			PlantInformationTextbox = new UIDisplayTextbox(string.Empty);
			PlantInformationTextbox.Top.Set(438, 0f);
			PlantInformationTextbox.Height.Set(126, 0);
			FirstPage.Append(PlantInformationTextbox);
			PlantInformationTextboxScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
			PlantInformationTextboxScrollbar.Left.Set(4, 0f);
			PlantInformationTextboxScrollbar.Height.Set(0, 1f);
			PlantInformationTextboxScrollbar.HAlign = 1f;
			PlantInformationTextbox.Append(PlantInformationTextboxScrollbar);
			PlantInformationTextbox.SetScrollbar(PlantInformationTextboxScrollbar);
			PlantInformationTextbox.BackgroundColor = BackgroundColour;
			PlantInformationTextbox.BorderColor = BorderColour;

			PlantInformationTextbox.Left.Set(14, 0f);
			PlantInformationTextbox.Width.Set(400, 0);

			PlantSlot_ItemChanged();
		}

        private void PlantSlot_ItemChanged()
        {
			GardeningInformation information = new GardeningInformation(string.Empty, string.Empty, string.Empty, 0, 0, (0, 0));

			if (!PlantSlot.Item.IsAir && PlantSlot.Item?.modItem is SeedItem seed)
            {
				information = seed.GeneralInformation;
			}

			PlantImage.imageTexture = ModContent.GetTexture(information.Texture.Equals(string.Empty) ? "Disarray/Assets/Blank" : information.Texture);
			PlantImage.DisplayName = information.DisplayName;
			DifficultyDisplay.ChangeCurrentProgress(information.Difficulty);
			LightNeededDisplay.ChangeCurrentProgress(information.Lighting);
			ThirstinessDisplay.ChangeCurrentProgress(information.Watering.rating);
			ThirstinessDisplay.orbColor = new Color(0, 120, 255);

			switch (information.Watering.type)
            {
				case 1:
					ThirstinessDisplay.orbColor = new Color(255, 75, 0);
					break;

				case 2:
					ThirstinessDisplay.orbColor = new Color(220, 160, 40);
					break;
            }

			PlantInformationTextbox.CurrentText = information.Description;
		}

        private void LockedMovement_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Lock = !Lock;
			string LockTexture = Lock ? "Locked" : "Unlocked";
			LockedMovement.SetImage(ModContent.GetTexture("Disarray/Assets/UI/" + LockTexture));
		}

		public void UpdateFirstPage() => DisplayCurrentTime.SetText(DateTime.Today.DayOfWeek + ", " + DateTime.Now);
	}
}