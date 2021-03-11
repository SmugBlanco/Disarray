using Disarray.Content.Gardening.Items.Pests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace Disarray.Core.Gardening.UI
{
	public class PesticideDisplay : GardeningFluidDisplay
	{
		public Texture2D skullTexture;

		public override void InitializeTextures()
		{
			backgroundTexture = ModContent.GetTexture(AssetDirectory + "PesticideDisplay");
			fluidTexture = ModContent.GetTexture(AssetDirectory + "Pesticide");
			fluidTextureTop = ModContent.GetTexture(AssetDirectory + "PesticideTop");
			skullTexture = ModContent.GetTexture(AssetDirectory + "PesticideSkull");
		}

		public override bool CheckItemType() => Player.HeldItem.modItem is PesticideClass;

		public override void PostDraw(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Vector2 drawPosition = dimensions.Position() + new Vector2(Background.Left.Pixels, Background.Top.Pixels);
			spriteBatch.Draw(skullTexture, drawPosition + new Vector2(-9, -24), null, Color.White * GreaterOpacity);
		}
	}
}