using Disarray.Content.Gardening.Items.Watering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace Disarray.Core.Gardening.UI
{
	public class WaterDisplay : GardeningFluidDisplay
	{
		public Texture2D flowerTexture;

		public override void InitializeTextures()
		{
			backgroundTexture = ModContent.GetTexture(AssetDirectory + "WaterDisplay");
			fluidTexture = ModContent.GetTexture(AssetDirectory + "Water");
			fluidTextureTop = ModContent.GetTexture(AssetDirectory + "WaterTop");
			flowerTexture = ModContent.GetTexture(AssetDirectory + "Flower");
		}

		public override bool CheckItemType() => Player.HeldItem.modItem is WateringCanClass;

		public override void PostDraw(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Vector2 drawPosition = dimensions.Position() + new Vector2(Background.Left.Pixels, Background.Top.Pixels);
			spriteBatch.Draw(flowerTexture, drawPosition + new Vector2(10, -8), null, Color.White * GreaterOpacity);
		}
	}
}