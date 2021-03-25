using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class OrichalcumSawblade : TemplateSawblade
	{
		public override string Material => "Orichalcum";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 11000;

		public override float EffectStrength => 1.3f;

		public override int CraftingMaterial => ItemID.OrichalcumBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}