using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class AdamantitePlating : TemplatePlating
	{
		public override string Material => "Adamantite";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 15000;

		public override float EffectStrength => 2.8f;

		public override int CraftingMaterial => ItemID.AdamantiteBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}