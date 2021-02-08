using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using System;
using Terraria;
using static Disarray.Core.Data.Moonphase;
using Terraria.ModLoader;
using System.Text.RegularExpressions;
using ReLogic.Graphics;
using Terraria.UI.Chat;
using Disarray.Core.Data;
using System.Collections.Generic;

namespace Disarray.Core.Almanac.UI
{
	public class CropDisplay : UIElement
	{
		public const string AssetDirectory = "Disarray/Core/Almanac/UI/Textures/";

		public Texture2D background;

		public int currentCrop => HandleCropDisplay.CurrentCrop;

		public Color currentCropColor => HandleCropDisplay.CurrentColor;

		public int nextCrop = (HandleCropDisplay.CurrentCrop + 1) % Crop.LoadedCrops.Count;

		public int CropSwitchTimer => HandleCropDisplay.CropTimer;

		public event Action CropChanged;

		public CropDisplay()
		{
			background = ModContent.GetTexture(AssetDirectory + "CropDisplay_Background");
		}

		public void ForceChange(int nextCrop)
        {
			HandleCropDisplay.CropTimer = 0;
			HandleCropDisplay.CurrentCrop = nextCrop;
			HandleCropDisplay.NextCrop = (HandleCropDisplay.CurrentCrop + 1) % Crop.LoadedCrops.Count;
			CropChanged.Invoke();
		}

        protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point drawPos = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(background, new Rectangle(drawPos.X, drawPos.Y, width, height), Color.White);

			if (currentCrop == nextCrop)
            {
				nextCrop = (HandleCropDisplay.CurrentCrop + 1) % Crop.LoadedCrops.Count;
				CropChanged.Invoke();
            }

			if (Crop.CropsImageData.TryGetValue(currentCrop, out Texture2D drawnCropTexture))
			{
				Vector2 CropImageDrawPosition = new Vector2(drawPos.X + 10, drawPos.Y + 8) + new Vector2(39, 39);
				float CropImageScale = 1f;
				if (drawnCropTexture.Width > 78 || drawnCropTexture.Height > 78)
                {
					CropImageScale = drawnCropTexture.Width >= drawnCropTexture.Height ? 78 / (int)drawnCropTexture.Width : 78 / (int)drawnCropTexture.Height;
				}
				spriteBatch.Draw(drawnCropTexture, CropImageDrawPosition, null, currentCropColor, 0f, drawnCropTexture.Size() / 2, CropImageScale, SpriteEffects.None, 0f);
			}
			
			float TextScale = 0.75f;
			Vector2 DrawPosition = drawPos.ToVector2() + new Vector2(49, 99);
			Vector2 StringSize = Main.fontMouseText.MeasureString(Crop.LoadedCrops[currentCrop].name);
			Vector2 DrawOrigin = new Vector2(StringSize.X, StringSize.Y * TextScale);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, Crop.LoadedCrops[currentCrop].name, DrawPosition, Color.White, 0f, DrawOrigin / 2, new Vector2(TextScale, TextScale), -1, 2);
		}

		public static class HandleCropDisplay
		{
			public static int CurrentCrop;

			public static int NextCrop = (CurrentCrop + 1) % Crop.LoadedCrops.Count;

			public static int CropTimer;

			public static Color CurrentColor = new Color(2.55f, 2.55f, 2.55f, 2.55f);

			public static readonly int BaseTime = 900;

			public static void Update()
			{
				if (CropTimer > BaseTime)
				{
					if (CropTimer < BaseTime + 50)
					{
						if (CurrentColor.A <= 0)
						{
							CropTimer++;
						}
						else
						{
							CurrentColor.A -= 5;
							CurrentColor.R -= 5;
							CurrentColor.G -= 5;
							CurrentColor.B -= 5;
						}
					}
					else
					{
						if (CropTimer == BaseTime + 50)
						{
							CurrentCrop = NextCrop;
							NextCrop = (CurrentCrop + 1) % Crop.LoadedCrops.Count;
							CropTimer++;
						}

						if (CurrentColor.A >= 255)
						{
							CropTimer = 0;
						}
						else
						{
							CurrentColor.A += 5;
							CurrentColor.R += 5;
							CurrentColor.G += 5;
							CurrentColor.B += 5;
						}
					}
				}
				else
				{
					CropTimer++;
				}
			}
		}
	}
}