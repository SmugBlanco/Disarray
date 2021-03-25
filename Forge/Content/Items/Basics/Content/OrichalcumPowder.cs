using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class OrichalcumPowder : TemplatePowder
	{
		public override string Material => "Orichalcum";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 11000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 14;

		public override int CraftingMaterial => ItemID.OrichalcumBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}