using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class TungstenPlating : TemplatePlating
	{
		public override string Material => "Tungsten";

		public override int Rarity => ItemRarityID.White;

		public override int Value => 3750;

		public override float EffectStrength => 0.45f;

		public override int CraftingMaterial => ItemID.TungstenBar;

		public override int CraftingStation => TileID.Anvils;
	}
}