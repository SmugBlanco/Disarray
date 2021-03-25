using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class CrimtanePowder : TemplatePowder
	{
		public override string Material => "Crimtane";

		public override int Rarity => ItemRarityID.Blue;

		public override int Value => 8000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 9;

		public override int CraftingMaterial => ItemID.CrimtaneBar;

		public override int CraftingStation => TileID.Anvils;
	}
}