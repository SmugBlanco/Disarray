using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class ChlorophyteSawblade : TemplateSawblade
	{
		public override string Material => "Chlorophyte";

		public override int Rarity => ItemRarityID.Green;

		public override int Value => 25000;

		public override float EffectStrength => 1.8f;

		public override int CraftingMaterial => ItemID.ChlorophyteBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}