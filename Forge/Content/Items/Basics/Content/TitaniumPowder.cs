using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class TitaniumPowder : TemplatePowder
	{
		public override string Material => "Titanium";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 17500;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 16;

		public override int CraftingMaterial => ItemID.TitaniumBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}