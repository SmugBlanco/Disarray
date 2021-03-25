using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class LuminitePowder : TemplatePowder
	{
		public override string Material => "Luminite";

		public override int Rarity => ItemRarityID.Red;

		public override int Value => 50000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 20;

		public override int CraftingMaterial => ItemID.LunarBar;

		public override int CraftingStation => TileID.LunarCraftingStation;
	}
}