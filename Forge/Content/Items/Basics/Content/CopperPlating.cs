using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class CopperPlating : TemplatePlating
	{
		public override string Material => "Copper";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 1000;

		public override float EffectStrength => 0.4f;

		public override int CraftingMaterial => ItemID.CopperBar;

		public override int CraftingStation => TileID.Anvils;
	}
}