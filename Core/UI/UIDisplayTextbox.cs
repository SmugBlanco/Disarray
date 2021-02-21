using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using ReLogic.Graphics;
using Terraria.UI.Chat;

namespace Disarray.Core.UI
{
	public class UIDisplayTextbox : UIPanel
	{
		public string CurrentText = string.Empty;
		public float TextScale;
		public Vector2 Padding = new Vector2(8, 6);
		public float _Height;
		public UIScrollbar scrollbar;

		public UIDisplayTextbox(string Text, float textScale = 0.75f)
		{
			CurrentText = Text;
			TextScale = textScale;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			DynamicSpriteFont dynamicSprite = Main.fontMouseText;

			float offset = 0f;
			if (scrollbar != null)
			{
				offset = 0f - scrollbar.GetValue();
			}

			float WidthAdjustedForScale = Width.Pixels * (1 / TextScale);
			float WidthAdjustedForScrollbar = scrollbar == null ? 0 : scrollbar.Width.Pixels * 1.66f;
			float WidthAdjustedForPadding = Padding.X * 2;
			string textReformatted = dynamicSprite.CreateWrappedText(CurrentText, WidthAdjustedForScale - WidthAdjustedForScrollbar - WidthAdjustedForPadding);

			_Height = dynamicSprite.MeasureString(textReformatted).Y * (22f / 28);

			string[] DisplayedText = textReformatted.Split('\n');

			CalculatedStyle space = GetDimensions();

			Vector2 drawPos = space.Position() + Padding;

			foreach (string text in DisplayedText)
			{
				float TextHeight = dynamicSprite.MeasureString(text).Y * 0.75f;

				if (offset + TextHeight > space.Height - 12)
				{
					break;
				}

				if (offset >= 0f)
				{
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, dynamicSprite, text, drawPos + new Vector2(0, offset), Color.White, 0f, Vector2.Zero, new Vector2(TextScale, TextScale), -1, 2);
				}

				offset += TextHeight;
			}

			Recalculate();
		}

		public override void Recalculate()
		{
			base.Recalculate();
			UpdateScrollbar();
		}

		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (scrollbar != null)
			{
				scrollbar.ViewPosition -= evt.ScrollWheelValue / 3;
			}
		}

		public void SetScrollbar(UIScrollbar scrollbar)
		{
			this.scrollbar = scrollbar;
			UpdateScrollbar();
		}

		private void UpdateScrollbar()
		{
			scrollbar?.SetView(GetInnerDimensions().Height, _Height);
		}
	}
}