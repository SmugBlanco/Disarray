using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class CobaltSawblade : TemplateSawblade
	{
		public override string Material => "Cobalt";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 7500;

		public override float EffectStrength => 1f;

		public override int CraftingMaterial => ItemID.CobaltBar;

		public override int CraftingStation => TileID.Anvils;
	}
}