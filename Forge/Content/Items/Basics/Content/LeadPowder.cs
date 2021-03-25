using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class LeadPowder : TemplatePowder
	{
		public override string Material => "Lead";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 2500;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 3;

		public override int CraftingMaterial => ItemID.LeadBar;

		public override int CraftingStation => TileID.Anvils;
	}
}