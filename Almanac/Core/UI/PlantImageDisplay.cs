using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Disarray.Almanac.Core.UI
{
	public class PlantImageDisplay : UIElement
	{
		public const string AssetDirectory = "Disarray/Almanac/Core/UI/Textures/";

		public Texture2D background;

		public Texture2D imageTexture;

		public string DisplayName;

		public PlantImageDisplay(Texture2D imageTexture, string displayName)
		{
			background = ModContent.GetTexture(AssetDirectory + "PlantImageDisplay");
			this.imageTexture = imageTexture;
			DisplayName = displayName;
			Width.Set(background.Width, 0f);
			Height.Set(background.Height, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point drawPos = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(background, new Rectangle(drawPos.X, drawPos.Y, width, height), Color.White);
			spriteBatch.Draw(imageTexture, new Rectangle(drawPos.X + 6, drawPos.Y + 4, 92, 92), Color.White);

			float TextScale = 0.66f;
			Vector2 DrawPosition = drawPos.ToVector2() + new Vector2(52, 107);
			Vector2 StringSize = Main.fontMouseText.MeasureString(DisplayName);
			Vector2 DrawOrigin = new Vector2(StringSize.X, StringSize.Y * TextScale);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, DisplayName, DrawPosition, Color.White, 0f, DrawOrigin / 2, new Vector2(TextScale, TextScale), -1, 2);
		}
	}
}