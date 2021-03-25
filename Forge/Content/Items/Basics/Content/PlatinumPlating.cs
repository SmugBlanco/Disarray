using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class PlatinumPlating : TemplatePlating
	{
		public override string Material => "Platinum";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 5000;

		public override float EffectStrength => 1.1f;

		public override int CraftingMaterial => ItemID.PlatinumBar;

		public override int CraftingStation => TileID.Anvils;
	}
}