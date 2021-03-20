using Disarray.Gardening.Core.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Disarray.Gardening.Core.UI
{
	public abstract class GardeningFluidDisplay : UIState
	{
		public Vector2 ScreenCenter => new Vector2(Main.screenWidth / 2 - backgroundTexture.Width / 2, Main.screenHeight / 2 - backgroundTexture.Height / 2);

		public UIElement Background;

		public Texture2D backgroundTexture;

		public Texture2D fluidTexture;

		public Texture2D fluidTextureTop;

		public Player Player => Main.LocalPlayer;

		public Item HeldItem => Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;

		public int TimeSinceLastInteraction => HeldItem?.modItem is GardeningUsableItem gardeningItem ? gardeningItem.GetTimeSinceLastInteraction : 999;

		public const float AppearTime = 180;

		public const float DisappearTime = 60;

		public float GreaterOpacity => TimeSinceLastInteraction < AppearTime ? 1 : Utils.Clamp(1f - (TimeSinceLastInteraction - AppearTime) / DisappearTime, 0, 1);

		public GardeningFluidDisplay() => InitializeTextures();

		public abstract void InitializeTextures();

		public override void OnInitialize()
		{
			Background = new UIElement();
			Background.Left.Set(ScreenCenter.X - 40 * Player.direction, 0);
			Background.Top.Set(ScreenCenter.Y, 0f);
			Background.Width.Set(backgroundTexture.Width, 0);
			Background.Height.Set(backgroundTexture.Height, 0);
			Append(Background);
		}

		public abstract bool CheckItemType();

		public override void Update(GameTime gameTime)
		{
			if (!CheckItemType())
			{
				ModContent.GetInstance<Disarray>().GardeningInterface?.SetState(null);
				return;
			}

			Background.Left.Set(ScreenCenter.X - 40 * Player.direction, 0f);
		}

		public sealed override void Draw(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Vector2 drawPosition = dimensions.Position() + new Vector2(Background.Left.Pixels, Background.Top.Pixels);
			if (HeldItem?.modItem is GardeningUsableItem gardeningItem && gardeningItem.GetQuantity > 0)
			{
				float cannisterCapacity = gardeningItem.GetQuantity / gardeningItem.MaxQuantity;
				Rectangle sourceRectangle = new Rectangle(0, 0, fluidTexture.Width, (int)(fluidTexture.Height * cannisterCapacity));
				Vector2 waterDrawPosition = drawPosition + new Vector2(0, fluidTexture.Height - sourceRectangle.Height) + new Vector2(6, 10);
				spriteBatch.Draw(fluidTexture, waterDrawPosition, sourceRectangle, Color.White * 0.75f * GreaterOpacity);

				if (gardeningItem.GetQuantity < gardeningItem.MaxQuantity)
				{
					spriteBatch.Draw(fluidTextureTop, waterDrawPosition + new Vector2(0, -4), null, Color.White * 0.75f * GreaterOpacity);
				}
			}

			spriteBatch.Draw(backgroundTexture, drawPosition, null, Color.White * GreaterOpacity);

			PostDraw(spriteBatch);
		}

		public virtual void PostDraw(SpriteBatch spriteBatch) { }
	}
}