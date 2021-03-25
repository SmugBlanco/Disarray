using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class CrimtaneSawblade : TemplateSawblade
	{
		public override string Material => "Crimtane";

		public override int Rarity => ItemRarityID.Blue;

		public override int Value => 8000;

		public override float EffectStrength => 0.65f;

		public override int CraftingMaterial => ItemID.CrimtaneBar;

		public override int CraftingStation => TileID.Anvils;
	}
}