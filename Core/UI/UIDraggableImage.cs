using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using System;
using Terraria;

namespace Disarray.Core.UI
{
	public class UIDraggableImage : UIElement
	{
		public Color Color { get; set; } = Color.White;

		public Texture2D Texture { get; private set; }

		public Func<UIMouseEvent, bool> PreDrag;

		public UIDraggableImage(Texture2D givenTexture, Color bgColor, Func<UIMouseEvent, bool> preDrag = null)
		{
			Texture = givenTexture;
			Color = bgColor;
			PreDrag = preDrag;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			if (IsDragging)
			{
				Left.Set(Main.MouseScreen.X - Offset.X, 0f);
				Top.Set(Main.MouseScreen.Y - Offset.Y, 0f);
				Recalculate();
			}

			CalculatedStyle dimensions = GetDimensions();
			Point drawPos = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw(Texture, new Rectangle(drawPos.X, drawPos.Y, width, height), Color);
		}

		private Vector2 Offset = Vector2.Zero;

		public bool IsDragging { get; private set; } = false;

		public override void MouseDown(UIMouseEvent evt)
		{
			if (PreDrag == null || PreDrag(evt))
			{
				Offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
				IsDragging = true;
			}
		}

		public override void MouseUp(UIMouseEvent evt)
		{
			if (PreDrag == null || PreDrag(evt))
			{
				Vector2 endPosition = evt.MousePosition;
				IsDragging = false;
				Left.Set(endPosition.X - Offset.X, 0f);
				Top.Set(endPosition.Y - Offset.Y, 0f);
				Recalculate();
			}
		}
	}
}