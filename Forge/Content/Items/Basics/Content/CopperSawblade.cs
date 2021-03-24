using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class CopperSawblade : TemplateSawblade
	{
		public override string Material => "Copper";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 1000;

		public override float EffectStrength => 0.2f;

		public override int CraftingMaterial => ItemID.CopperBar;

		public override int CraftingStation => TileID.Anvils;
	}
}