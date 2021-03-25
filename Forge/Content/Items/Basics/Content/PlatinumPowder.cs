using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class PlatinumPowder : TemplatePowder
	{
		public override string Material => "Platinum";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 5000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 7;

		public override int CraftingMaterial => ItemID.PlatinumBar;

		public override int CraftingStation => TileID.Anvils;
	}
}