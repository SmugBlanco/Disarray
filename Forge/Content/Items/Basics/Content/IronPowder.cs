using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class IronPowder : TemplatePowder
	{
		public override string Material => "Iron";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 2000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 2;

		public override int CraftingMaterial => ItemID.IronBar;

		public override int CraftingStation => TileID.Anvils;
	}
}