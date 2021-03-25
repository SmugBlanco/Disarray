using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class DemonitePlating : TemplatePlating
	{
		public override string Material => "Demonite";

		public override int Rarity => ItemRarityID.Blue;

		public override int Value => 7500;

		public override float EffectStrength => 1.2f;

		public override int CraftingMaterial => ItemID.DemoniteBar;

		public override int CraftingStation => TileID.Anvils;
	}
}