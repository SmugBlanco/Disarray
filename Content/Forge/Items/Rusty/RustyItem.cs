using Disarray.Core.Forge.Items;

namespace Disarray.Content.Forge.Items.Rusty
{
	public abstract class RustyItem : Templates
	{
		public override string ItemDescription() => "Can be repaired in 'The Forge' to become an usable item.";

		public override string ObtainingDetails() => "Found left behind in Wooden Chests and carried on the Undead.";

		public override string MiscDetails() => " ";
	}
}