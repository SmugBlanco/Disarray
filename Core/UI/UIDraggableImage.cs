using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using System;
using Terraria;

namespace Disarray.Core.UI
{
	public class UIDraggableImage : UIElement
	{
		public Color color = Color.White;
		public Texture2D texture;

		public UIDraggableImage(Texture2D givenTexture, Color bgColor)
		{
			texture = givenTexture;
			color = bgColor;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);

			if (ContainsPoint(MousePosition))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			if (isDragging)
			{
				Left.Set(MousePosition.X - offset.X, 0f);
				Top.Set(MousePosition.Y - offset.Y, 0f);
				Recalculate();
			}

			CalculatedStyle dimensions = GetDimensions();
			Point drawPos = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(texture, new Rectangle(drawPos.X, drawPos.Y, width, height), color);
		}

		private Vector2 offset = Vector2.Zero;
		public bool isDragging = false;

        public override void MouseDown(UIMouseEvent evt)
        {
			offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
			isDragging = true;
		}

        public override void MouseUp(UIMouseEvent evt)
        {
			Vector2 endPosition = evt.MousePosition;
			isDragging = false;
			Left.Set(endPosition.X - offset.X, 0f);
			Top.Set(endPosition.Y - offset.Y, 0f);
			Recalculate();
		}
    }
}