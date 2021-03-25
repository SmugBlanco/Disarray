using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class MythrilPowder : TemplatePowder
	{
		public override string Material => "Mythril";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 10000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 13;

		public override int CraftingMaterial => ItemID.MythrilBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}