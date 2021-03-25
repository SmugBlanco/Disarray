using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class SilverPowder : TemplatePowder
	{
		public override string Material => "Silver";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 3000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 4;

		public override int CraftingMaterial => ItemID.SilverBar;

		public override int CraftingStation => TileID.Anvils;
	}
}