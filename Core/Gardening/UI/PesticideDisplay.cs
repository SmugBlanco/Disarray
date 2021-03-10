using Disarray.Content.Gardening.Items.Pests;
using Terraria.ModLoader;

namespace Disarray.Core.Gardening.UI
{
	public class PesticideDisplay : GardeningFluidDisplay
	{
		public override void InitializeTextures()
		{
			backgroundTexture = ModContent.GetTexture(AssetDirectory + "PesticideDisplay");
			fluidTexture = ModContent.GetTexture(AssetDirectory + "Pesticide");
			fluidTextureTop = ModContent.GetTexture(AssetDirectory + "PesticideTop");
		}

		public override bool CheckItemType() => Player.HeldItem.modItem is PesticideClass;
	}
}