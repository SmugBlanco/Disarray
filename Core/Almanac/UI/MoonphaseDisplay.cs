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

namespace Disarray.Core.Almanac.UI
{
	public class MoonphaseDisplay : UIElement
	{
		public const string AssetDirectory = "Disarray/Core/Almanac/UI/Textures/";

		public Texture2D background;

		public Texture2D moonTexture;

		public PhasesOfMoon moonPhase;

		public string DisplayString = string.Empty;

		public string header;

		public MoonphaseDisplay(PhasesOfMoon phase, string header)
		{
			moonPhase = phase;
			this.header = header;
			background = ModContent.GetTexture(AssetDirectory + "MoonDisplay_Background");
			moonTexture = ModContent.GetTexture(AssetDirectory + "Moonphases/" + moonPhase.ToString());
			DisplayString = AddSpaceBeforeOtherCapitalLetters(moonPhase.ToString());
		}

		public string AddSpaceBeforeOtherCapitalLetters(string Input)
		{
			if (string.IsNullOrWhiteSpace(Input))
            {
				return string.Empty;
            }

			if (Input.Length < 2)
            {
				return Input;
            }

			string NewString = string.Empty;
			for (int Index = 0; Index < Input.Length; Index++)
            {
				if (char.IsUpper(Input[Index]))
                {
					NewString += " ";
				}
				
				NewString += Input[Index];
            }
			return NewString;
        }

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point drawPos = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(background, new Rectangle(drawPos.X, drawPos.Y, width, height), Color.White);
			spriteBatch.Draw(moonTexture, new Rectangle((int)(drawPos.X + 30), (int)(drawPos.Y + 49), (int)(moonTexture.Width * 1.5f), (int)(moonTexture.Height * 1.5f)), Color.White);

			float TextScale = 0.8f;
			Vector2 DrawPosition = drawPos.ToVector2() + new Vector2(8, 131) + new Vector2(55, 15);
			Vector2 StringSize = Main.fontMouseText.MeasureString(DisplayString);
			Vector2 DrawOrigin = new Vector2(StringSize.X, StringSize.Y * TextScale);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, DisplayString, DrawPosition, Color.White, 0f, DrawOrigin / 2, new Vector2(TextScale, TextScale), -1, 2);

			Vector2 HeaderDrawPos = drawPos.ToVector2() + new Vector2(8, 8) + new Vector2(55, 15);
			Vector2 HeaderSize = Main.fontMouseText.MeasureString(header);
			Vector2 HeaderDrawOrigin = new Vector2(HeaderSize.X, HeaderSize.Y * TextScale);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, header, HeaderDrawPos, Color.White, 0f, HeaderDrawOrigin / 2, new Vector2(TextScale, TextScale), -1, 2);
		}
	}
}