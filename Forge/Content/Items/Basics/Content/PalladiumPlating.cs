using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class PalladiumPlating : TemplatePlating
	{
		public override string Material => "Palladium";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 8000;

		public override float EffectStrength => 2.2f;

		public override int CraftingMaterial => ItemID.PalladiumBar;

		public override int CraftingStation => TileID.Anvils;
	}
}