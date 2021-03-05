using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Disarray.Content.Gardening.Items.Watering;

namespace Disarray.Core.Gardening.UI
{
	public class WaterDisplay : UIState
	{
		public const string AssetDirectory = "Disarray/Core/Gardening/UI/";

		public Vector2 ScreenCenter => new Vector2(Main.screenWidth / 2 - backgroundTexture.Width / 2, Main.screenHeight / 2 - backgroundTexture.Height / 2);

		public UIElement Background;

		public Texture2D backgroundTexture;

		public Texture2D waterTexture;

		public Texture2D waterTextureTop;

		public Texture2D flowerTexture;

		public Player Player => Main.LocalPlayer;

		public Item HeldItem => Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;

		public int TimeSinceLastInteraction => HeldItem?.modItem is WateringCan wateringCan ? wateringCan.TimeSinceLastInteract : 999;

		public const float AppearTime = 180;

		public const float DisappearTime = 60;

		public float GreaterOpacity => TimeSinceLastInteraction < AppearTime ? 1 : Utils.Clamp(1f - (TimeSinceLastInteraction - AppearTime) / DisappearTime, 0, 1);

		public WaterDisplay()
		{
			backgroundTexture = ModContent.GetTexture(AssetDirectory + "WaterDisplay");
			waterTexture = ModContent.GetTexture(AssetDirectory + "Water");
			waterTextureTop = ModContent.GetTexture(AssetDirectory + "WaterTop");
			flowerTexture = ModContent.GetTexture(AssetDirectory + "Flower");
		}

		public override void OnInitialize()
		{
			Background = new UIElement();
			Background.Left.Set(ScreenCenter.X - 40 * Player.direction, 0);
			Background.Top.Set(ScreenCenter.Y, 0f);
			Background.Width.Set(backgroundTexture.Width, 0);
			Background.Height.Set(backgroundTexture.Height, 0);
			Append(Background);
		}

		public override void Update(GameTime gameTime)
		{
			if (!(Player.HeldItem.modItem is WateringCan))
			{
				ModContent.GetInstance<Disarray>().GardeningInterface?.SetState(null);
				return;
			}

			Background.Left.Set(ScreenCenter.X - 40 * Player.direction, 0f);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Vector2 drawPosition = dimensions.Position() + new Vector2(Background.Left.Pixels, Background.Top.Pixels);
			if (HeldItem?.modItem is WateringCan wateringCan && wateringCan.GetWaterLevel > 0)
			{
				float cannisterCapacity = wateringCan.GetWaterLevel / 100f;
				Rectangle sourceRectangle = new Rectangle(0, 0, waterTexture.Width, (int)(waterTexture.Height * cannisterCapacity));
				Vector2 waterDrawPosition = drawPosition + new Vector2(0, waterTexture.Height - sourceRectangle.Height) + new Vector2(6, 10);
				spriteBatch.Draw(waterTexture, waterDrawPosition, sourceRectangle, Color.White * 0.75f * GreaterOpacity);

				if (wateringCan.GetWaterLevel < 100)
				{
					spriteBatch.Draw(waterTextureTop, waterDrawPosition + new Vector2(0, -4), null, Color.White * 0.75f * GreaterOpacity);
				}
			}

			spriteBatch.Draw(backgroundTexture, drawPosition, null, Color.White * GreaterOpacity);

			spriteBatch.Draw(flowerTexture, drawPosition + new Vector2(10, -8), null, Color.White * GreaterOpacity);
		}
	}
}