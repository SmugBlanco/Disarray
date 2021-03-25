using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class LuminiteSawblade : TemplateSawblade
	{
		public override string Material => "Luminite";

		public override int Rarity => ItemRarityID.Red;

		public override int Value => 50000;

		public override float EffectStrength => 2f;

		public override int CraftingMaterial => ItemID.LunarBar;

		public override int CraftingStation => TileID.LunarCraftingStation;
	}
}