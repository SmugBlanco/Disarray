using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class CopperPowder : TemplatePowder
	{
		public override string Material => "Copper";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 1000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 0;

		public override int CraftingMaterial => ItemID.CopperBar;

		public override int CraftingStation => TileID.Anvils;
	}
}