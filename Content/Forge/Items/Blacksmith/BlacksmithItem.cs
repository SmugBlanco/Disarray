using Disarray.Core.Forge.Items;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public abstract class BlacksmithItem : Templates
	{
		public override string ItemDescription() => "Serves as one of the most basic templates to create a custom item.";

		public override string ObtainingDetails() => "Purchasable from your local Blacksmith.";

		public override string MiscDetails() => "Your local Blacksmith may give a discount on these items if you happen to have joined his membership club.";
	}
}