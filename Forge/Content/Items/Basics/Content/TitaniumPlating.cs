using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class TitaniumPlating : TemplatePlating
	{
		public override string Material => "Titanium";

		public override int Rarity => ItemRarityID.Orange;

		public override int Value => 17500;

		public override float EffectStrength => 3f;

		public override int CraftingMaterial => ItemID.TitaniumBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}