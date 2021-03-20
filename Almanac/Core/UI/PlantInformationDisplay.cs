using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.UI.Chat;

namespace Disarray.Almanac.Core.UI
{
	public class PlantInformationDisplay : UIElement
	{
		public const string AssetDirectory = "Disarray/Almanac/Core/UI/Textures/";

		public Texture2D background;

		public Texture2D orbTexture;

		public string DisplayName { get; } = string.Empty;

        public float CurrentValue { get; private set; }

        public float MaximumValue { get; }

        public float ProgressToMax
        {
			get
            {
				float CurrentProgress = CurrentValue / MaximumValue;

				if (CurrentProgress < 0)
                {
					CurrentProgress = 0;
                }

				if (CurrentProgress > 1)
                {
					CurrentProgress = 1;
                }

				return CurrentProgress;
            }
        }

		public Color orbColor { get; set; }

		public PlantInformationDisplay(string displayName, float currentValue, float maxValue, Color orbColor)
		{
			background = ModContent.GetTexture(AssetDirectory + "PlantInformationDisplay_Background");
			orbTexture = ModContent.GetTexture(AssetDirectory + "PlantInformationDisplay_Orb");
			DisplayName = displayName;
			CurrentValue = currentValue;
			MaximumValue = maxValue;
			this.orbColor = orbColor;
		}

		public void ChangeCurrentProgress(float newCurrentValue)
        {
			CurrentValue = newCurrentValue;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point drawPos = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(background, new Rectangle(drawPos.X, drawPos.Y, width, height), Color.White);

			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, DisplayName, drawPos.ToVector2() + new Vector2(4, 4), Color.White, 0f, Vector2.Zero, new Vector2(0.66f, 0.66f));

			float CurrentProgress = ProgressToMax * 10;
			for (int Indexer = 0; Indexer < Math.Ceiling(CurrentProgress); Indexer++)
            {
				int offsetX = (orbTexture.Width + 2) * Indexer;
				Vector2 orbDrawPosition = drawPos.ToVector2() + new Vector2(offsetX + 4, 20);

				float progressOnNextOrb = (CurrentProgress - (int)CurrentProgress) / 1;

				int sourceRectX = orbTexture.Width;
				if (progressOnNextOrb % 1 != 0 && Indexer == Math.Ceiling(CurrentProgress) - 1)
				{
					sourceRectX = (int)(progressOnNextOrb * orbTexture.Width);
				}

				Rectangle sourceRect = new Rectangle(0, 0, sourceRectX, orbTexture.Height);
				spriteBatch.Draw(orbTexture, orbDrawPosition, sourceRect, orbColor);
			}
		}
	}
}