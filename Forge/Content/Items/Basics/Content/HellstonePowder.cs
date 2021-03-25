using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class HellstonePowder : TemplatePowder
	{
		public override string Material => "Hellstone";

		public override int Rarity => ItemRarityID.Green;

		public override int Value => 10000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 10;

		public override int CraftingMaterial => ItemID.HellstoneBar;

		public override int CraftingStation => TileID.Anvils;
	}
}