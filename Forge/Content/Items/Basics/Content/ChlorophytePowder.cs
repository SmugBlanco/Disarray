using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class ChlorophytePowder : TemplatePowder
	{
		public override string Material => "Chlorophyte";

		public override int Rarity => ItemRarityID.Green;

		public override int Value => 25000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 18;

		public override int CraftingMaterial => ItemID.ChlorophyteBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}