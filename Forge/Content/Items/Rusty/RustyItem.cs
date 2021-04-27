using Disarray.Forge.Core.Items;

namespace Disarray.Forge.Content.Items.Rusty
{
	public abstract class RustyItem : ForgeTemplate
	{
		public override string GeneralDescription => "Due to it's current state, it's unusable. Perhaps you can repair this in 'The Forge'";

		public override string ObtainingGuide => "Found stowed away in chests and carried on the undead.";
	}
}