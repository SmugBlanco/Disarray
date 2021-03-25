using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class PalladiumPowder : TemplatePowder
	{
		public override string Material => "Palladium";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 8000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 12;

		public override int CraftingMaterial => ItemID.PalladiumBar;

		public override int CraftingStation => TileID.Anvils;
	}
}