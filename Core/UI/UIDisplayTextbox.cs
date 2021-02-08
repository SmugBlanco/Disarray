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
using System.IO;
using Terraria.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria.UI.Chat;

namespace Disarray.Core.UI
{
	public class UIDisplayTextbox : UIPanel
	{
		public string CurrentText = string.Empty;
		public Vector2 Padding = new Vector2(8, 6);
		public float _Height;
		public UIScrollbar scrollbar;

		public UIDisplayTextbox(string Text)
		{
			CurrentText = Text;
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
			string textReformatted = dynamicSprite.CreateWrappedText(CurrentText, (Width.Pixels * 1.33333f) - scrollbar.Width.Pixels * 1.66f - Padding.X * 2);
			_Height = dynamicSprite.MeasureString(textReformatted).Y * (22f / 28);
			string[] DisplayedText = textReformatted.Split('\n');
			CalculatedStyle space = GetDimensions();
			Vector2 drawPos = space.Position() + Padding;
			foreach (string text in DisplayedText)
			{
				float TextHeight = dynamicSprite.MeasureString(text).Y * 0.75f;
				if (offset + TextHeight > space.Height)
				{
					break;
				}

				if (offset >= 0f)
				{
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, dynamicSprite, text, drawPos + new Vector2(0, offset), Color.White, 0f, Vector2.Zero, new Vector2(0.75f, 0.75f), -1, 2);
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