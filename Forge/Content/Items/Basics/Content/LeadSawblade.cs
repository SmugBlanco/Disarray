using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class LeadSawblade : TemplateSawblade
	{
		public override string Material => "Lead";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 2500;

		public override float EffectStrength => 0.35f;

		public override int CraftingMaterial => ItemID.LeadBar;

		public override int CraftingStation => TileID.Anvils;
	}
}