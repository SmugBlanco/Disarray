using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class MythrilPlating : TemplatePlating
	{
		public override string Material => "Mythril";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 10000;

		public override float EffectStrength => 2.4f;

		public override int CraftingMaterial => ItemID.MythrilBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}