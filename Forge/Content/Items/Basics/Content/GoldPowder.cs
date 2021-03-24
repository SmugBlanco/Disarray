using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class GoldPowder : TemplatePowder
	{
		public override string Material => "Gold";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 4000;

		public override float EffectStrength => 0.055f;

		public override int CraftingMaterial => ItemID.GoldBar;

		public override int CraftingStation => TileID.Anvils;
	}
}