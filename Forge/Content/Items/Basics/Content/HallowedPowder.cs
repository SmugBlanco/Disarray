using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class HallowedPowder : TemplatePowder
	{
		public override string Material => "Hallowed";

		public override int Rarity => ItemRarityID.LightRed;

		public override int Value => 12000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 17;

		public override int CraftingMaterial => ItemID.HallowedBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}