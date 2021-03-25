using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class PalladiumSawblade : TemplateSawblade
	{
		public override string Material => "Palladium";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 8000;

		public override float EffectStrength => 1.1f;

		public override int CraftingMaterial => ItemID.PalladiumBar;

		public override int CraftingStation => TileID.Anvils;
	}
}