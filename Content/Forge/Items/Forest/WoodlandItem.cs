using Disarray.Core.Forge.Items;
using Terraria;

namespace Disarray.Content.Forge.Items.Forest
{
	public abstract class WoodlandItem : Templates
	{
		public override string ItemDescription() => "Can be repaired in 'The Forge' to become an usable item.";

		public override string ObtainingDetails() => "Crafted from pieces of wood, binded together with common mushrooms.";

		public override string MiscDetails() => " ";

		public override void HoldItem(Player player)
		{
			player.lifeRegen += 1;
		}
	}
}