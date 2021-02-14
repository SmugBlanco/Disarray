using Disarray.Core.Forge.Items;

namespace Disarray.Content.Forge.Items.Cloud
{
	public abstract class CloudItem : Templates
	{
		public override string ItemDescription() => "Can be modified in 'The Forge' to become an usable item.";

		public override string ObtainingDetails() => "Crafted at a pool of water from solidified clouds, bound together by fallen stars.";

		public override string MiscDetails() => " ";
	}
}