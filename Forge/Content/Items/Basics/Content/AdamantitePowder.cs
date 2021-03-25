using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class AdamantitePowder : TemplatePowder
	{
		public override string Material => "Adamantite";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 15000;

		public override bool AutomaticallyCalculateStrength => true;

		public override float EffectStrength => 15;

		public override int CraftingMaterial => ItemID.AdamantiteBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}