using Terraria.ID;

namespace Disarray.Forge.Content.Items.Basics.Content
{
	public class HallowedPlating : TemplatePlating
	{
		public override string Material => "Hallowed";

		public override int Rarity => ItemRarityID.LightRed;

		public override int Value => 12000;

		public override float EffectStrength => 3.2f;

		public override int CraftingMaterial => ItemID.HallowedBar;

		public override int CraftingStation => TileID.MythrilAnvil;
	}
}