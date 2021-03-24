using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class TinPowder : TemplatePowder
	{
		public override string Material => "Tin";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 1250;

		public override float EffectStrength => 0.03f;

		public override int CraftingMaterial => ItemID.TinBar;

		public override int CraftingStation => TileID.Anvils;
	}
}