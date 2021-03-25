using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class HellstoneSawblade : TemplateSawblade
	{
		public override string Material => "Hellstone";

		public override int Rarity => ItemRarityID.Green;

		public override int Value => 10000;

		public override float EffectStrength => 0.75f;

		public override int CraftingMaterial => ItemID.HellstoneBar;

		public override int CraftingStation => TileID.Anvils;
	}
}