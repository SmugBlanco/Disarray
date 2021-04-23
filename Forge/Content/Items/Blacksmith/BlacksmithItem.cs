using Disarray.Forge.Core.Items;

namespace Disarray.Forge.Content.Items.Blacksmith
{
	public abstract class BlacksmithItem : ForgeTemplate
	{
		public override string GeneralDescription => "Serves as one of the most basic templates to create a custom item.";

		public override string ObtainingGuide => "Purchasable from your local Blacksmith.";

		public override string Miscellaneous => "Your local Blacksmith may give a discount on these items if you happen to have joined his membership club.";
	}
}