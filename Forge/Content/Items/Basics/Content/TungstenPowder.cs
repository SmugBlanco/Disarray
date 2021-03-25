using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class TungstenPowder : TemplatePowder
	{
		public override string Material => "Tungsten";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 3750;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 5;

		public override int CraftingMaterial => ItemID.TungstenBar;

		public override int CraftingStation => TileID.Anvils;
	}
}